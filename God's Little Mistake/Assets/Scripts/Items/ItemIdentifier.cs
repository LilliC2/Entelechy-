using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemIdentifier : GameBehaviour
{
    bool inRange;
    public Item itemInfo;

    private void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = itemInfo.icon;
    }

    private void Update()
    {
        if(inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                //pick up item
                if (_PC.playerInventory.Count < 5)//invenotry cap number here
                {
                    _AM.ItemPickUp();

                    _UI.CreateItemSelected(itemInfo);

                    ExecuteAfterFrames(3, () => Destroy(gameObject));
                    

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
            inRange = false;

        }
    }

    public void OnMouseOver()
    {
        _UI.UpdateItemPopUp(itemInfo);
        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    public void OnMouseExit()
    {
        _UI.statsPopUpPanel.SetActive(false);

    }
}
