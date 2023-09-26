using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cannot creat with abstract classes
public abstract class AbstractDungeonGenerator : GameBehaviour
{
    [SerializeField]
    protected TileMapVisualiser tileMapVisualiser = null;

    [SerializeField]
    protected Vector3 startPos = Vector3.zero;


    public void GenerateDungeon()
    {

        tileMapVisualiser.Clear();
        RunProceduralGeneration();
        //navigation.BakeNavMesh();

    }

    /// <summary>
    /// Generates tile map based on algrothim of choosing
    /// </summary>
    protected abstract void RunProceduralGeneration();

}
