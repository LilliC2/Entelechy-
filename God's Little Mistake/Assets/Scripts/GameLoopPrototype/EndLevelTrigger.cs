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
        if(inRange)
        {

            if(!popped)
            {
                doorAnimator.SetBool("Prime", true);
            }

            print("Reached end of level. Press E to spawn new level");
            if (Input.GetKeyDown(KeyCode.E))
            {

                if(!popped)
                {
                    doorAnimator.SetBool("Popped", true);
                    ExecuteAfterSeconds(1, () => popped = true);
                    
                }
                else
                {
                    ExecuteAfterSeconds(1, () => _GM.readyForGeneration = true);

                }

            }
        }
        else
        {
            if(!popped) doorAnimator.SetBool("Prime", false);

        }
    }


    public void ResetDoor()
    {
        popped = false;
        doorAnimator.SetBool("Prime", false);
        doorAnimator.SetBool("Popped", false);
    }

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
