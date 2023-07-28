using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private SimpleRandomWalkData randomWalkParameters;


    protected override void RunProceduralGeneration()
    {
        //create floor position
        HashSet<Vector3> floorPositions = RunRandomWalk();
        tileMapVisualiser.Clear();

        //generate tiles
        tileMapVisualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualiser);
    }


    protected HashSet<Vector3> RunRandomWalk()
    {
        var currenPos = startPos;
        HashSet<Vector3> floorPositions = new HashSet<Vector3>();
        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorthrims.SimpleRandomWalk(currenPos, randomWalkParameters.walkLength);
            //add to floor pos and ensure no dups are added
            floorPositions.UnionWith(path);
            if(randomWalkParameters.startRandomlyEachIteration)
            {
                currenPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
