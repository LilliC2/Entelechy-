using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : GameBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var horizontal = Input.GetAxis("Horizontal");

        var directional = _PC.directional.transform.eulerAngles.y;


        if (directional >=0 && directional <= 180) transform.localScale = new Vector3(-1, 1, 1);
        if (directional >=181 && directional <= 360) transform.localScale = Vector3.one;

        //if (horizontal < 0) transform.localScale = Vector3.one;
        //if (horizontal > 0) transform.localScale = new Vector3(-1,1,1);
    }
}
