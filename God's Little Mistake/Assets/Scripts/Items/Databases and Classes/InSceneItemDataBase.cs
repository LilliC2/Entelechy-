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
                   if(canAutoAddPrimary) _PC.ChangePrimary(index);


                    break;
                case Item.ItemType.Secondary:
                    break;
                case Item.ItemType.Passive:

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
        _PC.playerInventory.Remove(_PC.playerInventory[_inventoryID]);
        RemoveItemStats(_inventoryID);
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
        _PC.projectileRange += _PC.playerInventory[_inventoryID].projectile_range;
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
        _PC.projectileRange -= _PC.playerInventory[_inventoryID].projectile_range;
        _PC.meleeRange -= _PC.playerInventory[_inventoryID].melee_range;
        _PC.projectileSpeed -= _PC.playerInventory[_inventoryID].projectileSpeed;

        _PC.projectileFirerate += _PC.playerInventory[_inventoryID].firerate;
        _PC.critX -= _PC.playerInventory[_inventoryID].critX;
        _PC.critChance -= _PC.playerInventory[_inventoryID].critChance;
    }


}
