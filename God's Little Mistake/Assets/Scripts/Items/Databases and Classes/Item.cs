using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public int ID;
    public int lvl;
    public enum Category { Eyes, Mouth, Horns, Slappies, Punchies, Launchers, Legs, Crawlies, None }
    public Category category;
    public enum Segment {Head, Torso, Legs }
    public Segment segment;
    public enum MeleeAttackType {Cone, Line, None }
    public MeleeAttackType meleeAttackType;
    public enum ItemType {Primary, Secondary, Passive, Symbiote }
    public ItemType itemType;
    public float currencyValue;

    public bool active;

    public float dmg;
    public float dps;
    public float fireRate;
    public float meleeSpeed;

    public float critX; //crit dmg multipler
    public float critChance;
    public float range;

    public bool projectile;
    public float projectileSpeed;
    public GameObject projectilePF;

    public bool modifier;
    public float modifierID;

    [Header("Animation")]
    public Sprite icon;
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
