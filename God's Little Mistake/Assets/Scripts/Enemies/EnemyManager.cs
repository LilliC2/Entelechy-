using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public string[] enemyTypes;

    // Start is called before the first frame update
    void Start()
    {
        enemyTypes = new string[] { "_Short_Range", "_Long_Range" };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemiesForLevel()
    {
        //start of dungeon

        //find all spawn points

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        foreach (var spawnPoint in spawnPoints)
        {
            var enemyTypeR = Random.Range(0, 2); // last digit excluded

            GameObject enemy = Instantiate(Resources.Load("Enemy" + enemyTypes[enemyTypeR], typeof(GameObject))) as GameObject;

            //spawn enemies at appropriate level


        }

    }
}
