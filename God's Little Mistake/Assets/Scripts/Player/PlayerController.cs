using UnityEngine;
using DG.Tweening;
public class PlayerController : Singleton<PlayerController>
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed = 2.0f;

    public GameObject head;

    public Vector3 target;

    public float headSpeed = 1000;


    bool projectileShot;

    public GameObject firingPoint;
    public GameObject directional;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

    }

    void Update()
    {


        switch(_GM.gameState)
        {
            case GameManager.GameState.Playing:

                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * playerSpeed);

                controller.Move(playerVelocity * Time.deltaTime);

                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if(Physics.Raycast(ray,out RaycastHit raycasthit))
                //{
                //    var mousePos = raycasthit.point;
                //    print(mousePos);
                //    firingPoint.transform.LookAt(mousePos * Time.deltaTime * 10,Vector3.up);    
                //    directional.transform.LookAt(mousePos * Time.deltaTime * 1);
                //    Mathf.Clamp(directional.transform.rotation.x, 0, 0);
                //    Mathf.Clamp(directional.transform.rotation.z, 0, 0);

                //}

                #region Attacks


                if (Input.GetKey(KeyCode.Space))
                {
                    MeleeAttack();
                }

                if (Input.GetButton("Fire1"))
                {
                    //SPIT
                    FireProjectile(_AD.attacks[0].projectilePF, _AD.attacks[0].projectileSpeed, _AD.attacks[0].fireRate, _AD.attacks[0].range);

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


}


