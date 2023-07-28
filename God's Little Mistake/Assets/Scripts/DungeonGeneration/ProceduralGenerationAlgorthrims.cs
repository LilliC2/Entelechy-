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

    public static List<Vector3> RandomWalkCorridor(Vector3 _startPos, int _corridorLength)
    {
        List<Vector3> corridor = new List<Vector3>();
        var direction = Direction2D.GetRandomCardinalDirection();

        var currentPos = _startPos;

        //add start pos
        corridor.Add(currentPos);

        for (int i = 0; i < _corridorLength; i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;

        //get last position in path

        //return path created
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) //min width and height of splits
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();

        //take object from queue
        roomsQueue.Enqueue(spaceToSplit);

        while(roomsQueue.Count >0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth) //check if room can be split
            {
                if(Random.value<0.5f)
                {
                    //horizontal split
                    //check if big enough
                    if(room.size.y >= minHeight*2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);

                        
                        
                    }
                    //check if can split vertically
                    else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //if cannot be split at all but can hold room
                    else if(room.size.x >=minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    //vertical split
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //check if can split horizontally
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    //if cannot be split at all but can hold room
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }

                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x); //pick split spot

        //define bounds
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.min.y, room.min.z));
        BoundsInt room2 = new BoundsInt( new Vector3Int(room.min.x + xSplit, room.min.y,room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y,room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y); //pick split spot

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.min.x,ySplit, room.min.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
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
