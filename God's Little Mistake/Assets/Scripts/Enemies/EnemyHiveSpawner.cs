using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f;
    public string playerTag = "Player";
    public float activationRange = 10.0f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (IsPlayerInRange())
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Spawn enemy at the position of the spawner.
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    private bool IsPlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= activationRange;
        }
        return false;
    }
}