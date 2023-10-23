using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLook : GameBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (this.tag.Contains("Projectile"))
        {

            var directional = _PC.directional.transform.eulerAngles.y;

            print("Direction " + -_PC.directional.transform.eulerAngles.y);

            transform.eulerAngles = new Vector3(45, 0, -directional);

        }
    }

    // Update is called once per frame
    void Update()
    {

        //Rotates bullet so it faces the camera
        Vector3 rotate = new Vector3(45f, this.transform.rotation.y, this.transform.rotation.z);
        

        if(!this.tag.Contains("Projectile"))
        {
            //billboard temp
            //transform.LookAt(Camera.main.transform);
            //transform.Rotate(0, 180, 0);
        }
        else
        {

        }


    
    }
}
