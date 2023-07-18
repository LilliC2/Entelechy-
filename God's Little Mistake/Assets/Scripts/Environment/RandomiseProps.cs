using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomiseProps : GameBehaviour
{
    public GameObject[] props;

    public GameObject parent;
    public int numOfPropsSpawns;
    public int spawnRadius = 5;
    Vector3 spawnPoint;
    bool isSpawningProps = true;
    public GameObject centerOfSpawningSphere;

    // Start is called before the first frame update
    void Start()
    {
        RandomiseEnvionmentProps();
        

    }


    public void RandomiseEnvionmentProps()
    {
        if (isSpawningProps)
        {
            for (int i = 0; i < numOfPropsSpawns; i++)
            {
                Vector3 ranDir = centerOfSpawningSphere.transform.position + Random.insideUnitSphere * spawnRadius;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(ranDir, out hit, spawnRadius, NavMesh.AllAreas))
                {
                    spawnPoint = hit.position;
                    Instantiate(props[RandomIntBetweenTwoInt(0, props.Length)], spawnPoint, Quaternion.identity, parent.transform);
                }
                print("i=" + i + " numofprops spawn " + numOfPropsSpawns);

                if (i == numOfPropsSpawns - 1) isSpawningProps = false;
            }


        }
    }
}
