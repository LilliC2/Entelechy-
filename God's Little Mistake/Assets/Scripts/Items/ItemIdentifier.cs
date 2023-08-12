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
                
                //pick up item
                if (_PC.playerInventory.Count < 5)//invenotry cap number here
                {
                    Destroy(gameObject);
                    _UI.CreateItemSelected(id);

                    
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

    private void OnMouseOver()
    {
        _UI.UpdateItemPopUp(_ISitemD.inSceneItemDataBase[id].itemName, _ISitemD.inSceneItemDataBase[id].dmg, _ISitemD.inSceneItemDataBase[id].critX, _ISitemD.inSceneItemDataBase[id].critChance, _ISitemD.inSceneItemDataBase[id].fireRate);
        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    private void OnMouseExit()
    {
        _UI.statsPopUpPanel.SetActive(false);

    }
}
