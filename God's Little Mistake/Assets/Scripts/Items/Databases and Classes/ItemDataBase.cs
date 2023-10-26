using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : Singleton<ItemDataBase>
{ 
    public Item[] itemDataBase;


    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemToInventory(Item _item)
    {

        print("Item equping is " + _item.itemName);
        switch (_item.segment)
        {
            case Item.Segment.Head:
                _PC.headItem = _item;
                break;
            case Item.Segment.Torso:
                _PC.torsoItem = _item;
                break;
            case Item.Segment.Legs:
                _PC.legItem = _item;
                break;

        }
        _UI.UpdateInventorySlotImages();

    }

    /// <summary>
    /// Removes item from player's invetory and model and add its to the scene
    /// </summary>
    /// <param name="_inventoryID"></param>
    public void RemoveItemFromInventory(Item _item)
    {
        switch (_item.segment)
        {
            case Item.Segment.Head:
                _PC.headItem = null;
                break;
            case Item.Segment.Torso:
                _PC.torsoItem = null;
                break;
            case Item.Segment.Legs:
                _PC.legItem = null;
                break;

        }
        _UI.UpdateInventorySlotImages();
        //WHEN CURRENCY IS ADDED, PLAYER WOULD GAIN CURRENCY HERE

    }
}
