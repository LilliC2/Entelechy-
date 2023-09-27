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

    bool animationPlayed;
    bool runAway;
    float runAwaySpeed;
    float normalSpeed;

    public float sightRange = 7;
    public float attackRange;

    bool canAttack;
    bool canSee;

    ////patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    public LayerMask whatIsGround, whatIsPlayer;

    public BaseEnemy enemyStats;
    BaseEnemy BaseEnemy;
    Vector3 target;

    [Header("Enemy Sprites")]
    public GameObject frontOB;
    public GameObject backOB;
    public GameObject rightSideOB;
    public GameObject leftSideOB;

    [SerializeField]

    Animator frontAnim;
    [SerializeField]

    Animator backAnim;
    [SerializeField]

    Animator rightSideAnim;
    [SerializeField]
    Animator leftSideAnim;

    void Start()
    {
        //enemyRange = _ED.enemies[0].range;
        //enemySightRange = _ED.enemies[0].range + 0.5f;
        //player = GameObject.Find("Player");
        enemyStats = GetComponent<BaseEnemy>();
        BaseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        frontAnim = frontOB.GetComponentInChildren<Animator>();
        backAnim = backOB.GetComponentInChildren<Animator>();
        rightSideAnim = rightSideOB.GetComponentInChildren<Animator>();
        leftSideAnim = leftSideOB.GetComponentInChildren<Animator>();
        
        normalSpeed = enemyStats.stats.speed;
        runAwaySpeed = enemyStats.stats.speed*2;

        attackRange = enemyStats.stats.range;
        target = SearchWalkPoint();

    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = enemyStats.stats.speed;

        ////check for the sight and attack range
        if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Charmed || BaseEnemy.enemyState != BaseEnemy.EnemyState.Die) 
        {
            canSee = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            canAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            //if cant see player, patrol
            if (!canSee) BaseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
            else if (canSee) BaseEnemy.enemyState = BaseEnemy.EnemyState.Chase;
            //else agent.isStopped = tru;

        }
        print(canAttack);

        //Change sprites based on direction

        #region Turning Sprites
        //if angle is between 136 and 45, backwards
        var heading = Mathf.Atan2(transform.right.z, transform.right.x) * Mathf.Rad2Deg;
        if(heading >= -45 && heading <=45)
        {
            frontOB.transform.GetChild(0).gameObject.SetActive(false);
            rightSideOB.transform.GetChild(0).gameObject.SetActive(false);
            leftSideOB.transform.GetChild(0).gameObject.SetActive(false);
            backOB.transform.GetChild(0).gameObject.SetActive(true);

        }

        //if angle is between 46 and 315, right side
        if(heading >= 46 && heading <= 135)
        {
            frontOB.transform.GetChild(0).gameObject.SetActive(false);
            rightSideOB.transform.GetChild(0).gameObject.SetActive(true);
            leftSideOB.transform.GetChild(0).gameObject.SetActive(false);
            backOB.transform.GetChild(0).gameObject.SetActive(false);
        }

        //if angle is between 316 and 225, forwards
        if (heading >= 136 && heading >= -135)
        {
            frontOB.transform.GetChild(0).gameObject.SetActive(true);
            rightSideOB.transform.GetChild(0).gameObject.SetActive(false);
            leftSideOB.transform.GetChild(0).gameObject.SetActive(false);
            backOB.transform.GetChild(0).gameObject.SetActive(false);
        }

        //if angle is between 226 and 135, left side
        if (heading >= -136 && heading <= -45)
        {
            frontOB.transform.GetChild(0).gameObject.SetActive(false);
            rightSideOB.transform.GetChild(0).gameObject.SetActive(false);
            leftSideOB.transform.GetChild(0).gameObject.SetActive(true);
            backOB.transform.GetChild(0).gameObject.SetActive(false);
        }




        #endregion

        #region Animating Sprites

        //check if walking
        if (agent.velocity.magnitude > 0.1f)
        {
            frontAnim.SetBool("Walking", true);
            backAnim.SetBool("Walking", true);
            leftSideAnim.SetBool("Walking", true);
            rightSideAnim.SetBool("Walking", true);
        }
        else
        {
            frontAnim.SetBool("Walking", false);
            backAnim.SetBool("Walking", false);
            leftSideAnim.SetBool("Walking", false);
            rightSideAnim.SetBool("Walking", false);

        }


        #endregion
        //Visual indicator for health
        //HealthVisualIndicator(enemyStats.stats.health, enemyStats.stats.maxHP);


        firingPoint.transform.LookAt(player.transform.position);


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
                            target = SearchWalkPoint();
                            //new target
                            
                        }
                    }
                }


                agent.SetDestination(target);

                break;
            case BaseEnemy.EnemyState.Chase:


                if (Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange*2)
                {
                    //chase player
                    agent.isStopped = false;
                    enemyStats.stats.speed = normalSpeed;
                    print("Chase player");

                    runAway = false;
                    agent.SetDestination(player.transform.position);

                }
                else if(Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange  && 
                    Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange +3)
                {
                    if(!runAway)
                    {
                        frontAnim.SetBool("Walking", false);
                        backAnim.SetBool("Walking", false);
                        leftSideAnim.SetBool("Walking", false);
                        rightSideAnim.SetBool("Walking", false);

                        print("Stop to attack");
                        agent.isStopped = true;

                        transform.LookAt(player.transform.position);
                        if (!animationPlayed)
                        {
                            animationPlayed = true;
                            PlayAttackAnimation();
                            ExecuteAfterSeconds(enemyStats.stats.fireRate, () => ResetAttackAnimation());
                        }
                    }

                    

                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange)
                {
                    runAway = true;

                    enemyStats.stats.speed = runAwaySpeed;

                    print("Back awawy");
                    //run away from player
                    agent.isStopped = false;
                    Vector3 toPlayer = player.transform.position - transform.position;
                    Vector3 targetPosition = toPlayer.normalized * -10f;

                    agent.SetDestination(targetPosition);


                }
                //else
                //{

                //    agent.isStopped = true;


                //    orbit player

                //    transform.RotateAround(player.transform.position, Vector3.up, 9 * Time.deltaTime);

                //}

                // ATTACK
                


                break;
            case BaseEnemy.EnemyState.Die:

                BaseEnemy.Die();

                break;
            case BaseEnemy.EnemyState.Attacking:


                

                break;
        }




    }

    void ResetAttackAnimation()
    {

        print("Reset aniamtions");
        animationPlayed = false;
        
    }

    private Vector3 SearchWalkPoint()
    {

        return transform.position + Random.insideUnitSphere * walkPointRange;

    }


    public void FireProjectile(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {

        if (!projectileShot)
        {
            
            //Spawn bullet and apply force in the direction of the mouse
            //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
            GameObject bullet = Instantiate(_prefab, firingPoint.transform.position, firingPoint.transform.rotation);
            bullet.GetComponent<EnemyProjectile>().dmg = enemyStats.stats.dmg;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);

            Mathf.Clamp(bullet.transform.position.y, 0, 0);

            //This will destroy bullet once it exits the range, aka after a certain amount of time
            Destroy(bullet, _range);

            //Controls the firerate, player can shoot another bullet after a certain amount of time
            projectileShot = true;
            ExecuteAfterSeconds(_firerate, () => projectileShot = false);
        }

    }


    void PlayAttackAnimation()
    {
        print("Attack anim");
        frontAnim.SetBool("Walking", false);
        backAnim.SetBool("Walking", false);
        leftSideAnim.SetBool("Walking", false);
        rightSideAnim.SetBool("Walking", false);

        if (frontOB.activeSelf == true) frontAnim.SetTrigger("Attack");
        if(backOB.activeSelf == true) backAnim.SetTrigger("Attack");
        if(rightSideOB.activeSelf == true) rightSideAnim.SetTrigger("Attack");
        if(leftSideOB.activeSelf == true) leftSideAnim.SetTrigger("Attack");


    }

    //visualise sight range
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(player.transform.position, 5);
    //    //Gizmos.color = Color.yellow;
    //    //Gizmos.DrawWireSphere(transform.position, enemyStats.stats.range+1);



    //}   
}
