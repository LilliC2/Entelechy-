using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdentifier : GameBehaviour
{
    public int id;
    bool inRange;

    private void Update()
    {
        if(inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("PICK UP");
                //pick up item
                _ISitemD.AddItemToInventory(id);
                Destroy(this.gameObject);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player");
            inRange = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player");
            inRange = false;

        }
    }
}
