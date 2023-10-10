using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public string[] enemyTypes;

    public GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        enemyTypes = new string[] { "_Short_Range", "_Long_Range", "_Chompers" };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemiesForLevel()
    {
        //start of dungeon

        //find all spawn points

        //print("Spawn enemies");
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        foreach (var spawnPoint in spawnPoints)
        {
            var enemyTypeR = Random.Range(0, enemyTypes.Length); // last digit excluded

            //print("Spawn: Enemy" + enemyTypes[enemyTypeR]);

            GameObject enemy = Instantiate(Resources.Load("Enemy" + enemyTypes[enemyTypeR], typeof(GameObject)), spawnPoint.transform.position, Quaternion.identity) as GameObject;

            //spawn enemies at appropriate level


        }

    }
}
