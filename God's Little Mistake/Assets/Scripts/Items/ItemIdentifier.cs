using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemIdentifier : GameBehaviour
{
    bool inRange;
    public Item itemInfo;

    private void Update()
    {
        if(inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                //pick up item
                if (_PC.playerInventory.Count < 5)//invenotry cap number here
                {
                    Destroy(gameObject);
                    _UI.CreateItemSelected(itemInfo);

                }  

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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

    public void OnMouseOver()
    {
        print("HOVER");
        _UI.UpdateItemPopUp(itemInfo);
        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    public void OnMouseExit()
    {
        _UI.statsPopUpPanel.SetActive(false);

    }
}
