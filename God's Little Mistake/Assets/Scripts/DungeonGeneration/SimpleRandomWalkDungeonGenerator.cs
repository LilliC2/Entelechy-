using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkData randomWalkParameters;


    protected override void RunProceduralGeneration()
    {
        //create floor position
        HashSet<Vector3> floorPositions = RunRandomWalk(randomWalkParameters,startPos);
        tileMapVisualiser.Clear();

        //generate tiles
        tileMapVisualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualiser);
    }


    protected HashSet<Vector3> RunRandomWalk(SimpleRandomWalkData parameters, Vector3 _pos)
    {
        var currenPos = _pos;
        HashSet<Vector3> floorPositions = new HashSet<Vector3>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorthrims.SimpleRandomWalk(currenPos, parameters.walkLength);
            //add to floor pos and ensure no dups are added
            floorPositions.UnionWith(path);
            if(parameters.startRandomlyEachIteration)
            {
                currenPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
