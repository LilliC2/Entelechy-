using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f; 
    public int maxEnemyCount = 5; 
    public EnemyHiveBug existingEnemy; 

    private int currentEnemyCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemyCount)
            {
                for (int i = currentEnemyCount; i < maxEnemyCount; i++)
                {
                    SpawnEnemy();
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (existingEnemy != null)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, existingEnemy.transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyHiveBug>().spawner = this;
            currentEnemyCount++;
        }
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;
    }
}
