    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShortRange : GameBehaviour
{
    //public float detectionRange = 10f;
    //public float chaseSpeed = 3f;
    //public float roamSpeed = 1.5f;
    //public float attackRange = 2f;
    //public int attackDamage = 10;
    //public float attackCooldown = 2f;
    //public int maxHealth = 100;
    //public float roamingRadius = 5f;
    //public float rotationSpeed = 5f;

    //private float currentHealth;
    //private bool isChasing = false;
    //private bool canAttack = true;
    //private Transform player;
    //private Vector3 roamingPosition;
    //public NavMeshAgent navMeshAgent;
    //private Animator animator;

    //public float MeleeDMG;

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
    }
  
    private void Update()
    {
        //check for the sight and attack range
        if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Die)
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            
            playerInSightRange = Physics.CheckSphere(transform.position, enemyStats.stats.range+1, whatIsPlayer);

            if (playerInAttackRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Attacking;
            if (playerInSightRange && !playerInAttackRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Chase;
            
            if (!playerInSightRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
            
        }


        switch (BaseEnemy.enemyState)
        {
            case BaseEnemy.EnemyState.Patrolling:
                Patroling();
                walkPointRange = 5;
                break;
            case BaseEnemy.EnemyState.Chase:

                Chase();

                break;
            case BaseEnemy.EnemyState.Attacking:
                isPatrolling = false;
                walkPointRange = 2;

                print("attacking");
                Patroling();
                
                agent.isStopped = true;
                PerformAttack(enemyStats.stats.fireRate);
                //do attack

                break;
            case BaseEnemy.EnemyState.Die:
                Patroling();
                //death animation etc
                print("Dead");
                BaseEnemy.Die();
                break;
        }
    }


    void Patroling()
    {
        var pos = SearchWalkPoint();
        agent.SetDestination(pos);

        if (Vector3.Distance(transform.position, pos) < 1f) Patroling();
        //yield return new WaitForSeconds(Random.Range(3, 6));
        //StartCoroutine(PatrolingIE());

    }

    private Vector3 SearchWalkPoint()
    {
        return transform.position + Random.insideUnitSphere * walkPointRange;
    }

    private void PerformAttack(float _firerate)
    {
        if(!canAttack)
        {
            //attack shit
            canAttack = true;
            ExecuteAfterSeconds(_firerate, () => canAttack = false);
        }
    }

    private void Chase()
    {
        print("chase");
        agent.SetDestination(player.transform.position);
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, enemyStats.stats.range);

    //}

}