using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSlotStats : GameBehaviour
{
    public int slotNumber;

    public string itemName;
    public enum Category { Eyes, Mouth, Horns, Slappies, Punchies, Launchers, Legs, Crawlies, None }
    public Category category;
    public enum Segment { Head, Torso, Legs }
    public Segment segment;
    public enum ItemType { Primary, Secondary, Passive, Symbiote }
    public ItemType itemType;

    public enum AttackType { Cone, Straight }
    public AttackType attackType;

    public float currencyValue;

    public bool active;

    public float dmg;
    public float dps;
    public float fireRate;

    public float critX; //crit dmg multipler
    public float critChance;
    public float range;

    public bool projectile;
    public float projectileSpeed;
    public GameObject projectilePF;

    public bool modifier;
    public float modifierID;

    public Sprite icon;
    public GameObject avtarPrefab;
    public GameObject avtarPrefabLeft;
    public GameObject avtarPrefabRight;
    public GameObject avtarPrefabBack;

    // Start is called before the first frame update
    void Start()
    {
        MatchPlayerInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MatchPlayerInventory()
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




    }
}
