using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerItemAttacks : GameBehaviour
{
    [Header("Slug Legs")]
    public GameObject slugLeg_trail;
    public float timeBetweenTrail;
    public float destroyTime;
    bool spawnedTrail = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SNAIL LEGS
        if (_PC.isMoving)
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


    private Tween TweenValue(float endValue, float time, float tweenValue)
    {
        var speedTween = DOTween.To(() => tweenValue, (x) => tweenValue = x, endValue, time);
        return speedTween;
    }

}
