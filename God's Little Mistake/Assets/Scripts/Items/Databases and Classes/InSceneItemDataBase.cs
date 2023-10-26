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

<<<<<<< HEAD
=======

            _UI.statPop.SetActive(false);
            //print("added item");



            //print("Item ID in additem is " + index);

            if(_item != null) _PC.playerInventory.Add(_item);

            var index = _PC.playerInventory.IndexOf(_item);


            AddItemStats(index);

            _UI.UpdateInventorySlotImages();

            //check if its passive, secondary, primary or symbiote
            switch(_PC.playerInventory[index].itemType)
            {
                //calls apprioriate function
                case Item.ItemType.Primary:



                    //if primary and no other primarys are there, add this one
                    bool canAutoAddPrimary = true;
                    //find segement
                    foreach (var item in _PC.playerInventory)
                    {
                        //check if active in segment
                        if (item.active)
                        {
                            if(item.segment == _PC.playerInventory[index].segment)
                            {
                                canAutoAddPrimary = false;
                            }
                        }
                    }
                   //if(canAutoAddPrimary) _PC.ChangePrimary(index);


                    break;
                case Item.ItemType.Secondary:
                    break;
                case Item.ItemType.Passive:

                    break;
                case Item.ItemType.Symbiote:
                    break;
            }


            
>>>>>>> NLM-372-Sprite-Flip-Prototype-Implementation
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
