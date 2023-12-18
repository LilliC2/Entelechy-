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

    [Header("Inventory")]
    public Item headItem;
    public Item torsoItem;
    public Item legItem;

    public GameObject headFiringPoint;
    public GameObject headFiringPointDefault;
    public GameObject torsoFiringPoint;

    [Header("Sprites")] //this will be changed to prefabs when they are animated
    public SpriteRenderer headSprite;
    public SpriteRenderer torsoSprite;
    public SpriteRenderer legSprite;

    [Header("Movement")]
    public bool isMoving;
    public bool enableMovement = true;
    public Vector3 move;
    public float speed;
    public float maxSpeed;
    public GameObject directional; //is for current melee attack and will probably be removed


    [SerializeField]
    int initalSpeedBoost = 3;
    GameObject lastDir;
    Tween speedTween;

    [Header("Temporary")]
    public SpriteRenderer UIrangeIndicator;

    [Header("Animation")]
    GameObject playerAvatar;
    Vector3 currentPos;
    Vector3 lastPos;
    Animator legs;


    [Header("Knockback")]
    [SerializeField]
    float knockbackAmount;
    bool knockbackActive;
    public float knockbackDuration = 0.5f;
    private float knockbackStartTime;

    [Header("Physics")]
    public float gravity = 9.8f;
    public Vector3 lastPosOnGround;
    public GameObject groundCheck;
    public bool isGrounded;
    public LayerMask groundMask;
    public GameObject tileLastStoodOn;
    public bool fallDamage;

    [Header("Player Stats")]
    public float health;
    public float maxHP;
 


  
    public float dashAmount;

    //audio
    bool playedDeathSound = false;

    private void Start()
    {
        health = maxHP;
        controller = gameObject.GetComponent<CharacterController>();
        _UI.UpdateHealthText(health);
        _UI.UpdateHealthBar(health, maxHP);
        lastPos = transform.position;

        groundCheck = GameObject.Find("GroundCheck");

        CheckForStartingItems();

        legs = _EI.LegAvatar.transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        //RotateHead();
        if (health <= 0)
        {
            Die();
            Debug.Log("Die");
        }


        _UI.UpdateHealthText(health);
        _UI.UpdateHealthBar(health, maxHP);

        switch (_GM.gameState)
        {
            case GameManager.GameState.Playing:

                #region Movement

                if(legs!=null)
                {
                    if (controller.velocity.magnitude > 1f) legs.SetBool("Walking", true);
                    else legs.SetBool("Walking", false);
                }



                if (enableMovement)
                {
                    move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    if (!canFloat)
                    {
                        move.y -= gravity;
                    }

                    controller.Move(move * Time.deltaTime * legItem.movementSpeed);


                    #region isMoving Check

                    if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) isMoving = false;

                    //find inpus for movement
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        if (!isMoving)
                        {
                            speed = speed + initalSpeedBoost;

                            ExecuteAfterSeconds(0.1f, () => TweenSpeed(maxSpeed, 1));
                        }

                    }
                    else 

                    if (!isMoving)
                    {
                        speed = maxSpeed;
                    }


                }
                else
                {
                    move = new Vector3(0, 0, 0);
                    move.y -= gravity;

                    controller.Move(move * Time.deltaTime * legItem.movementSpeed);

                }

                isGrounded = Physics.CheckSphere(groundCheck.transform.position, 1, groundMask);


                #endregion

                #region Respawn If Fell Off

                //grounded check

                if (isGrounded)
                {
                    lastPosOnGround = lastPos;
                    var col = Physics.OverlapSphere(groundCheck.transform.position, 0.5f, groundMask);
                    if (col.Length != 0) tileLastStoodOn = col[0].gameObject;
                    fallDamage = true;
                    enableMovement = true;


                }
                else
                {
                    if (transform.position.y < -3)
                    {
                        enableMovement = false;
                        //remove control

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
                            ExecuteAfterSeconds(1.3f, () => _PE.landingPS.Play());
                            ExecuteAfterSeconds(1.3f, () => _AM.playerThud.Play());
                            ExecuteAfterSeconds(1.5f, () => RespawnAfterFallingCheck(targetPos));




                        }

                    }

                }

                //while grounded lastPosGrounded = lastPos;

                //if not grounded execute after 1 second and respawn them

                //for some pizazz could have them fly back in from the roof


                #endregion

                #endregion

                #region Animation

                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");


                if ((horizontal < 0 || horizontal > 0) || (vertical < 0 || vertical > 0)) isMoving = true;

                #endregion

                #region Rotate Firing Point
                //Rotate melee hit box and head
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 targetPos = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);

                    directional.transform.LookAt(targetPos);
                    //Mathf.Clamp(directional.transform.rotation.x, 0, 0);
                    //Mathf.Clamp(directional.transform.rotation.z, 0, 0);
                }

                #endregion

                #region Attacks

                if (Input.GetButton("Fire1"))
                {
                    if (headItem.itemName != "NULL")
                    {
                        //call appriopriate attack from attack script
                        _PAtk.CallAttack(headItem);              
                    }
                    //if no head item, torso attack is also bound to m0
                    if (headItem.itemName == "NULL" && torsoItem.itemName != "NULL")
                    {

                        _PAtk.CallAttack(torsoItem);
                    }
                }

                if (Input.GetButton("Fire2"))
                {
                    if (torsoItem.itemName != "NULL")
                    {
                        //call appriopriate attack from attack script
                        _PAtk.CallAttack(torsoItem);
                    }
                    //if no head item, torso attack is also bound to m0
                    if (torsoItem.itemName == "NULL" && headItem.itemName != "NULL")
                    {

                        _PAtk.CallAttack(headItem);
                    }


                }
                #endregion

                if (health <= 0)
                {
                    Die();
                    Debug.Log("Die");
                }

                lastPos = transform.position;

                #region Ability

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _PAbl.CallAbility(legItem);
                }

                #endregion

                break;




        }


    }

        



   

    void RespawnAfterFallingCheck(Vector3 _targetPos)
    {
        if (!fallDamage && !isGrounded)
        {
            transform.position = _targetPos;
            ExecuteAfterSeconds(1.3f, () => _PE.landingPS.Play());
            ExecuteAfterSeconds(1.3f, () => _AM.playerThud.Play());

        }
    }


    void CheckForStartingItems()
    {

        if(headItem.itemName != "NULL")
        {
            print("Head item is: " + headItem.itemName);

            _UI.CreateItemSelected(headItem);
            headItem.active = true;
        }
        if(torsoItem.itemName != "NULL")
        {
            print("Torso item is: " + torsoItem.itemName);


            _UI.CreateItemSelected(torsoItem);
            torsoItem.active = true;
        }
        if(legItem.itemName != "NULL")
        {
            print("Leg item is: " + legItem.itemName);

            _UI.CreateItemSelected(legItem);
            legItem.active = true;
        }

        //_PIA.PassiveAbilityItemCheck();

    }

    private Tween TweenSpeed(float endValue,float time)
    {
        speedTween = DOTween.To(() => speed, (x) => speed = x, endValue, time);
        return speedTween;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {


            if (_PIA.ramming)
            {

                _AM.RamHit();
                print("Ramming hit someone");
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
            //_PE.ChromaticABFade();
            health -= _dmg;
            _AM.PlayerHurt();
            _UI.heathAnim.SetTrigger("Damage");

            if (health > 0)
            {
                _PE.missyHitParticle.Play();
                _PE.RedVignetteFade();
                //HeadBobble();
                _UI.UpdateHealthText(health);
                _UI.UpdateHealthBar(health, maxHP);
            }
            else
            {
                Die();
            }
        }
        

    }

    public void Die()
    {
        if(!playedDeathSound)
        {
            playedDeathSound = true;
            _AM.PlayerDeathScream();

        }


        _GM.gameState = GameManager.GameState.Dead;
        print("I Am DEAD");

        //ExecuteAfterSeconds(0.5f, () => playerAvatar.SetActive(false));
        //ExecuteAfterSeconds(4, () => _GM.GameOver());

    }

        
}


