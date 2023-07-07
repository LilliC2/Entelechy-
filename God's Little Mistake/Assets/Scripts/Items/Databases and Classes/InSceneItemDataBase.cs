using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneItemDataBase : Singleton<InSceneItemDataBase>
{
    public List<Item> inSceneItemDataBase;

    public void AddItemToInventory(int _sceneID)
    {
        

        //move item to inventory
        if (_PC.playerInventory.Count < 6)
        {

            _UI.statsPopUpPanel.SetActive(false);
            _PC.playerInventory.Add(inSceneItemDataBase[_sceneID]);
            print("added item");
            inSceneItemDataBase.Remove(inSceneItemDataBase[_sceneID]);

            //move all items in list downm (automatic)
            //make sure their ids stay the same!!!
            for (int i = 0; i < inSceneItemDataBase.Count; i++)
            {
                inSceneItemDataBase[i].inSceneID = inSceneItemDataBase[i].inSceneID - 1;
            }

            _PC.playerInventory[_PC.playerInventory.Count - 1].ID = _PC.playerInventory.Count-1;

            _UI.UpdateInventorySlotImages();
        }


    }

    public void RemoveItemFromInventory(int _inventoryID)
    {
        //remove item from player canvas
        for (int i = 0; i < _AVTAR.slotsOnPlayer.Length; i++)
        {
            //check if slot hhas child
            if (_AVTAR.slotsOnPlayer[i].transform.childCount != 0)
            {
                //check if item equipped is the item
                var obj = _AVTAR.slotsOnPlayer[i].transform.GetChild(0);
                print(obj.name);
                print(i);
                if (obj.name.Contains(_inventoryID.ToString()))
                {
                    print("obj name contains " + i.ToString());
                    Destroy(obj.gameObject);
                }
            }
        }


        inSceneItemDataBase.Add(_PC.playerInventory[_inventoryID]);
        _PC.playerInventory.Remove(_PC.playerInventory[_inventoryID]);
        
        var item = Instantiate(_IG.itemTemp, GameObject.Find("Player").transform.position, Quaternion.identity);

        int index = _ISitemD.inSceneItemDataBase.Count - 1;
        inSceneItemDataBase[index].inSceneID = index;

        

        _UI.UpdateInventorySlotImages();
        //WHEN CURRENCY IS ADDED, PLAYER WOULD GAIN CURRENCY HERE

    }

}
