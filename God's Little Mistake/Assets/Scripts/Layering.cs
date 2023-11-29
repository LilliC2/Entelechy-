using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layering : GameBehaviour
{
    [SerializeField]
    SpriteRenderer[] spritesOnObject;


    // Start is called before the first frame update
    void Start()
    {
        spritesOnObject = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < _PC.transform.position.z)
        {

            //in front
            foreach (var sprite in spritesOnObject)
            {
                if(sprite != null) sprite.sortingLayerID = SortingLayer.NameToID("Front");

            }
        }
        else if(transform.position.z > _PC.transform.position.z)
        {

            //behind
            foreach (var sprite in spritesOnObject)
            {
                if (sprite != null) sprite.sortingLayerID = SortingLayer.NameToID("Back");
            }
        }
    }
}
