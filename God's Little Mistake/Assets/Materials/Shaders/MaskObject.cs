using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : Singleton<MaskObject>
{
    public List<GameObject> ObjMasked;
    void Start()
    {
        for (int i = 0; i < ObjMasked.Count; i++)
        {
            ObjMasked[i].GetComponentInChildren<SpriteRenderer>().material.renderQueue = 3002;


        }
    }

    public void UpdateMaskedObjects()
    {
        for (int i = 0; i < ObjMasked.Count; i++)
        {
            ObjMasked[i].GetComponentInChildren<SpriteRenderer>().material.renderQueue = 3002;


        }
    }

    void Update()
    {

    }
}

