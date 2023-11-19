using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : GameBehaviour
{

    Animator doorAnimator;

    bool inRange;


    bool popped;

    private void Start()
    {
        doorAnimator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_GM.isLevelCleared == true)
                {

                    print("new level");

                    _GM.readyForGeneration = true;

                }
                else print("Enemies remain");
            }
            
           
        }

    }


    //public void ResetDoor()
    //{
    //    popped = false;
    //    doorAnimator.SetBool("Prime", false);
    //    doorAnimator.SetBool("Popped", false);
    //}

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
