    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShortRange : GameBehaviour
{

    [Header("Enemy Navigation")]
    bool canAttack;
    public GameObject player;

    public NavMeshAgent agent;

    //patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    public float attackRange =1;

    public bool playerInSightRange, playerInAttackRange, playerInChaseRange;
    public bool isPatrolling;

    public LayerMask whatIsGround, whatIsPlayer;

    BaseEnemy enemyStats;
    BaseEnemy BaseEnemy;


    private void Start()
    {
        enemyStats = GetComponent<BaseEnemy>();
        BaseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(RandomNavSphere());
        player = GameObject.FindGameObjectWithTag("Player");
    }
  
    private void Update()
    {
        if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Die)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, enemyStats.stats.range + 1, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, enemyStats.stats.range, whatIsPlayer);
            if (playerInSightRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Chase;
            if (playerInAttackRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Attacking;
            else if (!playerInSightRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
        }
            


        switch (BaseEnemy.enemyState)
        {
            case BaseEnemy.EnemyState.Patrolling:
                if(agent.remainingDistance <= agent.stoppingDistance)
                {
                    walkPointRange = 5;
                    agent.SetDestination(RandomNavSphere());
                }
                    
                break;
            case BaseEnemy.EnemyState.Chase:
                agent.SetDestination(player.transform.position);
                
                break;
            case BaseEnemy.EnemyState.Attacking:
                
                PerformAttack(enemyStats.stats.fireRate);


                break;
            case BaseEnemy.EnemyState.Die:
                //death animation etc
                print("Dead");
                BaseEnemy.Die();
                break;
        }
    }


    public Vector3 RandomNavSphere()
    {
        Vector3 finalPos = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * walkPointRange;
        randomPos += transform.position;
        if(NavMesh.SamplePosition(randomPos, out NavMeshHit hit, walkPointRange, NavMesh.AllAreas))
        {
            print("Get new pos");
            finalPos = hit.position;
        }

        return finalPos; 
    }

    private void PerformAttack(float _firerate)
    {
        if(!canAttack)
        {
            print("Attack");
            //attack shit

            player.GetComponent<PlayerController>().health -= enemyStats.stats.dmg;

            canAttack = true;
            ExecuteAfterSeconds(_firerate, () => canAttack = false);
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, enemyStats.stats.range);

    //}

}