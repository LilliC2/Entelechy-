using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : GameBehaviour
{

    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    [SerializeField]
    GameObject parent;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(Humble());
        //ExecuteAfterSeconds(1f, () => surfaces = new NavMeshSurface[] { GetComponentInChildren<NavMeshSurface>() });
        
    }
    void BakeMesh()
    {
     
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BakeMesh();
        }
    }

    IEnumerator Humble()
    {
        yield return new WaitForSeconds(1);
        print("test");
        surfaces = new NavMeshSurface[] { GetComponentInChildren<NavMeshSurface>() };
        BakeMesh();
    }
    

}
