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

    public enum EnemyState
    {
        Patrolling, Chase, Attacking, Die
    }

    public EnemyState enemyState;

    public NavMeshAgent agent;




    //patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    public bool playerInSightRange, playerInAttackRange, enemyInHitRange;
    public bool isPatrolling;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject image;

    BaseEnemy enemyStats;

    //wowo
    // Start is called before the first frame update
    void Start()
    {
        //enemyRange = _ED.enemies[0].range;
        //enemySightRange = _ED.enemies[0].range + 0.5f;
        //player = GameObject.Find("Player");
        image = GameObject.Find("Image");
        enemyStats = GetComponent<BaseEnemy>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //check for the sight and attack range
        if (enemyState != EnemyState.Die)
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, enemyStats.stats.range, whatIsPlayer);
            playerInSightRange = Physics.CheckSphere(transform.position, enemyStats.stats.range + 1, whatIsPlayer);
            if (playerInAttackRange) enemyState = EnemyState.Attacking;
            if (!playerInAttackRange) enemyState = EnemyState.Patrolling;

        }

        //Health Manager
        if (enemyStats.stats.health <= 0)
        {
            enemyState = EnemyState.Die;
        }

        //Visual indicator for health
        HealthVisualIndicator(enemyStats.stats.health, enemyStats.stats.health);


        firingPoint.transform.LookAt(player.transform.position);

        switch(enemyState)
        {
            case EnemyState.Patrolling:
                if(isPatrolling != true)
                {
                    isPatrolling = true;
                    StartCoroutine(PatrolingIE());
                    walkPointRange = 5;
                }
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attacking:
                isPatrolling = false;
                walkPointRange = 2;
                print("ATTACK");
                FireProjectile(enemyStats.stats.projectilePF, enemyStats.stats.projectileSpeed, enemyStats.stats.fireRate, enemyStats.stats.range);
                break;
            case EnemyState.Die:
                StopCoroutine(PatrolingIE());
                //death animation etc
                print("Dead");
                Destroy(this.gameObject);
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

    void HealthVisualIndicator(float _health, float _maxHP)
    {
        var currentHPpercent = (_health / _maxHP) * 100;

        print(currentHPpercent);

        float H, S, V;

        Color.RGBToHSV(image.GetComponent<Renderer>().material.color, out H, out S, out V);

        image.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, currentHPpercent, V);
    }

    void Hit()
    {
        if (enemyStats.stats.health > 0)
        {
            enemyStats.stats.health -= _PC.dmg;
            print(enemyStats.stats.health);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Projectile"))
        {
            print("hit");
            //Add hit code here;
            Hit();

            //destroy bullet that hit it
            Destroy(collision.gameObject);
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
