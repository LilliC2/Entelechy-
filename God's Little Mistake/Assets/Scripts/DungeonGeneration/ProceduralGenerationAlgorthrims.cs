using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorthrims
{
    

    //might have to have vector2int

    //Hashset allows you to not process the same tile at the same time
    public static HashSet<Vector3> SimpleRandomWalk(Vector3 _startPos, int _walkLength)
    {
        HashSet<Vector3> path = new HashSet<Vector3>();

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
        public static List<Vector3> cardinalDirectionList = new List<Vector3>
        {
            new Vector3 (0,1), //up
            new Vector3(1,0), //right
            new Vector3(0,-1), //down
            new Vector3(-1,0) //down

        };

        //get the direction
        public static Vector3 GetRandomCardinalDirection()
        {

            return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
        }

    }
}
