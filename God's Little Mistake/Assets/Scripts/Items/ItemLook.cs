using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLook : GameBehaviour
{

    public GameObject firingPoint;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rotate = new Vector3(45f, this.transform.rotation.y, this.transform.rotation.z);
        var projectileScript = GetComponent<BasicProjectile>();
        


        if (this.tag.Contains("Projectile"))
        {
            rotate = new Vector3(45f, this.transform.rotation.y, this.transform.rotation.z);
            transform.eulerAngles = rotate;

            //print("Angle of firing point " + _PC.firingPoint.transform.eulerAngles);

            //var y1 = _PC.transform.position.y;
            //var y2 = gameObject.transform.position.y;
            //var x1 = _PC.transform.position.y;
            //var x2 = gameObject.transform.position.y;

            //print("Angle from image " + Mathf.Atan2(y2-y1,x2-x1));


            if (firingPoint.transform.eulerAngles.y > 0 && firingPoint.transform.eulerAngles.y < 180)
            {
                //stay the same
            }
            else if (firingPoint.transform.eulerAngles.y > 180 && firingPoint.transform.eulerAngles.y < 360)
            {
                float x;
                //print("flip it");
                //flip projectile
                if (gameObject.name == "Projectile_RPEAG")
                {

                    x = -GetComponent<RPEAGProjectile>().image.transform.localScale.x;
                    GetComponent<RPEAGProjectile>().image.transform.localScale = new Vector3(x, projectileScript.image.transform.localScale.y, projectileScript.image.transform.localScale.z);

                }
                else if (gameObject.name.Contains("Boomerang"))
                {
                    x = -GetComponent<BoomerangProjectile>().image.transform.localScale.x;

                    var projectileScript2 = GetComponent<BoomerangProjectile>();

                    projectileScript2.image.transform.localScale = new Vector3(x, projectileScript2.image.transform.localScale.y, projectileScript2.image.transform.localScale.z);

                }
                else
                {
                    x = GetComponent<BasicProjectile>().image.transform.localScale.x;

                    var projectileScript3 = GetComponent<BoomerangProjectile>();


                    GetComponent<BasicProjectile>().image.transform.localScale = new Vector3(x, projectileScript3.image.transform.localScale.y, projectileScript3.image.transform.localScale.z);

                }
                //print(x);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        //Rotates bullet so it faces the camera
        Vector3 rotate = new Vector3(45f, this.transform.rotation.y, this.transform.rotation.z);
        

        if(!this.tag.Contains("Projectile"))
        {
            transform.eulerAngles = rotate;
        }
        else
        {

        }


    
    }
}
