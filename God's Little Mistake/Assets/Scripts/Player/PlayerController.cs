using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerController : Singleton<PlayerController>
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public BearTrap bearTrap;
    public CactusTrap cactusTrap;

    #region Animation Variables
    [Header("Animation")]
    public Animator baseAnimator;
    public Animator nubsAnimator;
    public GameObject nubsOB;

    public Animator[] slotsAnim;
    public GameObject[] slotsGO;

    public GameObject torsoForward;
    public GameObject bellyForward;

    public GameObject missyLeftSide;
    public GameObject missyRightSide;
    public GameObject missyBack;
    public GameObject missyForward;

    public GameObject meleeUI;

    public List<Animator> itemsAnimForward;
    public List<Animator> itemsAnimRightSide;
    public List<Animator> itemsAnimLeftSide;
    public List<Animator> itemsAnimBack;


    public List<Animator> legsAnimators;

    Vector3 currentPos;
    Vector3 lastPos;
    #endregion

    #region Player Variables
    [Header("Player Stats")]
    public float health;
    public float maxHP;
    public float speed;
    public float maxSpeed;
    public float dmg;
    public float dps;
    public float range;
    public float firerate;

    public bool projectile;
    public float projectileSpeed;
    public GameObject projectilePF;


    [SerializeField]
    int initalSpeedBoost = 3;
    bool isMoving;

    Tween speedTween;

    #endregion
    
    [Header("Inventory")]
    public List<Item> playerInventory;

    [Header("Head Movement")]
    public float headSpeed = 1000;
    public GameObject head;

    [Header("Projectile")]
    public GameObject firingPoint;
    public GameObject directional; //is for current melee attack and will probably be removed
    public Vector3 target;
    bool projectileShot;

    [Header("Melee")]
    bool meleeCooDown;
    public GameObject lineHitbox;
    public GameObject coneHitbox;
    MeleeUISwitcher meleeUISwitcher;
    public enum MeleeHitBox { Line, Cone }
    public MeleeHitBox meleeHitBox;

    private void Start()
    {
        health = maxHP;
        controller = gameObject.GetComponent<CharacterController>();
        _UI.UpdateHealthText(health);
        lastPos = transform.position;

        meleeUISwitcher = GetComponent<MeleeUISwitcher>();

        CheckForStartingItems();

        //add stats for the 1 item in the inventory
        //_ISitemD.AddPassiveItem(0);
    }

    void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            _ISitemD.RemoveItemFromInventory(2);
        }


        UpdateMelee();
        _UI.UpdateHealthText(health);

        switch (_GM.gameState)
        {
            case GameManager.GameState.Playing:

                #region Movement
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * speed);

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

                #endregion

                //controller.Move(playerVelocity * Time.deltaTime);

                #region Animation

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
                        item.SetBool("Walk", true);
                    }
                }

                #endregion

                #region Swapping Missy Sprites

                //change for cardinal direction
                if (Input.GetKeyDown(KeyCode.W))
                {
                    missyForward.SetActive(false);
                    missyLeftSide.SetActive(false);
                    missyRightSide.SetActive(false);
                    missyBack.SetActive(true);

                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    _AVTAR.slotsOnPlayerLeft[3].SetActive(false); //turn off left side

                    missyForward.SetActive(false);
                    missyLeftSide.SetActive(true);
                    missyRightSide.SetActive(false);
                    missyBack.SetActive(false);

                }
                if (Input.GetKeyDown(KeyCode.S))
                {


                    missyForward.SetActive(true);

                    missyLeftSide.SetActive(false);
                    missyRightSide.SetActive(false);
                    missyBack.SetActive(false);

                }
                if (Input.GetKeyDown(KeyCode.D))
                {

                    _AVTAR.slotsOnPlayerRight[4].SetActive(false); //turn off right side
                    missyForward.SetActive(false);

                    missyLeftSide.SetActive(false);
                    missyRightSide.SetActive(true);
                    missyBack.SetActive(false);

                }


                #endregion
                #endregion

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


                #region Attacks


                if (Input.GetMouseButton(1))
                {

                    for (int i = 0; playerInventory.Count > i; i++)
                    {
                        //check for primary
                        if (playerInventory[i].active)
                        {
                            //check if primary is projectile
                            if (!playerInventory[i].projectile)
                            {
                                //shoot
                                if (meleeUI != null)
                                {
                                    meleeUI.gameObject.SetActive(true);

                                    meleeUI.GetComponent<Animator>().SetTrigger("Attack");
                                    ExecuteAfterSeconds(1, () => meleeUI.gameObject.SetActive(false));
                                }

                                

                                //active primary attack

                                itemsAnimForward[i].SetTrigger("Attack");
                                itemsAnimBack[i].SetTrigger("Attack");
                                itemsAnimLeftSide[i].SetTrigger("Attack");
                                itemsAnimRightSide[i].SetTrigger("Attack");



                                MeleeAttack(firerate);
                            }
                        }
                    }

                }
             

                if (Input.GetMouseButton(0))
                {
                    for (int i = 0; playerInventory.Count > i; i++)
                    {
                        //check for primary
                        if (playerInventory[i].active)
                        {
                            //check if primary is projectile
                            if (playerInventory[i].projectile)
                            {
                                //shoot

                                

                                //activate animation
                                //itemsAnimForward[i].SetTrigger("Attack");
                                //itemsAnimBack[i].SetTrigger("Attack");
                                //itemsAnimLeftSide[i].SetTrigger("Attack");
                                //itemsAnimRightSide[i].SetTrigger("Attack");

                                //changed to use player stats, the primary attack will just change
                                FireProjectile(playerInventory[0].projectilePF, projectileSpeed, firerate, range);

                                //ADD KNOCK BACK

                            }
                        }
                    }

                }

                #endregion
 

                break;

                
        }

        lastPos = transform.position;

        //change melee

        //change primary
        #region Primary Change Inputs

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePrimary(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("Chaning primary");
            ChangePrimary(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePrimary(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangePrimary(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangePrimary(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangePrimary(5);
        }



        #endregion
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
                _UI.CreateItemSelected(item);

                //check if item is arleady in inventory
            }
        }
    }

    private Tween TweenSpeed(float endValue,float time)
    {
        speedTween = DOTween.To(() => speed, (x) => speed = x, endValue, time);
        return speedTween;
    }
   
    //public void CloseSlots()
    //{
    //    for (int i = 0; i < slotsAnim.Length; i++)
    //    {
    //        if (_AVTAR.slotsOnPlayerFront[i].transform.childCount == 0)
    //        {
    //            print("trying to slot");
    //            slotsAnim[i].SetBool("OpenSlot", false);
    //            slotsAnim[i].SetBool("CloseSlot", true);

    //        }



    //        ExecuteAfterSeconds(1, () => ChangeSlots(false));
    //    }
    //}

    //void ChangeSlots(bool _bool)
    //{
    //    for (int i = 0; i < slotsAnim.Length; i++)
    //    {
    //        slotsGO[i].SetActive(_bool);
    //    }
    //}
    void UpdateMelee()
    {
        switch (meleeHitBox)
        {
            case MeleeHitBox.Line:
                lineHitbox.SetActive(true);
                coneHitbox.SetActive(false);
                lineHitbox.transform.localScale = new Vector3(1, 1, range);
                break;
            case MeleeHitBox.Cone:
                lineHitbox.SetActive(false);
                coneHitbox.SetActive(true);
                coneHitbox.transform.localScale = new Vector3(1, 1, range);
                break;
        }
    }

    public void ChangePrimary(int _inventorySlot)
    {

        //turn off any others in the same segment
        foreach (var item in playerInventory)
        {
            if(item.segment == playerInventory[_inventorySlot].segment)
            {
                item.active = false;
            }
        }

        //check if item is primary
        if (playerInventory[_inventorySlot].itemType == Item.ItemType.Primary)
        {
            //if yes activate
            playerInventory[_inventorySlot].active = true;


            //check if melee attack
            if(!playerInventory[_inventorySlot].projectile)
            {
                //change melee UI
                meleeUISwitcher.SwitchMeleeUI(playerInventory[_inventorySlot].ID);
                meleeUI.gameObject.SetActive(false);
            }

            
        }
    }

    void MeleeAttack(float _firerate)
    {
        var inRangeEnemies = GetComponentInChildren<MeleeRangeCheck>().inRangeEnemies;

        if (!meleeCooDown)
        {
            if (inRangeEnemies.Count != 0)
            {
                // add that enemey gets it, do with foreach in list, get enemy component then run hit script;
                print(inRangeEnemies[0]);

                foreach (var enemy in inRangeEnemies)
                {
                    enemy.GetComponent<BaseEnemy>().Hit();
                    if (enemy.GetComponent<BaseEnemy>().stats.health < 0) inRangeEnemies.Remove(enemy);

                }

                print("melee attack");
                meleeCooDown = true;
                ExecuteAfterSeconds(_firerate, () => meleeCooDown = false);
            }

        }

    }

    void FireProjectile(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 flatAimTarget = screenPoint + cursorRay / Mathf.Abs(cursorRay.y) * Mathf.Abs(screenPoint.y - transform.position.y);



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            firingPoint.transform.LookAt(hit.point);

            if (!projectileShot)
            {


                //Spawn bullet and apply force in the direction of the mouse
                //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
                GameObject bullet = Instantiate(_prefab, firingPoint.transform.position, firingPoint.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);

                Mathf.Clamp(bullet.transform.position.y, 0, 0);

                //This will destroy bullet once it exits the range, aka after a certain amount of time
                Destroy(bullet, _range);

                //Controls the firerate, player can shoot another bullet after a certain amount of time
                projectileShot = true;
                ExecuteAfterSeconds(_firerate, () => projectileShot = false);
            }
        }
    }

    public void Hit(float _dmg)
    {
        print("player has been hit");
        health -= _dmg;
        if (health > 0)
        {
            _UI.UpdateHealthText(health);
        }
        else
        {
            _GM.gameState = GameManager.GameState.Dead;
            DieAnimation();
            //add particles in die animation too
        }

    }

    void DieAnimation()
    {
        baseAnimator.SetBool("ForwardWalk", false);
        baseAnimator.SetBool("SideWalk", false);
        nubsAnimator.SetBool("ForwardWalk", false);
        nubsAnimator.SetBool("SideWalk", false);
        baseAnimator.SetTrigger("DeathTrigger");
    }


}


