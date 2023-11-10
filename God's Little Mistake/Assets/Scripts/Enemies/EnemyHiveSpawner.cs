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
                // Spawn one enemy at a time.
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (existingEnemy != null)
        {
            // Spawn enemy at the position of the spawner.
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // Set the spawner reference in the spawned enemy.
            EnemyHiveBug enemyScript = newEnemy.GetComponent<EnemyHiveBug>();
            if (enemyScript != null)
            {
                enemyScript.spawner = this;
            }

            currentEnemyCount++;
        }
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;
    }
}