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
        print("Equip item");

        //move item to inventory
        if (_PC.playerInventory.Count < 6)
        {


            _UI.statsPopUpPanel.SetActive(false);
            _PC.playerInventory.Add(_item);
            //print("added item");


            var index = _PC.playerInventory.Count - 1;

            //print("Item ID in additem is " + index);

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
                   if(canAutoAddPrimary) _PC.ChangePrimary(index);


                    break;
                case Item.ItemType.Secondary:
                    break;
                case Item.ItemType.Passive:
                    AddItemStats(index);
                    break;
                case Item.ItemType.Symbiote:
                    break;
            }


            
        }



    }

    /// <summary>
    /// Removes item from player's invetory and model and add its to the scene
    /// </summary>
    /// <param name="_inventoryID"></param>
    public void RemoveItemFromInventory(int _inventoryID)
    {
        //remove item from player canvas
        for (int i = 0; i < _AVTAR.slotsOnPlayerFront.Length; i++)
        {
            //check if slot hhas child
            if (_AVTAR.slotsOnPlayerFront[i].transform.childCount != 0)
            {
                //check if item equipped is the item
                var obj = _AVTAR.slotsOnPlayerFront[i].transform.GetChild(0);
                if (obj.name.Contains(_inventoryID.ToString()))
                {
                    _PC.itemsAnimForward.Remove(obj.GetComponentInChildren<Animator>());
                    _PC.itemsAnimLeftSide.Remove(obj.GetComponentInChildren<Animator>());
                    _PC.itemsAnimRightSide.Remove(obj.GetComponentInChildren<Animator>());
                    _PC.itemsAnimBack.Remove(obj.GetComponentInChildren<Animator>());

                    Destroy(obj.gameObject);
                }
            }
        }

        //check if its passive, secondary, primary or symbiote
        switch (_PC.playerInventory[_inventoryID].itemType)
        {
            //calls apprioriate function
            case Item.ItemType.Primary:

                //makes sure if previously active, it removes itself

                break;
            case Item.ItemType.Secondary:
                break;
            case Item.ItemType.Passive:
                RemoveItemStats(_inventoryID);
                break;
            case Item.ItemType.Symbiote:
                break;
        }


        //add item to scene and remove from inventory

        var item = Instantiate(_IG.itemTemp,_PC.transform.position, Quaternion.identity);
        item.GetComponent<ItemIdentifier>().itemInfo = _PC.playerInventory[_inventoryID];

        
        
        item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<ItemIdentifier>().itemInfo.icon;

        

        _UI.UpdateInventorySlotImages();
        //WHEN CURRENCY IS ADDED, PLAYER WOULD GAIN CURRENCY HERE

    }



    /// <summary>
    /// Add passive item buffs to player stats
    /// </summary>
    /// <param name="_index"></param>
    public void AddItemStats(int _inventoryID)
    {
        //if the items ever add HP
        //_PC.health += _PC.playerInventory[_index].health

        //if the items ever add speed
        //_PC.speed += _PC.playerInventory[_index].speed;
        
        _PC.dmg += _PC.playerInventory[_inventoryID].dmg;
        _PC.projectileRange += _PC.playerInventory[_inventoryID].longRange_range;
        _PC.meleeRange += _PC.playerInventory[_inventoryID].melee_range;
        _PC.projectileSpeed += _PC.playerInventory[_inventoryID].projectileSpeed;

        _PC.projectileFirerate += _PC.playerInventory[_inventoryID].firerate;
        _PC.critX += _PC.playerInventory[_inventoryID].critX;
        _PC.critChance += _PC.playerInventory[_inventoryID].critChance;
        _PC.speed += _PC.playerInventory[_inventoryID].movementSpeed;
        _PC.maxSpeed += _PC.playerInventory[_inventoryID].movementSpeed;
    }

    /// <summary>
    /// Remove passive item buffs to player stats
    /// </summary>
    /// <param name="_index"></param>
    public void RemoveItemStats(int _inventoryID)
    {
        //if the items ever add HP
        //_PC.health -= _PC.playerInventory[_index].health

        //if the items ever add speed
        //_PC.speed -= _PC.playerInventory[_index].speed;

        _PC.dmg -= _PC.playerInventory[_inventoryID].dmg;
        _PC.projectileRange -= _PC.playerInventory[_inventoryID].longRange_range;
        _PC.meleeRange -= _PC.playerInventory[_inventoryID].melee_range;
        _PC.projectileSpeed -= _PC.playerInventory[_inventoryID].projectileSpeed;

        _PC.projectileFirerate += _PC.playerInventory[_inventoryID].firerate;
        _PC.critX -= _PC.playerInventory[_inventoryID].critX;
        _PC.critChance -= _PC.playerInventory[_inventoryID].critChance;
    }


}
