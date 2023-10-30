using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttacks : Singleton<PlayerAttacks>
{


    [Header("Projectile")]
    public Vector3 target;
    bool projectileShot;

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



        }

    }

    public void PeaShooterAttack()
    {
        _PE.peaShooterPS.Play();
        BasicFireProjectile(_IM.itemDataBase[0].projectilePF, _IM.itemDataBase[0].projectileSpeed, _IM.itemDataBase[0].firerate, _IM.itemDataBase[0].projectileRange);

    }




    public void SabertoothAttack()
    {
        if(returned)
        {
            returned = false;
            BasicFireProjectile(_IM.itemDataBase[2].projectilePF, _IM.itemDataBase[2].projectileSpeed, _IM.itemDataBase[2].firerate, _IM.itemDataBase[2].projectileRange);
            ExecuteAfterSeconds(1, () => returned = true);
        }
        

    }

    public void BasicLobProjectile(float _range, float _projectileSpeed)
    {

        //get target position
        var x = _range * Mathf.Cos(_PC.directional.transform.rotation.y * Mathf.Deg2Rad);
        var y = _range * Mathf.Sign(_PC.directional.transform.rotation.y * Mathf.Deg2Rad);
        Vector3 targetPos = _PC.transform.position;
        targetPos.x += x;
        targetPos.y += y;

        //https://gamedev.stackexchange.com/questions/114522/how-can-i-launch-a-gameobject-at-a-target-if-i-am-given-everything-except-for-it

        float gSquared = Physics.gravity.sqrMagnitude;
        float b = _projectileSpeed * _projectileSpeed + Vector3.Dot(targetPos, Physics.gravity);


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