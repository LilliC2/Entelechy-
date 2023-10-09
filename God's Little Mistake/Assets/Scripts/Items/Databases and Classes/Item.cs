using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public int ID;
    public int lvl;
    public int inSlot = -1;
    public enum Category { Eyes, Mouth, Horns, Slappies, Punchies, Launchers, Legs, Crawlies, None }
    public Category category;
    public enum Segment {Head, Torso, Legs }
    public Segment segment;
    public enum AttackType {Cone, Line, None, Circle, Rapid, Lob, Laser, Cannon }
    public AttackType meleeAttackType;
    public enum ItemType {Primary, Secondary, Passive, Symbiote }
    public ItemType itemType;

    public bool hasActiveAbility;
    public float currencyValue;

    public bool active;

    public float dmg;
    public float dps;
    public float longRangeSpeed;
    public float meleeSpeed;

    public float critX; //crit dmg multipler
    public float critChance;
    public float longRange_range;
    public float melee_range;

    public bool projectile;
    public float projectileSpeed;
    public GameObject projectilePF;
    public float fireRate;


    public bool modifier;
    public float modifierID;

    [Header("Animation")]
    public Sprite icon;
    public Sprite pauseIcon;
    public GameObject avatarPrefabFrontLeft;
    public GameObject avatarPrefabFrontRight;

    public GameObject avtarPrefabLeft;
    public GameObject avtarPrefabRight;

    public GameObject avtarPrefabBackLeft;
    public GameObject avtarPrefabBackRight;

    public Item()
    {
        
    }

}
