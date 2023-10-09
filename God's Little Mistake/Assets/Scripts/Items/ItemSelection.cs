using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelection : GameBehaviour
{
    public Item itemInfo;
    public int selectedArmSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if( itemInfo.segment == Item.Segment.Torso ) 
        {
            if (scrollDelta > 0)
            {
                selectedArmSlot = 3;
            }
            else if (scrollDelta < 0)
            {
                selectedArmSlot = 4;
            }
        }

        // Scroll up sets selectedArmSlot to 3, scroll down sets it to 4
        
    }
}
