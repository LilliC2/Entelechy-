using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : GameBehaviour
{
    public GameObject pickupText;

    bool inRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange)
        {
            print("player pick up here");

            if (Input.GetKeyDown(KeyCode.E))
            {
               
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        if (other.CompareTag("Player")) inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) inRange = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);   
    }
}
