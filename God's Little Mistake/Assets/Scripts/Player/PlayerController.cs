using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerController : Singleton<PlayerController>
{
    public CharacterController controller;
    public BearTrap bearTrap;
    public CactusTrap cactusTrap;

    [Header("God Cheats")]
    public bool immortal;
    public bool canFloat;
    public float rotateAmount;
    public float time;
    public Ease ease;

    [Header("Temporary")]
    public SpriteRenderer UIrangeIndicator;

    #region Animation Variables
    [Header("Animation")]
    [SerializeField]
    GameObject playerAvatar;

    public Animator baseAnimator;
    public Animator nubsAnimator;
    public GameObject nubsOB;

    public Animator[] slotsAnim;
    public GameObject[] slotsGO;

    public GameObject missyLeftSide;
    public GameObject missyRightSide;
    public GameObject missyBack;
    public GameObject missyForward;

    Animator missyLeftSideAnim;
    Animator missyRightSideAnim;
    Animator missyFrontSideAnim;
    Animator missyBackSideAnim;

    public GameObject meleeUI;

    public List<Animator> itemsAnimForward;
    public List<Animator> itemsAnimRightSide;
    public List<Animator> itemsAnimLeftSide;
    public List<Animator> itemsAnimBack;

    GameObject currentHead;
    [SerializeField]
    GameObject frontHead;    
    [SerializeField]
    GameObject backHead;    
    [SerializeField]
    GameObject sideRHead;
    [SerializeField]
    GameObject sideLHead;

    public List<Animator> legsAnimators;

    bool meleeAnimationCooldown;



    Vector3 currentPos;
    Vector3 lastPos;

    public GameObject frontPivot;
    public GameObject rightPivot;
    public GameObject leftPivot;
    public GameObject backPivot;
    #endregion


    [Header("Physics")]
    public float gravity = 9.8f;
    public Vector3 lastPosOnGround;
    public GameObject groundCheck;
    public bool isGrounded;
    public LayerMask groundMask;
    public GameObject tileLastStoodOn;
    public bool fallDamage;

    #region Player Variables
    [Header("Player Stats")]
    public float health;
    public float maxHP;
    public float speed;
    public float maxSpeed;
    public float dmg;
    public float projectileRange;
    public float meleeRange;
    public float meleeFirerate;
    public float projectileFirerate;
    public float critX;
    public float critChance;

    public bool projectile;
    public float projectileSpeed;
    public float projectileKnockback;
    public GameObject projectilePF;


    [SerializeField]
    int initalSpeedBoost = 3;
    public bool isMoving;
    GameObject lastDir;
    Tween speedTween;

    #endregion
    
    [Header("Particle Systems")]
    public ParticleSystem deathExplosionPS;
    public GameObject missyDeathAnim;
    [SerializeField]
    ParticleSystem spitParticleSystem;

    [Header("Inventory")]
    public List<Item> playerInventory;


    [Header("Projectile")]
    [SerializeField]
    GameObject firingPointCurrent;
    public GameObject firingPointDefault;

    [SerializeField]
    GameObject[] firingPointActive;

    public GameObject directional; //is for current melee attack and will probably be removed
    public Vector3 target;
    bool projectileShot;


    [Header("Knockback")]
    [SerializeField]
    float knockbackAmount;
    bool knockbackActive;
    public float knockbackDuration = 0.5f;
    private float knockbackStartTime;

    [Header("Movement")]
    public bool enableMovement = true;
    public Vector3 move;

    [Header("Melee")]
    bool meleeCooDown;
    public GameObject lineHitbox;
    public GameObject coneHitbox;
    MeleeUISwitcher meleeUISwitcher;
    public enum MeleeHitBox { Line, Cone, None }
    public MeleeHitBox meleeHitBox;

    public float dashAmount;

    private void Start()
    {
        firingPointActive = new GameObject[4];

        health = maxHP;
        controller = gameObject.GetComponent<CharacterController>();
        _UI.UpdateHealthText(health);
        lastPos = transform.position;

        groundCheck = GameObject.Find("GroundCheck");

        meleeUISwitcher = GetComponent<MeleeUISwitcher>();

        CheckForStartingItems();

        missyBackSideAnim = missyBack.GetComponent<Animator>();
        missyFrontSideAnim = missyForward.GetComponent<Animator>();
        missyLeftSideAnim = missyLeftSide.GetComponent<Animator>();
        missyRightSideAnim = missyRightSide.GetComponent<Animator>();

        currentHead = frontHead;

        firingPointCurrent = firingPointDefault;
    }

    void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            HeadBobble();
        }

        if (health <= 0)
        {
            Die();
            Debug.Log("Die");
        }


        UpdateMelee();
        _UI.UpdateHealthText(health);

        switch (_GM.gameState)
        {
            case GameManager.GameState.Playing:

                #region Movement

                if(enableMovement)
                {
                    move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    if (!canFloat)
                    {
                        move.y -= gravity;
                    }

                    controller.Move(move * Time.deltaTime * speed);


                    #region isMoving Check
                    //find inpus for movement
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        if (!isMoving)
                        {
                            speed = speed + initalSpeedBoost;

                            ExecuteAfterSeconds(0.1f, () => TweenSpeed(maxSpeed, 1));
                        }

                    }



                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) isMoving = true;
                    else isMoving = false;

                    if (!isMoving)
                    {
                        speed = maxSpeed;
                    }

                    isGrounded = Physics.CheckSphere(groundCheck.transform.position, 1, groundMask);

                }



                #endregion

                #region Respawn If Fell Off

                //grounded check

                if (isGrounded)
                {
                    lastPosOnGround = lastPos;
                    var col = Physics.OverlapSphere(groundCheck.transform.position, 0.5f, groundMask);
                    if (col.Length != 0) tileLastStoodOn = col[0].gameObject;
                    fallDamage = true;


                }
                else
                {
                    if(transform.position.y < -3)
                    {

                        //find closest tile, put player above
                        Vector3 targetPos = new Vector3(tileLastStoodOn.transform.position.x, 4, tileLastStoodOn.transform.position.z);
                        print("Put on ground");

                        //player lose health when fall off

                        if (fallDamage)
                        {
                            fallDamage = false;
                            health = (health / 2) - 1;
                            print("Taken fall damage");
                            ExecuteAfterSeconds(1, () => transform.position = targetPos);

                        }

                    }
                    
                }

                //while grounded lastPosGrounded = lastPos;

                //if not grounded execute after 1 second and respawn them

                //for some pizazz could have them fly back in from the roof


                #endregion

                #endregion

                #region Animation

                #region Enable Walking Animation

                

                if(isMoving)
                {
                    missyFrontSideAnim.SetBool("Walking", true);
                    missyBackSideAnim.SetBool("Walking", true);
                    missyLeftSideAnim.SetBool("Walking", true);
                    missyRightSideAnim.SetBool("Walking", true);
                }
                else
                {
                    missyFrontSideAnim.SetBool("Walking", false);
                    missyBackSideAnim.SetBool("Walking", false);
                    missyLeftSideAnim.SetBool("Walking", false);
                    missyRightSideAnim.SetBool("Walking", false);
                }



                #endregion

                #region Legs Animation

                //Idle Check
                if (transform.position == lastPos)
                {
                    foreach (var item in legsAnimators)
                    {
                        item.SetBool("Walk", false);
                    }

                }
                else
                {
                    foreach (var item in legsAnimators)
                    {
                        print(item.name);
                        item.SetBool("Walk", true);
                    }
                }

                #endregion

                #region Swapping Firing Point

                if (firingPointActive[0] != null && firingPointActive[0].activeInHierarchy) firingPointCurrent = firingPointActive[0];
                if (firingPointActive[1] != null && firingPointActive[1].activeInHierarchy) firingPointCurrent = firingPointActive[1];
                if (firingPointActive[2] != null && firingPointActive[2].activeInHierarchy) firingPointCurrent = firingPointActive[2];
                if (firingPointActive[3] != null && firingPointActive[3].activeInHierarchy) firingPointCurrent = firingPointActive[3];

                if (firingPointCurrent == null) firingPointCurrent = firingPointDefault;

                #endregion

                    #region Swapping Missy Sprites

                    //change for cardinal direction
                if (Input.GetKeyDown(KeyCode.W)) //FACING BACK
                {

                    currentHead = backHead;


                    StopAllAnimations();

                    if (lastDir == missyLeftSide)
                    {
                        leftPivot.transform.DORotate(new Vector3(0, 50, 0), 0.05f);

                    }
                    if (lastDir == missyRightSide) rightPivot.transform.DORotate(new Vector3(0, -50, 0), 0.05f);

                    if(lastDir == missyForward)
                    {
                        
                        missyBack.SetActive(true);
                        missyForward.SetActive(false);
                        missyLeftSide.SetActive(false);
                        missyRightSide.SetActive(false);
                        //change firing point


                    }


                    if (lastDir != missyBack && lastDir != missyForward)
                    {

                       

                        missyForward.SetActive(false);

                        ExecuteAfterFrames(4, () => missyLeftSide.SetActive(false));
                        ExecuteAfterFrames(4, () => missyRightSide.SetActive(false));
                        ExecuteAfterFrames(4, () => missyBack.SetActive(true));
                        ExecuteAfterFrames(1, () => leftPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
                        ExecuteAfterFrames(1, () => rightPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
                    }




                    lastDir = missyBack;

                }
                if (Input.GetKeyDown(KeyCode.A)) //FACE LEFT
                {
                    currentHead = sideLHead;


                    StopAllAnimations();

                    _AVTAR.slotsOnPlayerLeft[3].SetActive(false); //turn off left side

                    if(lastDir == missyForward)
                    {
                        frontPivot.transform.DORotate(new Vector3(0, 50, 0), 0.05f);

                    }
                    if( lastDir == missyBack) backPivot.transform.DORotate(new Vector3(0, -50, 0), 0.05f);

                    if(lastDir == missyRightSide)
                    {
                        missyLeftSide.SetActive(true);
                        missyRightSide.SetActive(false);
                        missyForward.SetActive(false);
                        missyBack.SetActive(false);


                    }
                    else
                    {
                        missyRightSide.SetActive(false);

                        //WAIT UNTIL TWEEN ANIMATION IS DONE BEFORE CHANGING
                        ExecuteAfterFrames(4, () => missyForward.SetActive(false));
                        ExecuteAfterFrames(4, () => missyLeftSide.SetActive(true));
                        ExecuteAfterFrames(4, () => missyBack.SetActive(false));

                        //RESET PIVOTS
                        ExecuteAfterFrames(1, () => frontPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
                        ExecuteAfterFrames(1, () => backPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));

                    }


                    lastDir = missyLeftSide;


                }
                if (Input.GetKeyDown(KeyCode.S)) //FACE FORWARD
                {
                    currentHead = frontHead;


                    StopAllAnimations();
                    //print("Just pressed S. lastDir was " + lastDir.name);
                    if (lastDir == missyLeftSide)
                    {
                        leftPivot.transform.DORotate(new Vector3(0, -50, 0), 0.05f);

                    }
                    if (lastDir == missyRightSide) rightPivot.transform.DORotate(new Vector3(0, 50, 0), 0.05f);

                    if(lastDir == missyBack)
                    {
                        missyForward.SetActive(true);
                        missyLeftSide.SetActive(false);
                        missyRightSide.SetActive(false);
                        missyBack.SetActive(false);




                    }
                    else if (lastDir != missyForward)
                    {
                        //print("delay");

                        ExecuteAfterFrames(4, () => missyForward.SetActive(true));  
                        ExecuteAfterFrames(4, () => missyLeftSide.SetActive(false));
                        ExecuteAfterFrames(4, () => missyRightSide.SetActive(false));
                        ExecuteAfterFrames(1, () => leftPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
                        ExecuteAfterFrames(1, () => rightPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
                        missyBack.SetActive(false);

                    }



                    lastDir = missyForward;
                }
                if (Input.GetKeyDown(KeyCode.D)) //FACE RIGHT
                {
                    currentHead = sideRHead;


                    StopAllAnimations();
                    _AVTAR.slotsOnPlayerRight[4].SetActive(false); //turn off right side


                    if (lastDir == missyForward)
                    {
                        frontPivot.transform.DORotate(new Vector3(0, -50, 0), 0.05f);

                    }
                    if (lastDir == missyBack)
                    {
                        backPivot.transform.DORotate(new Vector3(0, 50, 0), 0.05f);
                    }
                    

                    if(lastDir == missyLeftSide)
                    {
                        missyForward.SetActive(false);
                        missyRightSide.SetActive(true);
                        missyBack.SetActive(false);
                        missyLeftSide.SetActive(false);


                    }
                    else
                    {
                        ExecuteAfterFrames(4, () => missyForward.SetActive(false));

                        ExecuteAfterFrames(4, () => missyRightSide.SetActive(true));
                        ExecuteAfterFrames(4, () => missyBack.SetActive(false));

                        ExecuteAfterFrames(1, () => frontPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
                        ExecuteAfterFrames(1, () => backPivot.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
                        missyLeftSide.SetActive(false);

                    }


                    lastDir = missyRightSide;


                }

                #endregion
                #endregion

                #region Rotate Firing Point
                //Rotate melee hit box and head
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 targetPos = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);

                    directional.transform.LookAt(targetPos);
                    Mathf.Clamp(directional.transform.rotation.x, 0, 0);
                    Mathf.Clamp(directional.transform.rotation.z, 0, 0);
                }

                #endregion

                #region Attacks
                if (Input.GetMouseButton(0))
                {
                    for (int i = 0; playerInventory.Count > i; i++)
                    {
                        //check for primary
                        if (playerInventory[i].active)
                        {
                            //check if primary is projectile
                            if (playerInventory[i].projectile) //PROJECTILE ATTACKS------------------------------------------------------------------
                            {
                                //shoot


                                //changed to use player stats, the primary attack will just change

                                FireProjectile(playerInventory[i].projectilePF, projectileSpeed, projectileFirerate, projectileRange);
                                //ADD KNOCK BACK
                                if (knockbackActive)
                                {
                                    float timeSinceKnockback = Time.time - knockbackStartTime;

                                    if (timeSinceKnockback >= knockbackDuration)
                                    {
                                        knockbackActive = false;
                                    }
                                    else
                                    {
                                        float knockbackProgress = timeSinceKnockback / knockbackDuration;
                                        var dir = (-firingPointCurrent.transform.forward * knockbackAmount);
                                        dir = new Vector3(dir.x, 0, dir.z);
                                        controller.Move(dir * Time.deltaTime);

                                    }
                                }
                                

                            }
                            else //MELEE ATTACKS-------------------------------------------------------------------------------------------------
                            {
                                print("Attack with item in slot " + i + " which is " + playerInventory[i].itemName);

                                //update melee attack pattern
                                if (playerInventory[i].meleeAttackType == Item.AttackType.Cone) meleeHitBox = MeleeHitBox.Cone;
                                else if (playerInventory[i].meleeAttackType == Item.AttackType.Line) meleeHitBox = MeleeHitBox.Line;


                                MeleeAttack(meleeFirerate, i);

                                //MELEE ATTACK
                                if (meleeUI != null)
                                {
                                    print("MELEE ATTACK");
                                    meleeUI.gameObject.SetActive(true);

                                }
                            }
                        }
                    }

                }

                #region OLD VERSION WITH SEPERATE BUTTONS FOR MELEE AND LONG RANGE

                //if (Input.GetMouseButton(1))
                //{

                //    for (int i = 0; playerInventory.Count > i; i++)
                //    {
                //        //check for primary
                //        if (playerInventory[i].active)
                //        {
                //            //check if primary is projectile
                //            if (!playerInventory[i].projectile)
                //            {
                //                print("Attack with item in slot " + i + " which is " + playerInventory[i].itemName);

                //                //update melee attack pattern
                //                if (playerInventory[i].meleeAttackType == Item.MeleeAttackType.Cone) meleeHitBox = MeleeHitBox.Cone;
                //                else if (playerInventory[i].meleeAttackType == Item.MeleeAttackType.Line) meleeHitBox = MeleeHitBox.Line;


                //                MeleeAttack(meleeFirerate, i);

                //                //MELEE ATTACK
                //                if (meleeUI != null)
                //                {
                //                    print("MELEE ATTACK");
                //                    meleeUI.gameObject.SetActive(true);

                //                }

                //            }
                //        }
                //    }

                //}



                //if (Input.GetMouseButton(0))
                //{
                //    for (int i = 0; playerInventory.Count > i; i++)
                //    {
                //        //check for primary
                //        if (playerInventory[i].active)
                //        {
                //            //check if primary is projectile
                //            if (playerInventory[i].projectile)
                //            {
                //                //shoot


                //                //changed to use player stats, the primary attack will just change

                //                FireProjectile(playerInventory[i].projectilePF, projectileSpeed, projectileFirerate, projectileRange);
                //                //ADD KNOCK BACK
                //                if (knockbackActive)
                //                {
                //                    float timeSinceKnockback = Time.time - knockbackStartTime;

                //                    if (timeSinceKnockback >= knockbackDuration)
                //                    {
                //                        knockbackActive = false;
                //                    }
                //                    else
                //                    {
                //                        float knockbackProgress = timeSinceKnockback / knockbackDuration;
                //                        var dir = (-firingPoint.transform.forward * knockbackAmount);
                //                        dir = new Vector3(dir.x, 0, dir.z);
                //                        controller.Move(dir * Time.deltaTime);

                //                    }
                //                }

                //            }
                //        }
                //    }

                //}

                #endregion
                #endregion

                if (health <= 0)
                {
                    Die();
                    Debug.Log("Die");
                }

                break;

                
        }

        UIrangeIndicator.size = new Vector2(UIrangeIndicator.size.x, meleeRange);

        lastPos = transform.position;

        //change melee

        //change primary
        #region Primary Change Inputs

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePrimary(0);
            _HUD.isPrimary1 = true;
            _HUD.isPrimary2 = false;
            _HUD.isPrimary3 = false;
            _HUD.isPrimary4 = false;
            _HUD.isPrimary5 = false;
            _HUD.isPrimary6 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("Chaning primary");
            ChangePrimary(1);
            _HUD.isPrimary1 = false;
            _HUD.isPrimary2 = true;
            _HUD.isPrimary3 = false;
            _HUD.isPrimary4 = false;
            _HUD.isPrimary5 = false;
            _HUD.isPrimary6 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePrimary(2);
            _HUD.isPrimary1 = false;
            _HUD.isPrimary2 = false;
            _HUD.isPrimary3 = true;
            _HUD.isPrimary4 = false;
            _HUD.isPrimary5 = false;
            _HUD.isPrimary6 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangePrimary(3);
            _HUD.isPrimary1 = false;
            _HUD.isPrimary2 = false;
            _HUD.isPrimary3 = false;
            _HUD.isPrimary4 = true;
            _HUD.isPrimary5 = false;
            _HUD.isPrimary6 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangePrimary(4);
            _HUD.isPrimary1 = false;
            _HUD.isPrimary2 = false;
            _HUD.isPrimary3 = false;
            _HUD.isPrimary4 = false;
            _HUD.isPrimary5 = true;
            _HUD.isPrimary6 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangePrimary(5);
            _HUD.isPrimary1 = false;
            _HUD.isPrimary2 = false;
            _HUD.isPrimary3 = false;
            _HUD.isPrimary4 = false;
            _HUD.isPrimary5 = false;
            _HUD.isPrimary6 = true;
        }



        #endregion
    }

    void StopAllAnimations()
    {
        //foreach (var animator in itemsAnimForward)
        //{
        //    animator.StopPlayback();
        //}
        //foreach (var animator in itemsAnimBack)
        //{
        //    if(animator.)
        //    animator.StopPlayback();
        //}
        //foreach (var animator in itemsAnimLeftSide)
        //{
        //    animator.StopPlayback();
        //}
        //foreach (var animator in itemsAnimRightSide)
        //{
        //    animator.StopPlayback();
        //}
    }

    public void UpdateLegAnimators()
    {

        legsAnimators.Clear();

        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i].segment == Item.Segment.Legs)
            {
                legsAnimators.Add(itemsAnimBack[i]);
                legsAnimators.Add(itemsAnimForward[i]);
                legsAnimators.Add(itemsAnimLeftSide[i]);
                legsAnimators.Add(itemsAnimRightSide[i]);
            }
        }

    }

    void CheckForStartingItems()
    {
        foreach (var item in playerInventory)
        {
            if(item !=null)
            {
                print(item.itemName);
                int index = playerInventory.IndexOf(item);
                _ISitemD.AddItemStats(index);
                _UI.CreateItemSelected(item);
                item.active = true;

                //check if item is arleady in inventory
            }
        }

        _PIA.PassiveAbilityItemCheck();

    }

    private Tween TweenSpeed(float endValue,float time)
    {
        speedTween = DOTween.To(() => speed, (x) => speed = x, endValue, time);
        return speedTween;
    }
   
    void UpdateMelee()
    {
        switch (meleeHitBox)
        {
            case MeleeHitBox.Line:
                lineHitbox.SetActive(true);
                coneHitbox.SetActive(false);
                lineHitbox.transform.localScale = new Vector3(1, 1, projectileRange);
                break;
            case MeleeHitBox.Cone:
                lineHitbox.SetActive(false);
                coneHitbox.SetActive(true);
                coneHitbox.transform.localScale = new Vector3(1, 1, projectileRange);
                break;
        }
    }

    public void ChangePrimary(int _inventorySlot)
    {

        //turn off any others
        foreach (var item in playerInventory)
        {
            item.active = false;
        }


        //check if item is primary
        if (playerInventory[_inventorySlot].itemType == Item.ItemType.Primary)
        {
            //if yes activate
            playerInventory[_inventorySlot].active = true;


            //check if melee attack
            if(!playerInventory[_inventorySlot].projectile)
            {
                ////change melee UI
                meleeUISwitcher.SwitchMeleeUI(playerInventory[_inventorySlot].ID);
                //meleeUI.gameObject.SetActive(false);
                ////change ui scale
                //meleeUI.GetComponentInParent<Transform>().localScale = new Vector3(meleeRange, meleeRange, meleeRange);

                UIrangeIndicator.size = new Vector2(UIrangeIndicator.size.x, meleeRange);
            }
            else
            {
                FindCurrentFiringPoint(playerInventory[_inventorySlot]);
            }

            
        }
    }

    void MeleeAttack(float _firerate, int _index)
    {
        var inRangeEnemies = GetComponentInChildren<MeleeRangeCheck>().inRangeEnemies;

        if (!meleeCooDown)
        {
            if(!meleeAnimationCooldown)
            {
                meleeAnimationCooldown = true;
                //meleeUI.GetComponent<Animator>().SetTrigger("Attack");
                print("Attack count");
                //activate animation
                print("Anim slot " + _index);
                itemsAnimForward[_index].SetTrigger("Attack");

                if (itemsAnimBack[_index] != null) itemsAnimBack[_index].SetTrigger("Attack");


                itemsAnimLeftSide[_index].SetTrigger("Attack");
                itemsAnimRightSide[_index].SetTrigger("Attack");
                ExecuteAfterSeconds(_firerate, () => meleeAnimationCooldown = false);


                //if(meleeUI != null)
                //{


                //}

                if (inRangeEnemies.Count != 0)
                {

                    // add that enemey gets it, do with foreach in list, get enemy component then run hit script;
                    print("Enemies in range of melee attack" + inRangeEnemies[0]);

                    foreach (var enemy in inRangeEnemies)
                    {
                        if(enemy != null)
                        {
                            enemy.GetComponent<BaseEnemy>().Hit(dmg);

                            if (enemy.GetComponent<BaseEnemy>().stats.health < 0) inRangeEnemies.Remove(enemy);
                        }

                    }

                    print("melee attack");
                    meleeCooDown = true;
                    ExecuteAfterSeconds(_firerate, () => meleeCooDown = false);
                }
            }
        }
    }

    public void FireProjectile(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 flatAimTarget = screenPoint + cursorRay / Mathf.Abs(cursorRay.y) * Mathf.Abs(screenPoint.y - transform.position.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            firingPointCurrent.transform.LookAt(hit.point);

            if (!projectileShot)
            {
                print("Fire");
                //particle system
                FiringParticleSystem(_prefab);

                //Spawn bullet and apply force in the direction of the mouse
                //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
                GameObject bullet = Instantiate(_prefab, firingPointCurrent.transform.position, firingPointCurrent.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);

                bullet.GetComponent<ItemLook>().firingPoint = firingPointCurrent;

                knockbackActive = true;
                knockbackStartTime = Time.time;
                Mathf.Clamp(bullet.transform.position.y, 0, 0);

                //This will destroy bullet once it exits the range

                //if(Vector3.Distance(transform.position,bullet.transform.position) > projectileRange) Destroy(bullet);



                //Controls the firerate, player can shoot another bullet after a certain amount of time
                projectileShot = true;

                ExecuteAfterSeconds(_firerate, () => projectileShot = false);
            }
            print("FIRE PROJECTILE");

        }
    }

    void FindCurrentFiringPoint(Item _item)
    {
        //find slot
        var slot = _item.inSlot;
        print("In slot " + slot);
        //get firing point for each side
        //put into array

        firingPointActive[0] = _AVTAR.slotsOnPlayerFront[slot].transform.GetChild(0).gameObject.transform.Find("FiringPoint").gameObject;
        firingPointActive[1] = _AVTAR.slotsOnPlayerBack[slot].transform.GetChild(0).gameObject.transform.Find("FiringPoint").gameObject;
        firingPointActive[2] = _AVTAR.slotsOnPlayerLeft [slot].transform.GetChild(0).gameObject.transform.Find("FiringPoint").gameObject;
        firingPointActive[3] = _AVTAR.slotsOnPlayerRight [slot].transform.GetChild(0).gameObject.transform.Find("FiringPoint").gameObject;





    }

    void FiringParticleSystem(GameObject _projectile)
    {
        if(_projectile.name != "Solar_Laser_Projectile")
        {
            spitParticleSystem.Play();

        }
    }

    void HeadBobble()
    {
        currentHead.transform.DOLocalRotate(new Vector3(0f, 0f, -rotateAmount),time).SetEase(ease);
        ExecuteAfterSeconds(time, () => currentHead.transform.DOLocalRotate(new Vector3(0f, 0f, rotateAmount), time).SetEase(ease));
        ExecuteAfterSeconds(time*2, () => currentHead.transform.DOLocalRotate(new Vector3(0f, 0f, 0), time).SetEase(ease));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {


            if (_PIA.ramming)
            {
                collision.gameObject.GetComponent<BaseEnemy>().ApplyStun(_PIA.ramHornsStunDuration);
                collision.gameObject.GetComponent<BaseEnemy>().Hit(_PIA.ramHornsDamage);

            }
            if(_PIA.gutpunch)
            {
                print("GUT PUNCHED");
                collision.gameObject.GetComponent<BaseEnemy>().Hit(_PIA.humanFistDamage);
            }
        }
        
    }

    public void Hit(float _dmg)
    {
        if(!immortal)
        {
            
            print("player has been hit");
            health -= _dmg;
            //_PE.ChromaticABFade();

            _AM.PlayerHurt();

            if (health > 0)
            {
                _PE.VignetteFade();
                HeadBobble();
                _UI.UpdateHealthText(health);
            }
            else
            {
                Die();
            }
        }
        

    }

    public void Die()
    {
        _AM.PlayerDeathScream();

        _GM.gameState = GameManager.GameState.Dead;
        DieAnimation();
        print("I Am DEAD");

        ExecuteAfterSeconds(0.5f, () => playerAvatar.SetActive(false));
        ExecuteAfterSeconds(1, () => _GM.GameOver());

    }

    void DieAnimation()
    {

        missyRightSideAnim.SetBool("Walking", false);
        missyLeftSideAnim.SetBool("Walking", false);
        missyFrontSideAnim.SetBool("Walking", false);
        missyBackSideAnim.SetBool("Walking", false);

        missyRightSideAnim.SetTrigger("Death 0");
        missyLeftSideAnim.SetTrigger("Death 0");
        missyFrontSideAnim.SetTrigger("Death 0");
        missyBackSideAnim.SetTrigger("Death 0");
    }


}


