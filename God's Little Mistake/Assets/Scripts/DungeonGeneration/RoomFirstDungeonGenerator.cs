using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    //gen paramaters
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;

    //area we split
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;


    Vector3 startRoom;
    Vector3 endRoom;

    public GameObject playerTemp;
    public GameObject endRoomOB;


    //split offset
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;

    //check want to use randomwalk for rooms or boundsint rooms
    [SerializeField]
    private bool randomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        


        var roomsList = ProceduralGenerationAlgorthrims.BinarySpacePartitioning(new BoundsInt(Vector3Int.FloorToInt(startPos),
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector3> floor = new HashSet<Vector3>();
        


        if(randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else floor = CreateSimpleRooms(roomsList);


        List<Vector3> roomCenters = new List<Vector3>();
        foreach (var room in roomsList)
        {
            if(floor.Contains(room.center))
            {
                roomCenters.Add(room.center);
            }
            

        }

        List<List<Vector3>> corridors = ConnectRooms(roomCenters);

        var hashset = new HashSet<Vector3>();

        for (int i = 0; i < corridors.Count; i++)
        {

            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);

            var temp = new HashSet<Vector3>(corridors[i]);

            hashset.UnionWith(temp);
        }

        floor.UnionWith(hashset);

        tileMapVisualiser.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualiser);

        playerTemp.transform.position = new Vector3(startRoom.x, 1, startRoom.y) ;
        endRoomOB.transform.position = new Vector3(endRoom.x, 1, endRoom.y) ;
    }

    

    private List<Vector3> IncreaseCorridorBrush3by3(List<Vector3> corridor)
    {
        List<Vector3> newCorridor = new List<Vector3>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector3(x, y));

                }
            }
        }
        return newCorridor;
    }

    private HashSet<Vector3> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector3> floor = new();

        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = roomBounds.center;

            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

            //account for offset
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset)
                    && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {

                    
                    floor.Add(position);
                }
            }

        }

        return floor;
    }

    private List<List<Vector3>> ConnectRooms(List<Vector3> roomCenters)
    {

        List<List<Vector3>> corridors = new();

        //can use this to get start room and end room

        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];

        startRoom = currentRoomCenter; 

        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            if (roomCenters.Count == 1) endRoom = currentRoomCenter;
            
            Vector3 closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            var newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.Add(newCorridor);
        }
        return corridors;
    }



    private List<Vector3> CreateCorridor(Vector3 currentRoomCenter, Vector3 destination)
    {
        List<Vector3> corridor = new ();

        //this creates corridors that lead to nothing, could fix with dead end function

        var position = currentRoomCenter;
        corridor.Add(position);
        

        if(position.y != destination.y)
        {
            //vertical
            if (destination.y > position.y)
            {
                var distance = destination.y - position.y;

                for (int i = 0; i < distance; i++)
                {
                    position += Vector3.up;


                    corridor.Add(position);
                }

                
            }
            if (destination.y < position.y)
            {
                var distance =  position.y - destination.y;

                for (int i = 0; i < distance; i++)
                {
                    position += Vector3.down;
                    corridor.Add(position);
                }

                
            }
            //horizontal

           
        }

        if (position.x != destination.x)
        {
            if (destination.x < position.x)
            {
                var distance = position.x - destination.x;

                for (int i = 0; i < distance; i++)
                {
                    position += Vector3.left;
                    corridor.Add(position);
                }


            }
            if (destination.x > position.x)
            {
                var distance = destination.x - position.x;

                for (int i = 0; i < distance; i++)
                {
                    position += Vector3.right;
                    corridor.Add(position);
                }


            }
        }


        return corridor;
    }

    private Vector3 FindClosestPointTo(Vector3 currentRoomCenter, List<Vector3> roomCenters)
    {
        Vector3 closest = Vector3.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector3.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }
    private HashSet<Vector3> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector3> floor = new HashSet<Vector3>();

        //to customise each room further, save each room in a hashset and iterate further

        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector3 position = room.min + new Vector3(col, row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }
}
