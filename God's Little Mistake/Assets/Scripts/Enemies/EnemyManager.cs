using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemiesForLevel()
    {
        // Find all spawn points
        GameObject[] spawnPointsArray = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        List<GameObject> spawnPoints = spawnPointsArray.ToList();

        // Now you can work with the List<GameObject> as needed
    }
}