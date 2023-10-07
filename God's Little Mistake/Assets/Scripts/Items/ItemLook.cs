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

            print(firingPoint);

            if (firingPoint.transform.eulerAngles.y > 0 && firingPoint.transform.eulerAngles.y < 180)
            {
                //stay the same
            }
            else if (firingPoint.transform.eulerAngles.y > 180 && firingPoint.transform.eulerAngles.y < 360)
            {
                //print("flip it");
                //flip projectile
                var x = -projectileScript.image.transform.localScale.x;
                //print(x);
                projectileScript.image.transform.localScale = new Vector3(x, projectileScript.image.transform.localScale.y, projectileScript.image.transform.localScale.z);
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
