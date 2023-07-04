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

    }

    void Update()
    {
        //for testing
        if(Input.GetKeyDown(KeyCode.K))
        {
            _ISitemD.RemoveItemFromInventory(0);    
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
                    MeleeAttack();
                }

                if (Input.GetButton("Fire1"))
                {
                    //SPIT

                    //THIS WILL BE REWRITTEN WHEN INVENTORY IS IMPLEMENTED
                    FireProjectile(playerInventory[0].projectilePF, playerInventory[0].projectileSpeed, playerInventory[0].fireRate, playerInventory[0].range);


                }

                #endregion

                break;

            case GameManager.GameState.Iventory:
                break;
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

    public void AddItemStatsToPlayer(int _inventoryID)
    {
        dmg += playerInventory[_inventoryID].dmg;
        dps += playerInventory[_inventoryID].dps;
        range += playerInventory[_inventoryID].range;

    }
    
    public void RemoveItemStatsToPlayer(int _inventoryID)
    {
        dmg -= playerInventory[_inventoryID].dmg;
        dps -= playerInventory[_inventoryID].dps;
        range -= playerInventory[_inventoryID].range;

    }

}


