using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttacks : Singleton<PlayerAttacks>
{


    [Header("Projectile")]
    public Vector3 target;
    public bool projectileShot;

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

                print("Lob");

                BasicLobProjectile(_IM.itemDataBase[3].projectileRange, _IM.itemDataBase[3].projectileSpeed, _IM.itemDataBase[3].projectilePF);

                break;

            case 4: //Squito

                SquitoAttack();

                break;



        }

    }

    public void PeaShooterAttack()
    {
        _PE.peaShooterPS.Play();
        BasicFireProjectile(_IM.itemDataBase[0].projectilePF, _IM.itemDataBase[0].projectileSpeed, _IM.itemDataBase[0].firerate, _IM.itemDataBase[0].projectileRange);
        //_FDM.leftHasFired = true;
        //_FDM.leftFireCurrent = 0;
        ////_FDM.leftFireTotal = _IM.itemDataBase[0].firerate;
        //_FDM.leftFireTotal = 0.02f;

    }
    
    public void SquitoAttack()
    {
        BasicFireProjectile(_IM.itemDataBase[4].projectilePF, _IM.itemDataBase[4].projectileSpeed, _IM.itemDataBase[4].firerate, _IM.itemDataBase[4].projectileRange);
        //_FDM.rightHasFired = true;
        //_FDM.rightFireCurrent = 0;
        //_FDM.rightFireTotal = _IM.itemDataBase[4].firerate;
    }




    public void SabertoothAttack()
    {
        if(returned)
        {
            returned = false;
            BasicFireProjectile(_IM.itemDataBase[2].projectilePF, _IM.itemDataBase[2].projectileSpeed, _IM.itemDataBase[2].firerate, _IM.itemDataBase[2].projectileRange);
            ExecuteAfterSeconds(1, () => returned = true);
            //_FDM.leftFillObject.SetActive(true);
            //_FDM.leftFireCurrent = 0;
            //_FDM.leftFireTotal = _IM.itemDataBase[2].firerate;
        }
        

    }

    public void BasicLobProjectile(float _range, float _projectileSpeed, GameObject _prefab)
    {

        GameObject bullet = Instantiate(_prefab, _PC.headFiringPoint.transform.position, _PC.headFiringPoint.transform.rotation);

        bullet.GetComponent<CurveProjectile>().Shoot();

        //bullet.GetComponent<CurveProjectile>().angle = _PC.directional.transform.rotation.y;



    }

    Vector3 CalculateTarget(Vector3 _originalPos, float _distance, float _angle)
    {
        // Convert the angle from degrees to radians
        float angleInRadians = _angle * Mathf.Deg2Rad;

        // Calculate the new position
        float newX = _originalPos.x + _distance * Mathf.Cos(angleInRadians);
        float newY = _originalPos.y + _distance * Mathf.Sin(angleInRadians);

        return new Vector3(newX, newY, _originalPos.z);
    }

    public void BasicFireProjectile(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
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

}