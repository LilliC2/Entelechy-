using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSlotStats : GameBehaviour
{
    public int slotNumber;

    public Item itemInfo;

    public UnityEngine.UI.Image itemSprite;
    //public UnityEngine.UI.Image itemSlot;

    public Sprite itemEmpty;

    public bool isFlipped = false;



    // Start is called before the first frame update
    void Start()
    {
        MatchPlayerInventory(slotNumber);
    }

    // Update is called once per frame
    void Update()
    {
        MatchPlayerInventory(slotNumber);
        itemSprite.sprite = itemInfo.pauseIcon;
        if(itemInfo.pauseIcon == null)
        {
            itemSprite.sprite = itemEmpty;
        }
        else
        {
            itemSprite.sprite = itemInfo.pauseIcon;
        }

        if(slotNumber == 4 && !isFlipped)
        {
            Flip();
        }
    }

    public void OnMouseOver()
    {
        _PF.UpdateStats(itemInfo);
        print("Slot" + slotNumber);
    }

    public void UpdateStats()
    {
        _PF.UpdateStats(itemInfo);

    }

    public void Flip()
    {
        Vector2 spriteLook = gameObject.transform.localScale;
        spriteLook.x *= -1;
        gameObject.transform.localScale = spriteLook;

        isFlipped = true;

    }

    public void MatchPlayerInventory(int i)
    {

        Item inslot = new();

        switch(i)
        {
            case 0:
                inslot = _PC.headItem;
                break;
            case 1:
                inslot = _PC.torsoItem;
                break;
            case 2:
                inslot = _PC.legItem;
                break;
        }

    }
}
