using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHiveBug : MonoBehaviour
{
    public string playerTag = "Player";
    public float patrolSpeed = 2.0f;
    public float patrolRadius = 10.0f;
    public float avoidanceRange = 5.0f;

    private NavMeshAgent agent;
    private Vector3 initialPosition;
    private bool isPlayerInRange = false;

    public EnemyHiveSpawner spawner;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        agent.angularSpeed = 0;
        EnsureCorrectRotation();
        StartCoroutine(Patrol());
    }

    private void EnsureCorrectRotation()
    {
        if (Mathf.Abs(transform.eulerAngles.x - 45f) > 0.01f)
        {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.x = 45f;
            transform.eulerAngles = newRotation;
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            if (!isPlayerInRange)
            {
                agent.speed = patrolSpeed;
                agent.SetDestination(GetRandomPatrolPosition());
            }

            yield return new WaitForSeconds(2.0f);
        }
    }

    private Vector3 GetRandomPatrolPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += initialPosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
        return hit.position;
    }

    private void Update()
    {
        EnsureCorrectRotation();

        if (IsPlayerInRange())
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag(playerTag).transform.position;
            Vector3 directionToPlayer = transform.position - playerPosition;

            if (directionToPlayer.magnitude < avoidanceRange)
            {
                Vector3 avoidancePosition = transform.position + directionToPlayer.normalized * avoidanceRange;
                agent.SetDestination(avoidancePosition);
                isPlayerInRange = true;
            }
            else
            {
                isPlayerInRange = false;
            }
        }
    }

    private bool IsPlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= avoidanceRange;
        }
        return false;
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.OnEnemyDeath();
        }
    }
}