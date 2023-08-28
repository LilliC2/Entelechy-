using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProceduralGenerationAlgorthrims;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector3> floorPositions, TileMapVisualiser tileMapVisualiser)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        foreach (var position in basicWallPositions)
        {
            tileMapVisualiser.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector3> FindWallsInDirections(HashSet<Vector3> floorPositions, List<Vector3> directionList)
    {
        //for straight walls
        HashSet<Vector3> wallPositions = new HashSet<Vector3>();
        foreach (var pos in floorPositions)
        {
            foreach (var direction in directionList)
            {
                //check if position is in floor postions, if not then its a wwall
                var neighbourPosition = pos + direction;
                if(floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }

}
