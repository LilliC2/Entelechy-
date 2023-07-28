using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualiser : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    GameObject parent;
    [SerializeField]
    private TileBase floorTile; //change to gameobject??? can change to array later
    public GameObject floorOB; //change to gameobject??? can change to array later

    public void PaintFloorTiles(IEnumerable<Vector3> _floorPositions)
    {

        PaintTiles(_floorPositions, floorTilemap, floorTile); 
    }

    private void PaintTiles(IEnumerable<Vector3> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector3 position)
    {
        HashSet<Vector3> floorPositions3D = new HashSet<Vector3>();
        //get pos
        //var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        //var pos = new Vector3(tilePosition.x, 1, tilePosition.y);


        //floorPositions3D.UnionWith(pos);

        //print("making a tile at " + pos);

        Instantiate(floorOB, new Vector3(position.x,1f, position.y), Quaternion.identity,parent.transform);
        //paint
        
        //tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        while (parent.transform.childCount > 0)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
        //floorTilemap.ClearAllTiles();
    }
}
