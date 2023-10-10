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

    //public string itemName;
    //public enum Category { Eyes, Mouth, Horns, Slappies, Punchies, Launchers, Legs, Crawlies, None }
    //public Category category;
    //public enum Segment { Head, Torso, Legs }
    //public Segment segment;
    //public enum ItemType { Primary, Secondary, Passive, Symbiote }
    //public ItemType itemType;

    //public enum AttackType { Cone, Straight }
    //public AttackType attackType;

    //public float currencyValue;

    //public bool active;

    //public float dmg;
    //public float dps;
    //public float fireRate;

    //public float critX; //crit dmg multipler
    //public float critChance;
    //public float range;

    //public bool projectile;
    //public float projectileSpeed;
    //public GameObject projectilePF;

    //public bool modifier;
    //public float modifierID;

    //public Sprite icon;
    //public GameObject avtarPrefab;
    //public GameObject avtarPrefabLeft;
    //public GameObject avtarPrefabRight;
    //public GameObject avtarPrefabBack;

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
