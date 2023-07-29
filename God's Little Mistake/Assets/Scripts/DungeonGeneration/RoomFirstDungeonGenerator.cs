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




    //split offset
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;

    private bool hasReachedX, hasReachedZ;

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
        floor = CreateSimpleRooms(roomsList);

        List<Vector3> roomCenters = new List<Vector3>();
        foreach (var room in roomsList)
        {
            roomCenters.Add(room.center);

        }

        HashSet<Vector3> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tileMapVisualiser.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualiser);
        
    }

    private HashSet<Vector3> ConnectRooms(List<Vector3> roomCenters)
    {
        HashSet<Vector3> corridors = new HashSet<Vector3>();

        //can use this to get start room and end room

        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            Vector3 closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector3> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }



    private HashSet<Vector3> CreateCorridor(Vector3 currentRoomCenter, Vector3 destination)
    {
        HashSet<Vector3> corridor = new HashSet<Vector3>();
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

            

        //while (position.y != destination.y)
        //{
        //    if (destination.y > position.y)
        //    {
        //        position += Vector3.up;
        //    }
        //    else if (destination.y < position.y)
        //    {
        //        position += Vector3.down;
        //    }
        //    corridor.Add(position);
        //}
        //while (position.x != destination.x)
        //{
        //    if (destination.x > position.x)
        //    {
        //        position += Vector3.right;
        //    }
        //    else if (destination.x < position.x)
        //    {
        //        position += Vector3.left;
        //    }
        //    corridor.Add(position);
        //}
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
