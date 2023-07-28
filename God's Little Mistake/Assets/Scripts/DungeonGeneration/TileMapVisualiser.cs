using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualiser : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    GameObject parentFloor;
    [SerializeField]
    public GameObject floorOB, wallTopOB; 

    public void PaintFloorTiles(IEnumerable<Vector3> _floorPositions)
    {

        PaintTiles(_floorPositions, floorTilemap); 
    }

    internal void PaintSingleBasicWall(Vector3 position)
    {
        PaintSingleTile(wallTilemap, wallTopOB, position);
    }

    private void PaintTiles(IEnumerable<Vector3> positions, Tilemap tilemap)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, floorOB, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, GameObject tile,Vector3 position)
    {
        //get pos
        //var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        //var pos = new Vector3(tilePosition.x, 1, tilePosition.y);


        //floorPositions3D.UnionWith(pos);

        //print("making a tile at " + pos);

        Instantiate(tile, new Vector3(position.x,1f, position.y), Quaternion.identity,tilemap.transform);
        //paint
        
        //tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        Tilemap[] tileMaps = FindObjectsOfType<Tilemap>();

        foreach (var tileMap in tileMaps)
        {
            while (tileMap.transform.childCount > 0)
            {
                DestroyImmediate(tileMap.transform.GetChild(0).gameObject);
            }
        }

        
        //floorTilemap.ClearAllTiles();
    }
}
