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
        int rand = 0;

        rand = Random.Range(0, _IM.itemDataBase.Length);
        if (_IM.itemDataBase[rand].ID == 10 || _IM.itemDataBase[rand].ID == 11 || _IM.itemDataBase[rand].ID == -5)
            rand = Random.Range(0, _IM.itemDataBase.Length);

        //pick a random possible drop

        Item itemSpawned = _IM.itemDataBase[rand];

        
        itemTemp.GetComponentInChildren<ItemIdentifier>().itemInfo = itemSpawned;
        
        

        return itemTemp;
    }
}
