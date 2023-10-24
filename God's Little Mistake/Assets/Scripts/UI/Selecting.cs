using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
//using static UnityEditor.Progress;

public class Selecting : GameBehaviour
{
    public Item selectedItem;

    ItemIdentifier itemIdentifier;

    [SerializeField]
    public GameObject itemGO;
    public Item previousItem;
    
    // Start is called before the first frame update
    void Start()
    {
        itemIdentifier = GetComponent<ItemIdentifier>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    

    public void RemovePreviousItem()
    {
        //remove from player avatar
        int inSlot = -1;

        switch (previousItem.segment)
        {
            case Item.Segment.Head:
                inSlot = 0;

                break;
            case Item.Segment.Torso:
                inSlot = 1;


                break;
            case Item.Segment.Legs:
                inSlot = 2;


                break;

        }

        var toDestroyLeft = _AVTAR.slotsOnPlayerLeft[inSlot].transform.GetChild(0);
        var toDestroyBack = _AVTAR.slotsOnPlayerBack[inSlot].transform.GetChild(0);
        var toDestroyFront = _AVTAR.slotsOnPlayerRight[inSlot].transform.GetChild(0);
        var toDestroyRight = _AVTAR.slotsOnPlayerFront[inSlot].transform.GetChild(0);

        //print("Destroying: " + toDestroyLeft + ", " + toDestroyBack + ", " + toDestroyFront + ", " + toDestroyRight);


        Destroy(toDestroyBack.gameObject);
        Destroy(toDestroyLeft.gameObject);
        Destroy(toDestroyRight.gameObject);
        Destroy(toDestroyFront.gameObject);

        _ISitemD.RemoveItemFromInventory(previousItem);
    }
}
