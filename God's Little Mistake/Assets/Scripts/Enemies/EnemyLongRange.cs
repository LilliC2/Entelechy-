using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLongRange : GameBehaviour
{
    bool projectileShot;
    public GameObject firingPoint;

    public GameObject player;

    public enum EnemyState
    {
        Patrolling, Chase, Attacking, Die
    }

    EnemyState enemyState;

    public NavMeshAgent agent;

    //states
    bool walking;
    bool idle;
    bool rotateDetermined = false;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public bool playerInSightRange, playerInAttackRange, enemyInHitRange;

    public LayerMask whatIsGround, whatIsPlayer;



    float enemySpeed;
    float enemyRange;
    float enemySightRange;


    // Start is called before the first frame update
    void Start()
    {

        enemySpeed = _ED.enemies[0].speed;
        enemyRange = _ED.enemies[0].range;
        enemySightRange = _ED.enemies[0].range + 0.5f;
        //player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //check for the sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, enemyRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, enemySightRange, whatIsPlayer);

        if (playerInAttackRange) enemyState = EnemyState.Attacking;
        if (!playerInAttackRange) enemyState = EnemyState.Patrolling;

        firingPoint.transform.LookAt(player.transform.position);

        switch(enemyState)
        {
            case EnemyState.Patrolling:
                StartCoroutine(PatrolingIE());
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attacking:
                print("ATTACK");
                FireProjectile(_ED.enemies[0].projectilePF, _ED.enemies[0].projectileSpeed, _ED.enemies[0].fireRate, _ED.enemies[0].range);
                break;
            case EnemyState.Die:
                break;
        }


    }

    IEnumerator PatrolingIE()
    {
        rotateDetermined = false;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            gameObject.GetComponent<NavMeshAgent>().speed = enemySpeed;
        }


        //calculate distance to walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpouint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            float result = RandomFloatBetweenTwoFloats(2f, 5f);
            yield return new WaitForSeconds(result);
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomZ);

        //check if new walkpoint is on ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Projectile"))
        {
            print("hit");
            //Add hit code here;

            //destroy bullet that hit it
            Destroy(collision.gameObject);
        }
    }

    //visualise sight range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemySightRange);



    }   
}
