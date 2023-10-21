using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
//using static UnityEditor.Progress;

public class Selecting : GameBehaviour
{
    public Item selectedItem;

    ItemIdentifier itemIdentifier;

    [SerializeField]
    public GameObject itemGO;
    public Item previousItem;
    
    // Start is called before the first frame update
    void Start()
    {
        itemIdentifier = GetComponent<ItemIdentifier>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public bool CheckIfItemIsInSlot()
    {
        var category = itemIdentifier.itemInfo.category;

        print("hover item cat is " + category);

        bool isItemInSlot = false;

        if (itemIdentifier.itemInfo.segment != Item.Segment.Torso)
        {
            foreach (var item in _PC.playerInventory)
            {
                if (item.category == category)
                {
                    previousItem = item;
                    isItemInSlot = true;
                }
            }

        }
        else
        {
            bool slot3 = false;
            bool slot4 = false;

            foreach (var item in _PC.playerInventory)
            {
                if (item.inSlot == 3)
                {
                    slot3 = true;
                }
                if (item.inSlot == 4)
                {
                    slot4 = true;
                }

                if (slot3 && slot4)
                {
                    previousItem = item;
                    isItemInSlot = true;
                }
            }


        }


        return isItemInSlot;
    }

    public void RemovePreviousItem()
    {
        //remove from player avatar


        var toDestroyLeft = _AVTAR.slotsOnPlayerLeft[previousItem.inSlot].transform.GetChild(0);
        var toDestroyBack = _AVTAR.slotsOnPlayerBack[previousItem.inSlot].transform.GetChild(0);
        var toDestroyFront = _AVTAR.slotsOnPlayerRight[previousItem.inSlot].transform.GetChild(0);
        var toDestroyRight = _AVTAR.slotsOnPlayerFront[previousItem.inSlot].transform.GetChild(0);

        //print("Destroying: " + toDestroyLeft + ", " + toDestroyBack + ", " + toDestroyFront + ", " + toDestroyRight);


        Destroy(toDestroyBack.gameObject);
        Destroy(toDestroyLeft.gameObject);
        Destroy(toDestroyRight.gameObject);
        Destroy(toDestroyFront.gameObject);

        //remove from player inventory
        var index = _PC.playerInventory.IndexOf(previousItem);
        _ISitemD.RemoveItemStats(index);
        _PC.playerInventory.Remove(previousItem);
        _UI.UpdateInventorySlotImages();
    }
}
