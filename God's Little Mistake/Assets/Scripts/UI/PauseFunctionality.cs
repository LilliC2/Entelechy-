using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunctionality : GameBehaviour
{
    public int slotNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PausingFucntion()
    {
        _GM.OnPause();
    }

    public void SearchSlot()
    {
        //_AVTAR.slotsOnCanvas[slotNumber];

        //_PC.playerInventory.(slotNumber)
    }

    //public List<Item> SearchForItemMatch(Item item)
    //{
    //    List<Item> itemMatchInPlayerInven = new();

    //}
}
