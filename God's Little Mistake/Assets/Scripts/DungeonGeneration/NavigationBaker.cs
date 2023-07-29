using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    [SerializeField]
    GameObject parent;

    // Use this for initialization
    void Start()
    {

        //surfaces = new NavMeshSurface[] { parent.GetComponent<NavMeshSurface>() };
     
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

}
