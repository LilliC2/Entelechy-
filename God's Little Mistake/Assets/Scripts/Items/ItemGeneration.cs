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
        //get enemy category

        //get random number

        //pick item from list

        //check game level

        //generate item levelds
    }

    public GameObject GenerateItem(string _category)
    {

        //create list of all possible item drops
        //List<Item> possibleDrops;
        possibleDropsIndex = new List<int>();

        for(int i = 0; i < _ItemD.itemDataBase.Length; i++)
        {
            if (_ItemD.itemDataBase[i].category.ToString() == _category)
            {
                //possibleDrops.Add(_ItemD.itemDataBase[i]);
                possibleDropsIndex.Add(i);
                print("Item added to possible drops CATEGORY: " + _category + i);
            }
        }

        //pick a random possible drop
        int rand = RandomIntBetweenTwoInt(0, possibleDropsIndex.Count);

        Item itemSpawned = _ItemD.itemDataBase[possibleDropsIndex[0]];

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
