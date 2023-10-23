using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeadRotate : GameBehaviour
{
    [SerializeField]
    float directional;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        //var angle = FindRotation();

        //transform.LookAt(angle);

        directional = _PC.directional.transform.eulerAngles.y;

        print("Direction " +-_PC.directional.transform.eulerAngles.y);

        transform.eulerAngles = new Vector3(45,0 ,-directional);
    }


}
