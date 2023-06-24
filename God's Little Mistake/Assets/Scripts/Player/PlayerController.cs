using UnityEngine;
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

    //[SerializeField]
    


    public Attack spitAtk;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();


    }



    void Update()
    {



        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        controller.Move(playerVelocity * Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycasthit))
        {
           var mousePos = raycasthit.point;
           firingPoint.transform.LookAt(mousePos * Time.deltaTime * 10);
        }


        

        #region Attacks



        if (Input.GetButton("Fire1"))
        {

            //SPIT
            FireProjectile(_AD.attacks[0].projectilePF, _AD.attacks[0].projectileSpeed, _AD.attacks[0].fireRate,_AD.attacks[0].range);




        }

        #endregion

    }

    void FireProjectile(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {

        //Vector2 screenMousePos = Input.mousePosition;
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(screenMousePos.x, screenMousePos.y, 1));

        //GameObject bullet = Instantiate(_prefab, firingPoint.transform.position, Quaternion.identity);

        //if(!projectileShot)
        //{
        //    Vector3 bulletDirection = new Vector3(mousePos.x, mousePos.y, 1) - transform.position;
        //    bullet.GetComponent<Rigidbody>().velocity += _projectileSpeed * bulletDirection;
        //    projectileShot = true;
        //    ExecuteAfterSeconds(_firerate, () => projectileShot = false);
        //}




        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (!projectileShot)
            {
                Vector3 target = hit.point;
                target = new Vector3(target.x, 0.5f, target.z);


                //spawn bullet
                GameObject bullet = Instantiate(_prefab, firingPoint.transform.position, firingPoint.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);
                Destroy(bullet, _range);

                //shoot direction   

                projectileShot = true;
                ExecuteAfterSeconds(_firerate, () => projectileShot = false);



            }
        }
    }


}


