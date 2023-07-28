using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ProceduralGenerationAlgorthrims;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;


    protected override void RunProceduralGeneration()
    {
        CorridorFirstDungeonGeneration();
    }

    private void CorridorFirstDungeonGeneration()
    {
        HashSet<Vector3> floorPos = new HashSet<Vector3>();
        HashSet<Vector3> potentialRoomPos = new HashSet<Vector3>();

        List<List<Vector3>> corridors = CreateCorridors(floorPos, potentialRoomPos);

        HashSet<Vector3> roomPositions = CreateRooms(potentialRoomPos);

        //find dead ends
        List<Vector3> deadEnds = FindAllDeadEnds(floorPos);

        //add rooms to dead ends
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPos.UnionWith(roomPositions);

        //widen corridor by 1
        for (int i = 0; i < corridors.Count; i++)
        {
            //corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPos.UnionWith(corridors[i]);
        }

        tileMapVisualiser.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualiser);
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

    private List<Vector3> IncreaseCorridorSizeByOne(List<Vector3> corridor)
    {
        //list does have dupes!!!
        List<Vector3> newCorridor = new List<Vector3>();
        Vector3 previousDirection = Vector3.zero; //helpd find corners
        for (int i = 1; i < corridor.Count; i++)
        {
            //take first and second tile and find direction
            Vector3 directionFromCell = corridor[i] - corridor[i - 1];
            //direction is different, it is a corner
            if(previousDirection!=Vector3.zero && directionFromCell != previousDirection)
            {
                //handle corner
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector3(x, y));

                    }
                    previousDirection = directionFromCell;
                }
            }
            else
            {
                previousDirection = directionFromCell;

                //add a single cell in the direction + 90 degress
                Vector3 newCorriderTileOffset = GetDirection90From(directionFromCell);
                //add original tile and offset
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorriderTileOffset);
            }
        }

        return newCorridor;
    }

    private Vector3 GetDirection90From(Vector3 direction)
    {
        if (direction == Vector3.up)
            return Vector3.right;
        if (direction == Vector3.right)
            return Vector3.down;
        if (direction == Vector3.down)
            return Vector3.left;
        if (direction == Vector3.left)
            return Vector3.up;
        return Vector3.zero; //just in case
    }

    private void CreateRoomsAtDeadEnd(List<Vector3> deadEnds, HashSet<Vector3> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            //check if room is already there
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }

        
    }

    private List<Vector3> FindAllDeadEnds(HashSet<Vector3> floorPos)
    {
        //loop through floor pos
        List<Vector3> deadEnds = new List<Vector3>();

        foreach (var position in floorPos)
        {
            int neighboursCount = 0;
            //loop through each direction and check if neighbour position is in floor pos, if neighbour only in 1 position we found dead end
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPos.Contains(position + direction))
                    neighboursCount++;
            }
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector3> CreateRooms(HashSet<Vector3> potentialRoomPos)
    {
        HashSet<Vector3> roomPositions = new HashSet<Vector3>();

        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);

        //randomise which points gets rooms
        List<Vector3> roomsToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList(); //GUID = global unquie id

        foreach (var roomPos in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters,roomPos);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private List<List<Vector3>> CreateCorridors(HashSet<Vector3> floorPos, HashSet<Vector3> potentialRoomPos)
    {
        var currentPos = startPos;
        potentialRoomPos.Add(currentPos);
        List < List < Vector3 >> corridors = new List<List<Vector3>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var path = ProceduralGenerationAlgorthrims.RandomWalkCorridor(currentPos, corridorLength);

            corridors.Add(path);

            //ensure they are connected
            currentPos = path[path.Count - 1];

            //add each end of corridor to potential rooms
            potentialRoomPos.Add(currentPos);
            floorPos.UnionWith(path);
        }
        return corridors;
    }
}
