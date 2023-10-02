using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomisation : GameBehaviour
{

    public SpriteRenderer[] enemySpritesArray;

    public Color[] hueArray;
    Color colour;


    // Update is called once per frame
    void Start()
    {

        colour = hueArray[Random.Range(0, hueArray.Length)];
        //get all sprites in children

        enemySpritesArray = GetComponentsInChildren<SpriteRenderer>();

        foreach (var sprite in enemySpritesArray)
        {
            sprite.color = colour;
        }
    }


}
