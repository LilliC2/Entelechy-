using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLook : GameBehaviour
{
    float directional;

    // Start is called before the first frame update
    void Start()
    {
        if (this.tag.Contains("Projectile"))
        {
            directional = _PC.directional.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(45, 0, -directional);
            //print("Direction " +-_PC.directional.transform.eulerAngles.y);

        }


    }

    // Update is called once per frame
    void Update()
    {

        //Rotates bullet so it faces the camera
        Vector3 rotate = new Vector3(45f, this.transform.rotation.y, this.transform.rotation.z);


        if (!this.tag.Contains("Projectile"))
        {
            transform.eulerAngles = rotate;
        }


    
    }
}
