using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneItemDataBase : Singleton<InSceneItemDataBase>
{
    public List<Item> inSceneItemDataBase;

    public void AddItemToInventory(int _sceneID)
    {
        //move item to inventory
        if(_PC.playerInventory.Count < 9)
        {
            _PC.playerInventory.Add(inSceneItemDataBase[_sceneID]);
            print("added item");
            inSceneItemDataBase.Remove(inSceneItemDataBase[_sceneID]);

            //move all items in list downm (automatic)
            //make sure their ids stay the same!!!
            for (int i = 0; i < inSceneItemDataBase.Count; i++)
            {
                inSceneItemDataBase[i].inSceneID = inSceneItemDataBase[i].inSceneID - 1;
            }

            int index = _PC.playerInventory.Count - 1;
            //run add item stats here

            _PC.AddItemStatsToPlayer(index);

                 //run add modifer script here
        }



        



    }

    public void RemoveItemFromInventory(int _inventoryID)
    {
        //run remove modifer script here

        //run remove item stats here
        _PC.RemoveItemStatsToPlayer(_inventoryID);


        inSceneItemDataBase.Add(_PC.playerInventory[_inventoryID]);
        _PC.playerInventory.Remove(_PC.playerInventory[_inventoryID]);
        
        var item = Instantiate(_IG.itemTemp, GameObject.Find("Player").transform.position, Quaternion.identity);

        int index = _ISitemD.inSceneItemDataBase.Count - 1;
        inSceneItemDataBase[index].inSceneID = index;

        //WHEN CURRENCY IS ADDED, PLAYER WOULD GAIN CURRENCY HERE

        
    }

}
