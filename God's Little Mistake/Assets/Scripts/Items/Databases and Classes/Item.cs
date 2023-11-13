using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public int ID;
    public int lvl;

    public enum Segment {Head, Torso, Legs }
    public Segment segment;

    public float currencyValue;

    public bool active;

    public float dmg;
    public float firerate;

    public float critChance;
    public float projectileRange;

    public bool projectile;
    public float projectileSpeed;
    public GameObject projectilePF;

    public float movementSpeed;

    public bool modifier;
    public float modifierID;

    [Header("Animation")]
    public Sprite icon;
    public Sprite pauseIcon;

    public Sprite staticTempSprite;

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
