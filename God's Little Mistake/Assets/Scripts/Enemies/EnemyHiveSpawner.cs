using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyHiveSpawner : GameBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2.0f;
    public string playerTag = "Player";
    public float activationRange = 10.0f;

    EnemyHiveBug enemyHiveBug;

    [SerializeField]
    ParticleSystem spawningPS;
    Animator anim;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        enemyHiveBug = GetComponent<EnemyHiveBug>();
        anim = enemyHiveBug.anim;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (IsPlayerInRange())
            {
                gameObject.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1f);
                ExecuteAfterSeconds(1, () => SpawnEnemy());

            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        anim.SetTrigger("Spawning");
        // Spawn enemy at the position of the spawner.
        gameObject.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.5f);
        spawningPS.Play();
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        _EM.enemiesSpawned.Add(enemy);

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