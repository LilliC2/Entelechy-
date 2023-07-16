using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Rotates bullet so it faces the camera
        Vector3 rotate = new Vector3(-45f, 0f, 0f);
        transform.eulerAngles = rotate;
    }
}
