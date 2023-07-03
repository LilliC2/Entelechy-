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
    public string category;
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


    public Item()
    {

    }

}
