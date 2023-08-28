    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShortRange : GameBehaviour
{

    [Header("Enemy Navigation")]
    bool projectileShot;
    public GameObject firingPoint;
    public GameObject player;
    public NavMeshAgent agent;


    public float sightRange = 7;
    public float attackRange;

    [SerializeField]
    bool canAttack;
    bool canSee;
    bool attacking = false;



    //patrolling
    //patrolling
    public Vector3 walkPoint;
    public float walkPointRange;


    public LayerMask whatIsGround, whatIsPlayer;


    BaseEnemy enemyStats;
    BaseEnemy BaseEnemy;

    Vector3 target;

    void Start()
    {

        enemyStats = GetComponent<BaseEnemy>();
        BaseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        attackRange = enemyStats.stats.range;
        target = SearchWalkPoint();

    }

    // Update is called once per frame
    void Update()
    {

        ////check for the sight and attack range
        if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Die)
        {
            canSee = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            canAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            //if cant see player, patrol
            if (!canSee) BaseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
            else if (canSee) BaseEnemy.enemyState = BaseEnemy.EnemyState.Chase;

        }

        //Visual indicator for health
        //HealthVisualIndicator(enemyStats.stats.health, enemyStats.stats.maxHP);

        switch (BaseEnemy.enemyState)
        {
            case BaseEnemy.EnemyState.Patrolling:



                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            // Destination reached

                            //new target
                            target = SearchWalkPoint();
                        }
                    }
                }


                agent.SetDestination(target);

                break;
            case BaseEnemy.EnemyState.Chase:

                if (Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange)
                {

                    agent.isStopped = false;

                    agent.SetDestination(player.transform.position);

                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange-1)
                {

                    agent.isStopped = false;
                    Vector3 toPlayer = player.transform.position - transform.position;
                    Vector3 targetPosition = toPlayer.normalized * -5f;

                    agent.SetDestination(targetPosition);


                }
                else
                {

                    agent.isStopped = true;


                    //orbit player

                    //transform.RotateAround(player.transform.position, Vector3.up, 9 * Time.deltaTime);

                }

                // ATTACK

                if (canAttack)
                {
                    //print("Can Attack");
                    PerformAttack(enemyStats.stats.fireRate);

                }


                break;
            case BaseEnemy.EnemyState.Die:

                BaseEnemy.Die();

                break;
        }




    }

    private Vector3 SearchWalkPoint()
    {

        return transform.position + Random.insideUnitSphere * walkPointRange;

    }

    private void PerformAttack(float _firerate)
    {
        if (!attacking)
        {
            //print("Attack");
            //attack shit

            _PC.Hit(enemyStats.stats.dmg);


            attacking = true;
            ExecuteAfterSeconds(_firerate, () => attacking = false);
        }
    }

    //private void Start()
    //{
    //    enemyStats = GetComponent<BaseEnemy>();
    //    BaseEnemy = GetComponent<BaseEnemy>();
    //    agent = GetComponent<NavMeshAgent>();
    //    agent.SetDestination(RandomNavSphere());
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}

    //private void Update()
    //{
    //    if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Die)
    //    {
    //        playerInSightRange = Physics.CheckSphere(transform.position, enemyStats.stats.range + 1, whatIsPlayer);
    //        playerInAttackRange = Physics.CheckSphere(transform.position, enemyStats.stats.range, whatIsPlayer);
    //        if (playerInSightRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Chase;
    //        if (playerInAttackRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Attacking;
    //        else if (!playerInSightRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
    //    }



    //    switch (BaseEnemy.enemyState)
    //    {
    //        case BaseEnemy.EnemyState.Patrolling:
    //            if(agent.remainingDistance <= agent.stoppingDistance)
    //            {
    //                walkPointRange = 5;
    //                agent.SetDestination(RandomNavSphere());
    //            }

    //            break;
    //        case BaseEnemy.EnemyState.Chase:
    //            agent.SetDestination(player.transform.position);

    //            break;
    //        case BaseEnemy.EnemyState.Attacking:

    //            PerformAttack(enemyStats.stats.fireRate);


    //            break;
    //        case BaseEnemy.EnemyState.Die:
    //            //death animation etc
    //            print("Dead");
    //            BaseEnemy.Die();
    //            break;
    //    }
    //}


    //public Vector3 RandomNavSphere()
    //{
    //    Vector3 finalPos = Vector3.zero;
    //    Vector3 randomPos = Random.insideUnitSphere * walkPointRange;
    //    randomPos += transform.position;
    //    if(NavMesh.SamplePosition(randomPos, out NavMeshHit hit, walkPointRange, NavMesh.AllAreas))
    //    {
    //        print("Get new pos");
    //        finalPos = hit.position;
    //    }

    //    return finalPos; 
    //}



    ////private void OnDrawGizmos()
    ////{
    ////    Gizmos.color = Color.red;
    ////    Gizmos.DrawWireSphere(transform.position, attackRange);
    ////    Gizmos.color = Color.yellow;
    ////    Gizmos.DrawWireSphere(transform.position, enemyStats.stats.range);

    ////}

}