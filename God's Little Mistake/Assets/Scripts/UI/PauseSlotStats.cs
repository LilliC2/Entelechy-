using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSlotStats : GameBehaviour
{
    public int slotNumber;

    public Item itemInfo;

    public UnityEngine.UI.Image itemSprite;

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
        //if (_PC.playerInventory[slotNumber] !=null)
        //{
        //    itemName = _PC.playerInventory[slotNumber].itemName;
        //    dmg = _PC.playerInventory[slotNumber].dmg;
        //    critChance = _PC.playerInventory[slotNumber].critChance;
        //    critX = _PC.playerInventory[slotNumber].critX;
        //    fireRate = _PC.playerInventory[slotNumber].fireRate;
        //    projectile = _PC.playerInventory[slotNumber].projectile;
        //    projectileSpeed = _PC.playerInventory[slotNumber].projectileSpeed;
        //    icon = _PC.playerInventory[slotNumber].icon;
        //}

        //itemInfo == _PC.playerInventory[i];

        Item inslot = new();

        foreach (var item in _PC.playerInventory)
        {
            if(item.inSlot == i)
            {
                itemInfo = item;
            }
        }

        //dmg = itemInfo.dmg;
        //critChance = itemInfo.critChance;
        //critX = itemInfo.critX;
        //fireRate = itemInfo.fireRate;
        //itemName = itemInfo.itemName;
        //icon = itemInfo.icon;


        //foreach (var item in _PC.playerInventory)
        //{
        //    if (item.inSlot == 3) itemSlot3 = item;
        //    if (item.inSlot == 4) itemSlot4 = item;
        //}








    }
}
