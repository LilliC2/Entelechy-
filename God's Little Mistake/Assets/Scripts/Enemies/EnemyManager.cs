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
        enemyTypes = new string[] { "_Long_Range", "_Chompers", "_Bullet_Sponge", "_HiveBug",  };
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
            //var enemyTypeR = Random.Range(0, enemyTypes.Length); // last digit excluded

            var r = Random.Range(0.0f, 1.0f);
            int type = 0;

            //bubbles 40%
            if(r>= 0.0f && r<= 40.0f)
            {
                type = 0;
            }
            //chompe 40
            else if(r>= 40.0f && r<= 80.0f)
            {
                type = 1;
            }
            //hive 15
            else if(r>= 80.0f && r<= 95.0f)
            {
                type = 2;
            }
            //sponge 5
            else if(r>= 95 && r<= 1f)
            {
                type = 2;
            }


            GameObject enemy = Instantiate(Resources.Load("Enemy" + enemyTypes[type], typeof(GameObject)), spawnPoint.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

        }

    }
}
