using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : Singleton<ItemGeneration>
{
    public GameObject itemTemp;

    List<int> possibleDropsIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GenerateItem()
    {

        //create list of all possible item drops
        //List<Item> possibleDrops;
        
        

        //pick a random possible drop
        int rand = Random.Range(0, _IM.itemDataBase.Length);

        Item itemSpawned = _IM.itemDataBase[rand];

        //scaling section here
        /*Here we would edit the item values here, so based on level and etc. the prefab of the item would be saved on this too
         */

        //add item to inscene list
        //_ISitemD.inSceneItemDataBase.Add(itemSpawned);

        //give item inscene id

        //give item script inscene id
        itemTemp.GetComponentInChildren<ItemIdentifier>().itemInfo = itemSpawned;
        
        

        return itemTemp;
    }
}
