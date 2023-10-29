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
            print(spawnPoint.name);
            //var enemyTypeR = Random.Range(0, enemyTypes.Length); // last digit excluded

            //var r = Random.Range(0, 8);
            GameObject enemy = Instantiate(Resources.Load("Enemy" + enemyTypes[1], typeof(GameObject)), spawnPoint.transform.position,new Quaternion(0, 0, 0,0)) as GameObject;

            //chompers
            //if(r <= 4)
            //{
            //    GameObject enemy = Instantiate(Resources.Load("Enemy" + enemyTypes[2], typeof(GameObject)), spawnPoint.transform.position, Quaternion.identity) as GameObject;

            //}
            //else if(r >4 && r <=6)
            //{
            //    GameObject enemy = Instantiate(Resources.Load("Enemy" + enemyTypes[1], typeof(GameObject)), spawnPoint.transform.position, Quaternion.identity) as GameObject;

            //}
            //else
            //{
            //    GameObject enemy = Instantiate(Resources.Load("Enemy" + enemyTypes[0], typeof(GameObject)), spawnPoint.transform.position, Quaternion.identity) as GameObject;

            //}

            //print("Spawn: Enemy" + enemyTypes[enemyTypeR]);


            //spawn enemies at appropriate level


        }

    }
}
