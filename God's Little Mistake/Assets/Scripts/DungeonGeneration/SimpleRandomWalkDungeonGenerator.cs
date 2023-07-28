using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private int iterations = 10; //number of times the algrothim is run
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;


    protected override void RunProceduralGeneration()
    {
        //create floor position
        HashSet<Vector3> floorPositions = RunRandomWalk();
        tileMapVisualiser.Clear();

        //generate tiles
        tileMapVisualiser.PaintFloorTiles(floorPositions);
    }


    protected HashSet<Vector3> RunRandomWalk()
    {
        var currenPos = startPos;
        HashSet<Vector3> floorPositions = new HashSet<Vector3>();
        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorthrims.SimpleRandomWalk(currenPos, walkLength);
            //add to floor pos and ensure no dups are added
            floorPositions.UnionWith(path);
            if(startRandomlyEachIteration)
            {
                currenPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
