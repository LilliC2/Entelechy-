using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerItemAttacks : Singleton<PlayerItemAttacks>
{
    [Header("Slug Legs")]
    bool slugLegsEquipped;
    public GameObject slugLeg_trail;
    public float timeBetweenTrail;
    public float destroyTime;
    bool spawnedTrail = false;

    [Header("Slug Eyes")]
    public bool slugEyesEquipped;
    bool slowProjectile;
    [SerializeField]
    public float slowDuration;
    [SerializeField]
    public float slowPercent; //decimal


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        //SNAIL LEGS
        if (_PC.isMoving&& slugLegsEquipped)
        {
            SlugLegs(timeBetweenTrail);
        }        
    }


    /// <summary>
    /// Creates trial of toxic slime behind player
    /// </summary>
    void SlugLegs(float _timeBetweenTrail)
    {
        if(!spawnedTrail)
        {
            spawnedTrail = true;
            GameObject trail = Instantiate(slugLeg_trail, _PC.transform.position, Quaternion.identity);
            ExecuteAfterSeconds(_timeBetweenTrail, () => spawnedTrail = false);
        }
    }

    public bool SlugEyes()
    {
        var r = Random.Range(1, 3);
        if (r == 1) slowProjectile = true;
        return slowProjectile;
    }

    private Tween TweenValue(float endValue, float time, float tweenValue)
    {
        var speedTween = DOTween.To(() => tweenValue, (x) => tweenValue = x, endValue, time);
        return speedTween;
    }

    public void ItemCheck()
    {
        slugLegsEquipped = false;
        //check if slug legs are on player
        foreach (var item in _PC.playerInventory)
        {
            if (item.ID == 15) slugLegsEquipped = true;
            if (item.ID == 5) slugEyesEquipped = true;
        }

    }

}
