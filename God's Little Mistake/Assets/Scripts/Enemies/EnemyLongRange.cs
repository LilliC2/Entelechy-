using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLongRange : GameBehaviour
{

    [Header ("Enemy Navigation")]
    bool projectileShot;
    public GameObject firingPoint;
    public GameObject player;



    public NavMeshAgent agent;




    //patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    public bool playerInSightRange, playerInAttackRange, enemyInHitRange;
    public bool isPatrolling;

    public LayerMask whatIsGround, whatIsPlayer;


    BaseEnemy enemyStats;
    BaseEnemy BaseEnemy;

    //wowo
    // Start is called before the first frame update
    void Start()
    {
        //enemyRange = _ED.enemies[0].range;
        //enemySightRange = _ED.enemies[0].range + 0.5f;
        //player = GameObject.Find("Player");
        enemyStats = GetComponent<BaseEnemy>();
        BaseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //check for the sight and attack range
        if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Die)
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, enemyStats.stats.range, whatIsPlayer);
            playerInSightRange = Physics.CheckSphere(transform.position, enemyStats.stats.range + 1, whatIsPlayer);
            if (playerInAttackRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Attacking;
            if (!playerInAttackRange) BaseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;

        }

        //Visual indicator for health
        //HealthVisualIndicator(enemyStats.stats.health, enemyStats.stats.maxHP);


        firingPoint.transform.LookAt(player.transform.position);

        switch(BaseEnemy.enemyState)
        {
            case BaseEnemy.EnemyState.Patrolling:
                if(isPatrolling != true)
                {
                    isPatrolling = true;
                    StartCoroutine(PatrolingIE());
                    walkPointRange = 5;
                }
                break;
            case BaseEnemy.EnemyState.Chase:
                break;
            case BaseEnemy.EnemyState.Attacking:
                isPatrolling = false;
                walkPointRange = 2;
                print("ATTACK");
                FireProjectile(enemyStats.stats.projectilePF, enemyStats.stats.projectileSpeed, enemyStats.stats.fireRate, enemyStats.stats.range);
                break;
            case BaseEnemy.EnemyState.Die:
                StopCoroutine(PatrolingIE());
                //death animation etc
                print("Dead");
                BaseEnemy.Die();
                break;
        }

    }

    IEnumerator PatrolingIE()
    {

        agent.SetDestination(SearchWalkPoint());
        yield return new WaitForSeconds(Random.Range(2, 6));
        StartCoroutine(PatrolingIE());
    }

    private Vector3 SearchWalkPoint()
    {

        return transform.position + Random.insideUnitSphere * walkPointRange;

    }


    void FireProjectile(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {

        if (!projectileShot)
        {

            //Spawn bullet and apply force in the direction of the mouse
            //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
            GameObject bullet = Instantiate(_prefab, firingPoint.transform.position, firingPoint.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);

            Mathf.Clamp(bullet.transform.position.y, 0, 0);

            //This will destroy bullet once it exits the range, aka after a certain amount of time
            Destroy(bullet, _range);

            //Controls the firerate, player can shoot another bullet after a certain amount of time
            projectileShot = true;
            ExecuteAfterSeconds(_firerate, () => projectileShot = false);
        }
    }









    //visualise sight range
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, enemyStats.stats.range);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, enemyStats.stats.range+1);



    }   
}
