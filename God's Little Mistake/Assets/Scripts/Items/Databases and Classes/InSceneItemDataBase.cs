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

            for(int i = 0; i < inSceneItemDataBase.Count; i++)
            {
                inSceneItemDataBase[i].inSceneID = inSceneItemDataBase[i].inSceneID - 1;
            }
        }


        //move all items in list downm (automatic)
        //make sure their ids stay the same!!!
    }
}
