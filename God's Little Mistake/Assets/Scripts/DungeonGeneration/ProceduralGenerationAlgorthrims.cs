using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorthrims
{
    

    //might have to have vector2int

    //Hashset allows you to not process the same tile at the same time
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int _startPos, int _walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(_startPos);
        var prevPos = _startPos;

        //Move one step in random direction
        for (int i = 0; i < _walkLength; i++)
        {
            //random direction
            var newPos = prevPos + Direction2D.GetRandomCardinalDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    /// <summary>
    /// Get random direction
    /// </summary>
    public static class Direction2D
    {
        //cardinal directions
        public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
        {
            new Vector2Int(0,1), //up
            new Vector2Int(1,0), //right
            new Vector2Int(0,-1), //down
            new Vector2Int(-1,0) //down

        };

        //get the direction
        public static Vector2Int GetRandomCardinalDirection()
        {

            return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
        }

    }
}
