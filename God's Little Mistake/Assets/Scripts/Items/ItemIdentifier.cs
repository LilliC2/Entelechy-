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
                if (_PC.playerInventory.Count < 5)//invenotry cap number here
                {
                    _UI.CreateItemSelected(id);
                }

                
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

    private void OnMouseOver()
    {
        print("mouse on me");

        _UI.UpdateItemPopUp(_ISitemD.inSceneItemDataBase[id].itemName, _ISitemD.inSceneItemDataBase[id].dmg, _ISitemD.inSceneItemDataBase[id].critX, _ISitemD.inSceneItemDataBase[id].critChance, _ISitemD.inSceneItemDataBase[id].fireRate);
        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    private void OnMouseExit()
    {
        _UI.statsPopUpPanel.SetActive(false);

    }
}
