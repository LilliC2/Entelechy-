using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats 
{
    public enum Segment { Head, Torso, Legs }
    public Segment[] segments;
    public float health;
    public float maxHP;
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
