using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    public string atkName;
    public int ID;
    public string category;
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


    public Attack()
    {

    }

    public Attack(string atkName,int ID, string category, bool active, float dmg, float dps, float fireRate, float critX, float critChance, float range, bool projectile, float projectileSpeed, GameObject projectilePF)
    {
        this.atkName = atkName;
        this.ID = ID;
        this.category = category;
        this.active = active;
        this.dmg = dmg;
        this.dps = dps;
        this.fireRate = fireRate;
        this.critX = critX;
        this.critChance = critChance;
        this.range = range;
        this.projectile = projectile;
        this.projectileSpeed = projectileSpeed;
        this.projectilePF = projectilePF;
    }

}
