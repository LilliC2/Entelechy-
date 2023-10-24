using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneItemDataBase : Singleton<InSceneItemDataBase>
{

    /// <summary>
    /// Adds item to players inventory and removes it from scene
    /// </summary>
    /// <param name="_sceneID"></param>
    public void AddItemToInventory(Item _item)
    {
        switch(_item.segment)
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
