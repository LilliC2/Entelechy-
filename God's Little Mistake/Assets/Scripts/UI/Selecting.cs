using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecting : GameBehaviour
{
    public Item selectedItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        

        if(scrollDelta > 0)
        {
            selectedItem = _UI.leftArmItem;
        }
        if(scrollDelta < 0)
        {
            selectedItem = _UI.rightArmItem;
        }

        if(Input.GetKey(KeyCode.E))
        {
            _UI.CreateItemSelected(selectedItem);
        }
    }
}
