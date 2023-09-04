using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemAttacks : GameBehaviour
{
    [Header("Slug Legs")]
    public GameObject slugLeg_trail;
    public float timeBetweenTrail;
    bool spawnedTrail;

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
            DeteriateTrail(trail);
            ExecuteAfterSeconds(_timeBetweenTrail, () => spawnedTrail = false);
        }
    }

    void DeteriateTrail(GameObject _trail)
    {
        //will add a fade away later

        ExecuteAfterSeconds(3, () => Destroy(_trail));
    }

}
