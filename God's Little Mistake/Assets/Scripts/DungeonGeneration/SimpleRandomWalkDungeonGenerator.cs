using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10; //number of times the algrothim is run
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;

    [SerializeField]
    private TileMapVisualiser tileMapVisualiser;

    public void RunProceduralGeneration()
    {
        //create floor position
        HashSet<Vector2Int> floorPositions = RunRandomWalk();

        //temp visualtion
        tileMapVisualiser.PaintFloorTiles(floorPositions);
    }

    //private method
    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currenPos = startPos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
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
