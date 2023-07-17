using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    float startingPos;
    float lengthOfSprite;
    public float amountofParralax; //amount of scroll
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position.x;
        lengthOfSprite = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (mainCamera.transform.position.x * amountofParralax);
        //Vector3 pos = mainCamera.transform.position;
        float temp = mainCamera.transform.position.x * (1 - amountofParralax);
        //float distance = pos.x * amountofParralax;

        transform.position = new Vector3(startingPos + dist, transform.position.y, transform.position.z);

        //never ending background
        if (temp > startingPos + (lengthOfSprite))
        {
            startingPos += lengthOfSprite;
        }
        else if (temp < startingPos - (lengthOfSprite))
        {
            startingPos -= lengthOfSprite;
        }
    }
}
