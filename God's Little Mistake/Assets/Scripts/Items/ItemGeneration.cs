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

        //generate item level
    }

    public GameObject GenerateItem(string _category)
    {

        //create list of all possible item drops
        //List<Item> possibleDrops;
        possibleDropsIndex = new List<int>();

        for(int i = 0; i < _ItemD.itemDataBase.Length; i++)
        {
            if (_ItemD.itemDataBase[i].category == _category)
            {
                //possibleDrops.Add(_ItemD.itemDataBase[i]);
                possibleDropsIndex.Add(i);
            }
        }

        //pick a random possible drop
        int rand = RandomIntBetweenTwoInt(0, possibleDropsIndex.Count);

        Item itemSpawned = _ItemD.itemDataBase[possibleDropsIndex[rand]];

        //scaling section here
        /*Here we would edit the item values here, so based on level and etc. the prefab of the item would be saved on this too
         */

        _ISitemD.inSceneItemDataBase.Add(itemSpawned);
        int index = _ISitemD.inSceneItemDataBase.Count - 1;
        print(index);
        _ISitemD.inSceneItemDataBase[index].inSceneID = index;

        return itemTemp;
    }
}
