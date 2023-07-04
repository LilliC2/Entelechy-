using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShortRange : GameBehaviour
{
    public float detectionRange = 10f;
    public float chaseSpeed = 3f;
    public float roamSpeed = 1.5f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 2f;
    public int maxHealth = 100;
    public float roamingRadius = 5f;
    public float rotationSpeed = 5f;

    private int currentHealth;
    private bool isChasing = false;
    private bool canAttack = true;
    private Transform player;
    private Vector3 roamingPosition;
    public NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStats = GetComponent<BaseEnemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GenerateRoamingPosition();
    }

    BaseEnemy enemyStats;

    public enum EnemyState
    {
        Patrolling, Chase, Attacking, Die
    }

    public EnemyState enemyState;
    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Patrolling:
                if (Vector3.Distance(transform.position, player.position) <= detectionRange)
                {
                    isChasing = true;
                    enemyState = EnemyState.Chase;
                }
                else
                {
                    isChasing = false;
                    Roam();
                }
                break;
            case EnemyState.Chase:
                if (Vector3.Distance(transform.position, player.position) <= attackRange)
                {
                    enemyState = EnemyState.Attacking;
                    Attack();
                }
                else if (Vector3.Distance(transform.position, player.position) > detectionRange)
                {
                    enemyState = EnemyState.Patrolling; 
                }
                else
                {
                    Chase();
                }
                break;
            case EnemyState.Attacking:
                break;
            case EnemyState.Die:
                // Death animation, etc.
                print("Dead");
                Destroy(this.gameObject);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    void Hit()
    {
        if (enemyStats.stats.health != 0)
        {
            enemyStats.stats.health -= _PC.dmg;
            print(enemyStats.stats.health);
            StopAllCoroutines();
        }
        else
        {
            Roam();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
           
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        canAttack = false;

        while (enemyState == EnemyState.Attacking)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                Debug.Log("Enemy performs melee attack!");
                // player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }

            yield return new WaitForSeconds(attackCooldown);
        }

        canAttack = true;
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy defeated!");

        Destroy(gameObject);
    }

    private void Chase()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            enemyState = EnemyState.Attacking; 
            Attack();
        }
        else if (distanceToPlayer > detectionRange)
        {
            enemyState = EnemyState.Patrolling; 
        }
        else
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
    private void Roam()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            GenerateRoamingPosition();
            navMeshAgent.SetDestination(roamingPosition);
        }
    }

    private void GenerateRoamingPosition()
    {
        roamingPosition = transform.position + Random.insideUnitSphere * roamingRadius;
        roamingPosition.y = transform.position.y;

        NavMeshHit navHit;
        NavMesh.SamplePosition(roamingPosition, out navHit, roamingRadius, -1);
        roamingPosition = navHit.position;
    }
}