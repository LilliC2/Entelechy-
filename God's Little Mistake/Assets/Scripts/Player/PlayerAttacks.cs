using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttacks : Singleton<PlayerAttacks>
{


    [Header("Lob Projectile")]
    [SerializeField]
    float angle;
    [SerializeField]
    float power;

    [Header("Projectile")]
    public Vector3 target;

    bool projectileShot;
    bool projectileShot2;
    bool projectileShot3;


    [Header("Sabertooth Projectile")]
    public bool returned = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallAttack(Item _item)
    {
        switch (_item.ID)
        {
            case 0: //Pea Shooter

                PeaShooterAttack();

                break;
            
            case 2: //Sabertooth

                print("Saberooth");

                SabertoothAttack();

                break;
                
            case 3: //Sabertooth


                BasicLobProjectile(_IM.itemDataBase[3].projectileRange, _IM.itemDataBase[3].projectileSpeed, _IM.itemDataBase[3].projectilePF, _IM.itemDataBase[3].firerate);

                break;

            case 4: //Squito

                print("Lob");

                SquitoAttack();

                break;
                
            case 7: //Rocket Launcher Eyeball

                print("Rocket");

                RocketAttack();

                break;

            case 8: //LMG Porcupine

                print("LMG");

                LMGAttack(_IM.itemDataBase[8].projectilePF, _IM.itemDataBase[8].projectileSpeed, _IM.itemDataBase[8].firerate, _IM.itemDataBase[8].projectileRange);

                break;
            
            case 9: //Shotgun

                print("Shotgun");

                ShotgunAttack(_IM.itemDataBase[9].projectilePF, _IM.itemDataBase[9].projectileSpeed, _IM.itemDataBase[9].firerate, _IM.itemDataBase[9].projectileRange);

                break;



        }

    }

    public void LMGAttack(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 flatAimTarget = screenPoint + cursorRay / Mathf.Abs(cursorRay.y) * Mathf.Abs(screenPoint.y - transform.position.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _PC.headFiringPoint.transform.LookAt(hit.point);

            if (!projectileShot3)
            {
                print("Fire");
                //particle system

                //Spawn bullet and apply force in the direction of the mouse
                //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
                GameObject bullet1 = Instantiate(_prefab, _PC.headFiringPoint.transform.position, _PC.headFiringPoint.transform.rotation);
                GameObject bullet2 = Instantiate(_prefab, _PC.headFiringPoint.transform.position, _PC.headFiringPoint.transform.rotation);
                GameObject bullet3 = Instantiate(_prefab, _PC.headFiringPoint.transform.position, _PC.headFiringPoint.transform.rotation);

                bullet1.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);
                bullet2.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * _projectileSpeed);
                bullet3.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * _projectileSpeed);

                //bullet.GetComponent<ItemLook>().firingPoint = _PC.headFiringPoint;
                bullet1.GetComponent<RangeDetector>().range = _range;
                bullet1.GetComponent<RangeDetector>().positionShotFrom = _PC.transform.position;
                
                bullet2.GetComponent<RangeDetector>().range = _range;
                bullet2.GetComponent<RangeDetector>().positionShotFrom = _PC.transform.position;
                                
                bullet3.GetComponent<RangeDetector>().range = _range;
                bullet3.GetComponent<RangeDetector>().positionShotFrom = _PC.transform.position;


                //knockbackActive = true;
                //knockbackStartTime = Time.time;
                Mathf.Clamp(bullet1.transform.position.y, 0, 0);
                Mathf.Clamp(bullet2.transform.position.y, 0, 0);
                Mathf.Clamp(bullet3.transform.position.y, 0, 0);



                //Controls the firerate, player can shoot another bullet after a certain amount of time
                projectileShot3 = true;

                ExecuteAfterSeconds(_firerate, () => projectileShot3 = false);
            }
            print("FIRE PROJECTILE");

        }
    }

    public void ShotgunAttack(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 flatAimTarget = screenPoint + cursorRay / Mathf.Abs(cursorRay.y) * Mathf.Abs(screenPoint.y - transform.position.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _PC.headFiringPoint.transform.LookAt(hit.point);

            if (!projectileShot3)
            {
                print("Fire");
                //particle system

                //Spawn bullet and apply force in the direction of the mouse
                //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
                List<GameObject> bulletInstances = new List<GameObject>();

                for (int i = 0; i < 10; i++)
                {
                    GameObject bullet = Instantiate(_prefab, _PC.headFiringPoint.transform.position, _PC.headFiringPoint.transform.rotation);
                    //bullet1.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Vector3.forward.x + Random.Range(-2,2),Vector3.forward.y, Vector3.forward.z) * _projectileSpeed);
                    //bullet1.GetComponent<RangeDetector>().range = _range;
                    //Mathf.Clamp(bullet1.transform.position.y, 0, 0);

                    bulletInstances.Add(bullet);
                    print("Make bullet");
                }

                foreach (var bullet in bulletInstances)
                {
                    bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Vector3.forward.x + Random.Range(-1f, 1f), Vector3.forward.y, Vector3.forward.z) * Random.Range(_projectileSpeed-1f, _projectileSpeed));
                    bullet.GetComponent<RangeDetector>().range = _range;
                    bullet.GetComponent<RangeDetector>().positionShotFrom = _PC.torsoFiringPoint.transform.position;
                    Mathf.Clamp(bullet.transform.position.y, 0, 0);
                }
                //Controls the firerate, player can shoot another bullet after a certain amount of time
                projectileShot3 = true;

                ExecuteAfterSeconds(_firerate, () => projectileShot3 = false);
            }
            print("FIRE PROJECTILE");

        }
    }


    public void RocketAttack()
    {
        BasicFireProjectileTorso(_IM.itemDataBase[7].projectilePF, _IM.itemDataBase[7].projectileSpeed, _IM.itemDataBase[7].firerate, _IM.itemDataBase[7].projectileRange);

    }

    public void PeaShooterAttack()
    {
        _PE.peaShooterPS.Play();
        BasicFireProjectileHead(_IM.itemDataBase[0].projectilePF, _IM.itemDataBase[0].projectileSpeed, _IM.itemDataBase[0].firerate, _IM.itemDataBase[0].projectileRange);
        if (_FDM.leftFireFilling == false)
        {
            _FDM.leftHasFired = true;
            _FDM.leftFireCurrent = 0;
            _FDM.leftFireTotal = _IM.itemDataBase[0].firerate;
        }

    }
    
    public void SquitoAttack()
    {
        BasicFireProjectileHead(_IM.itemDataBase[4].projectilePF, _IM.itemDataBase[4].projectileSpeed, _IM.itemDataBase[4].firerate, _IM.itemDataBase[4].projectileRange);
        if (_FDM.rightFireFilling == false)
        {
            _FDM.rightHasFired = true;
            _FDM.rightFireCurrent = 0;
            _FDM.rightFireTotal = _IM.itemDataBase[4].firerate;
        }
    }




    public void SabertoothAttack()
    {
        if(returned)
        {
            returned = false;
            BasicFireProjectileHead(_IM.itemDataBase[2].projectilePF, _IM.itemDataBase[2].projectileSpeed, _IM.itemDataBase[2].firerate, _IM.itemDataBase[2].projectileRange);
            ExecuteAfterSeconds(1, () => returned = true);
            if (_FDM.leftFireFilling == false)
            {
                _FDM.leftHasFired = true;
                _FDM.leftFireCurrent = 0;
                _FDM.leftFireTotal = _IM.itemDataBase[2].firerate;
            }
        }
        

    }

    public void BasicLobProjectile(float _range, float _projectileSpeed, GameObject _prefab, float _firerate)
    {

        print("lobbed");
        //bullet.GetComponent<CurveProjectile>().Shoot();

        //bullet.GetComponent<CurveProjectile>().angle = _PC.directional.transform.rotation.y;

        if(!projectileShot2)
        {
            GameObject bullet = Instantiate(_prefab, _PC.torsoFiringPoint.transform.position, _PC.torsoFiringPoint.transform.rotation);

            print(_PC.directional.transform.forward);

            _PC.torsoFiringPoint.transform.localEulerAngles = new(-angle, _PC.torsoFiringPoint.transform.localEulerAngles.y, _PC.torsoFiringPoint.transform.localEulerAngles.z);


            bullet.GetComponent<Rigidbody>().AddForce(power * _PC.torsoFiringPoint.transform.forward, ForceMode.Impulse);

            projectileShot2 = true;

            ExecuteAfterSeconds(_firerate, () => projectileShot2 = false);
        }


    }


    public void BasicFireProjectileHead(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 flatAimTarget = screenPoint + cursorRay / Mathf.Abs(cursorRay.y) * Mathf.Abs(screenPoint.y - transform.position.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _PC.headFiringPoint.transform.LookAt(hit.point);

            if (!projectileShot)
            {
                print("Fire");
                //particle system

                //Spawn bullet and apply force in the direction of the mouse
                //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
                GameObject bullet = Instantiate(_prefab, _PC.headFiringPoint.transform.position, _PC.headFiringPoint.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);

                //bullet.GetComponent<ItemLook>().firingPoint = _PC.headFiringPoint;
                bullet.GetComponent<RangeDetector>().range = _range;
                bullet.GetComponent<RangeDetector>().positionShotFrom = _PC.transform.position;

                //knockbackActive = true;
                //knockbackStartTime = Time.time;
                Mathf.Clamp(bullet.transform.position.y, 0, 0);



                //Controls the firerate, player can shoot another bullet after a certain amount of time
                projectileShot = true;

                ExecuteAfterSeconds(_firerate, () => projectileShot = false);
            }
            print("FIRE PROJECTILE");

        }
    }
    
    public void BasicFireProjectileTorso(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 flatAimTarget = screenPoint + cursorRay / Mathf.Abs(cursorRay.y) * Mathf.Abs(screenPoint.y - transform.position.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _PC.headFiringPoint.transform.LookAt(hit.point);

            if (!projectileShot3)
            {
                print("Fire");
                //particle system

                //Spawn bullet and apply force in the direction of the mouse
                //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
                GameObject bullet = Instantiate(_prefab, _PC.headFiringPoint.transform.position, _PC.headFiringPoint.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);

                //bullet.GetComponent<ItemLook>().firingPoint = _PC.headFiringPoint;
                bullet.GetComponent<RangeDetector>().range = _range;
                bullet.GetComponent<RangeDetector>().positionShotFrom = _PC.transform.position;

                //knockbackActive = true;
                //knockbackStartTime = Time.time;
                Mathf.Clamp(bullet.transform.position.y, 0, 0);



                //Controls the firerate, player can shoot another bullet after a certain amount of time
                projectileShot3 = true;

                ExecuteAfterSeconds(_firerate, () => projectileShot3 = false);
            }
            print("FIRE PROJECTILE");

        }
    }

}