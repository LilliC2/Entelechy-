using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLook : GameBehaviour
{

    public GameObject firingPoint;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

        //Rotates bullet so it faces the camera
        Vector3 rotate = new Vector3(45f, this.transform.rotation.y, this.transform.rotation.z);
        

        if(this.tag.Contains("Projectile"))
        {
            //transform.eulerAngles = rotate;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            //print("direction " + direction);
            transform.up = direction;
        }
        else
        {
            transform.eulerAngles = rotate;
        }


    
    }
}
