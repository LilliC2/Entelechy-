using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Selecting : GameBehaviour
{
    public Item selectedItem;

    [SerializeField]
    public GameObject itemGO;
    public Item previousItem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ////check if selected item is arms!!!


        //if (isHovering)
        //{
        //    if (itemInfo.segment == Item.Segment.Torso)
        //    {
        //        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        //        if (scrollDelta > 0)
        //        {
        //            _UI.leftArmItem = itemInfo;
        //            //Changes item to left here
        //        }
        //        if (scrollDelta < 0)
        //        {
        //            _UI.rightArmItem = itemInfo;
        //            //Changes item to left here
        //        }
        //    }
        //}


        //if (inRange)
        //{
        //    if (Input.GetKey(KeyCode.E))
        //    {
        //        //check which segment it is

        //        //check if item is already there
        //        bool itemInSlot = selecting.CheckIfItemIsInSlot();

        //        if (itemInSlot)
        //        {

        //            //destroy old item from player avatar
        //            selecting.RemovePreviousItem();

        //            //place old item on ground
        //            GameObject item = Instantiate(itemGO, gameObject.transform.position, Quaternion.identity);

        //            item.GetComponent<ItemIdentifier>().itemInfo = selecting.previousItem;
        //            item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<ItemIdentifier>().itemInfo.icon;
        //        }


        //        //equip new items
        //        _UI.CreateItemSelected(itemInfo);
        //    }
        //}


        //if (Input.GetKey(KeyCode.E))
        //{
        //    //check which segment it is

        //    //check if item is already there
        //    bool itemInSlot = CheckIfItemIsInSlot();

        //    if(itemInSlot)
        //    {

        //        //destroy old item from player avatar
        //        RemovePreviousItem();

        //        //place old item on ground
        //        GameObject item = Instantiate(itemGO, gameObject.transform.position, Quaternion.identity);

        //        item.GetComponent<ItemIdentifier>().itemInfo = previousItem;
        //        item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<ItemIdentifier>().itemInfo.icon;
        //    }


        //    //equip new items
        //    _UI.CreateItemSelected(selectedItem);
        //}

    }

    public bool CheckIfItemIsInSlot()
    {
        var segment = selectedItem.segment;

        bool isItemInSlot = false;

        foreach (var item in _PC.playerInventory)
        {
            if (item.segment == segment)
            {
                previousItem = item;
                isItemInSlot = true;
            }
        }

        return isItemInSlot;
    }

    public void RemovePreviousItem()
    {
        //remove from player avatar

        var toDestroyLeft = _AVTAR.slotsOnPlayerLeft[previousItem.inSlot].transform.GetChild(0);
        var toDestroyBack = _AVTAR.slotsOnPlayerBack[previousItem.inSlot].transform.GetChild(0);
        var toDestroyFront = _AVTAR.slotsOnPlayerFront[previousItem.inSlot].transform.GetChild(0);
        var toDestroyRight = _AVTAR.slotsOnPlayerFront[previousItem.inSlot].transform.GetChild(0);

        Destroy(toDestroyBack);
        Destroy(toDestroyLeft);
        Destroy(toDestroyRight);
        Destroy(toDestroyFront);

        //remove from player inventory
        _PC.playerInventory.Remove(previousItem);
    }
}
