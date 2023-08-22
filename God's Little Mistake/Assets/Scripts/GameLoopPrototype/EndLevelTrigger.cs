using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : GameBehaviour
{
    bool inRange;
    private void FixedUpdate()
    {
        if(inRange)
        {
            print("Reached end of level. Press E to spawn new level");
            if (Input.GetKeyDown(KeyCode.E))
            {
                _GM.readyForGeneration = true;
            }
        }
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
