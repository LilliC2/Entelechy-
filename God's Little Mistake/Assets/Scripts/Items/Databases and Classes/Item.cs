using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public float ID;
    public float inSceneID;
    public int lvl;
    public enum Category { Eyes, Mouth, Horns, Slappies, Punchies, Launchers, Legs, Crawlies, None }
    public Category category;
    public enum Segment {Head, Torso, Legs }
    public Segment segment;
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

    public Item()
    {

    }

}
