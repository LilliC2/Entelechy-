using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : GameBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Reached end of level. Press E to spawn new level");
            if (Input.GetKeyDown(KeyCode.E))
            {
                _GM.readyForGeneration = true;
            }
        }


    }
}
