using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats 
{
    public enum CategoryHead { Eyes, Mouth, Horns, None}
    public CategoryHead categoryHead;
    public enum CategoryTorso { Slappies, Punchies, Launchers, None}
    public CategoryTorso categoryTorso;
    public enum CategoryLegs { Legs, Crawlies, None}
    public CategoryLegs categoryLegs;

    public float health;
    public float speed;
    public float dmg;
    public float dps;
    public float fireRate;

    //public float critX; //crit dmg multipler
    //public float critChance;
    public float range;

    public bool projectile;
    public float projectileSpeed;
    public GameObject projectilePF;



    
}
