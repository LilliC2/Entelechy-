using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : GameBehaviour
{
    public GameObject pickupText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.CheckSphere(transform.position, 2))
        {

            pickupText.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {

                _DC.heldItem = gameObject;
                pickupText.SetActive(false);
            }
               

        }
        else pickupText.SetActive(false);



    }
}
