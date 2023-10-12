using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    float startingPosX;
    float startingPosZ;
    float lengthOfSpriteX;
    float lengthOfSpriteZ;
    public float amountofParralax; //amount of scroll
    public Camera mainCamera;

    public bool voidFloor;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindAnyObjectByType<Camera>();

        startingPosX = transform.position.x;
        startingPosZ = transform.position.z;
        lengthOfSpriteX = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        lengthOfSpriteZ = GetComponentInChildren<SpriteRenderer>().bounds.size.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(voidFloor)
        {
            float distX = (mainCamera.transform.position.x * amountofParralax);
            float distZ = (mainCamera.transform.position.z * amountofParralax);
            //Vector3 pos = mainCamera.transform.position;
            float tempX = mainCamera.transform.position.x * (1 - amountofParralax);
            float tempZ = mainCamera.transform.position.z * (1 - amountofParralax);
            //float distance = pos.x * amountofParralax;

            transform.position = new Vector3(startingPosX + distX, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, startingPosZ + distZ); //Z because the spirtes are rotated 90 degrees

            //never ending background
            //X
            if (tempX > startingPosX + (lengthOfSpriteX))
            {
                startingPosX += lengthOfSpriteX;
            }
            else if (tempX < startingPosX - (lengthOfSpriteX))
            {
                startingPosX -= lengthOfSpriteX;
            }

            //Z
            if (tempZ > startingPosZ + (lengthOfSpriteZ))
            {
                startingPosZ += lengthOfSpriteZ;
            }
            else if (tempZ < startingPosZ - (lengthOfSpriteZ))
            {
                startingPosZ -= lengthOfSpriteZ;
            }
        }
        else
        {
            float dist = (mainCamera.transform.position.x * amountofParralax);
            //Vector3 pos = mainCamera.transform.position;
            float temp = mainCamera.transform.position.x * (1 - amountofParralax);
            //float distance = pos.x * amountofParralax;

            transform.position = new Vector3(startingPosX + dist, transform.position.y, transform.position.z);

            //never ending background
            if (temp > startingPosX + (lengthOfSpriteX))
            {
                startingPosX += lengthOfSpriteX;
            }
            else if (temp < startingPosX - (lengthOfSpriteX))
            {
                startingPosX -= lengthOfSpriteX;
            }
        }

      
    }
}
