using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerController : Singleton<PlayerController>
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    [Header ("Player Stats")]
    public float health;
    public float speed;
    public float dmg;
    public float dps;
    public float range;
    public float firerate;

    public bool projectile;
    public float projectileSpeed;
    public GameObject projectilePF;

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


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        _UI.UpdateHealthText(health);

    }

    void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            _ISitemD.RemoveItemFromInventory(2);    
        }


        switch(_GM.gameState)
        {
            case GameManager.GameState.Playing:

                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * speed);

                controller.Move(playerVelocity * Time.deltaTime);

                //Rotate melee hit box and head
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    directional.transform.LookAt(hit.point);
                    Mathf.Clamp(directional.transform.rotation.x, 0, 0);
                    Mathf.Clamp(directional.transform.rotation.z, 0, 0);
                }


                #region Attacks


                if (Input.GetKey(KeyCode.Space))
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

                                //THIS WILL BE REWRITTEN WHEN INVENTORY IS IMPLEMENTED
                                //changed to use player stats, the primary attack will just change
                                MeleeAttack();
                            }
                        }
                    }
                    
                }


                if (Input.GetButton("Fire1"))
                {
                    for (int i = 0; playerInventory.Count > i; i++)
                    {
                        //check for primary
                        if(playerInventory[i].active)
                        {
                            //check if primary is projectile
                            if(playerInventory[i].projectile)
                            {
                                //shoot

                                //THIS WILL BE REWRITTEN WHEN INVENTORY IS IMPLEMENTED
                                //changed to use player stats, the primary attack will just change
                                FireProjectile(playerInventory[0].projectilePF, projectileSpeed, firerate, range);
                            }
                        }
                    }

                }

                #endregion

                break;

        }


        //change primary
        #region Primary Change Inputs

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePrimary(0);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePrimary(1);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePrimary(2);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangePrimary(3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangePrimary(4);
        }

        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangePrimary(5);
        }



        #endregion
    }

    void ChangePrimary(int _inventorySlot)
    {
        //check if item is primary
        if (playerInventory[_inventorySlot].itemType == Item.ItemType.Primary)
        {
            //if yes activate
            playerInventory[_inventorySlot].active = true;

            //turn off any others in the same segment
            for (int i = _inventorySlot; playerInventory.Count > i; i++)
            {
                if (i != _inventorySlot)
                {
                    if (playerInventory[i].segment == playerInventory[_inventorySlot].segment)
                    {
                        if (playerInventory[i].active == true) playerInventory[i].active = false;
                    }
                }
            }
        }
    }

    void MeleeAttack()
    {
        var inRangeEnemies = GetComponentInChildren<MeleeRangeCheck>().inRangeEnemies;

        print(inRangeEnemies[0]);

        //add that enemey gets it, do with foreach in list, get enemy component then run hit script;
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

    void Hit(float _dmg)
    {
        print("player has been hit");
        health -= _dmg;
        if(health >0)
        {
            _UI.UpdateHealthText(health);
        }
        else
        {
            //GAME OVER
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("EnemyProjectile"))
        {
            print("player has been hit in collision");

            Destroy(collision.gameObject);
            //get dmg from enemy
            //Hit(collision.collider.gameObject.GetComponent<BaseEnemy>().stats.dmg);
        }
    }
}


