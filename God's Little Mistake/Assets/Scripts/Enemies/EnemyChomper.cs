using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChomper : GameBehaviour
{
    [Header("Enemy Navigation")]
    bool projectileShot;
    GameObject firingPoint;
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;

    bool animationPlayed;

    public float sightRange = 7;
    public float attackRange;

    [SerializeField]
    bool canAttack;
    bool canSee;
    bool attacking = false;

    float jumpSpeed;
    float normalSpeed;

    bool jumpingBack;
    //patrolling
    //patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    [SerializeField]
    GameObject runningParticleGO;
    [SerializeField]
    ParticleSystem runningParticle;

    public LayerMask whatIsGround, whatIsPlayer;

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


    public BaseEnemy enemyStats;
    BaseEnemy BaseEnemy;

    Vector3 target;

    void Start()
    {

        enemyStats = GetComponent<BaseEnemy>();
        BaseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        frontAnim = frontOB.GetComponentInChildren<Animator>();
        backAnim = backOB.GetComponentInChildren<Animator>();
        rightSideAnim = rightSideOB.GetComponentInChildren<Animator>();
        leftSideAnim = leftSideOB.GetComponentInChildren<Animator>();

        attackRange = enemyStats.stats.range;
        target = SearchWalkPoint();

        normalSpeed = enemyStats.stats.speed;
        jumpSpeed = enemyStats.stats.speed*1.5f;

    }

    // Update is called once per frame
    void Update()
    {
        runningParticleGO.transform.rotation = gameObject.transform.rotation;

        agent.speed = enemyStats.stats.speed;


        ////check for the sight and attack range
        if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Charmed)
        {
            if (BaseEnemy.enemyState != BaseEnemy.EnemyState.Die)
            {
                canSee = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                canAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                //if cant see player, patrol
                if (!canSee) BaseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
                else if (canSee) BaseEnemy.enemyState = BaseEnemy.EnemyState.Chase;
            }
        }

        #region Turning Sprites
        //if angle is between 136 and 45, backwards
        var heading = Mathf.Atan2(transform.right.z, transform.right.x) * Mathf.Rad2Deg;
        if (heading >= -45 && heading <= 45)
        {
            frontOB.transform.GetChild(0).gameObject.SetActive(false);
            rightSideOB.transform.GetChild(0).gameObject.SetActive(false);
            leftSideOB.transform.GetChild(0).gameObject.SetActive(false);
            backOB.transform.GetChild(0).gameObject.SetActive(true);

        }

        //if angle is between 46 and 315, right side
        if (heading >= 46 && heading <= 135)
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

        switch (BaseEnemy.enemyState)
        {
            case BaseEnemy.EnemyState.Patrolling:

                enemyStats.stats.speed = normalSpeed;

                agent.isStopped = true;

                gameObject.transform.localEulerAngles = new Vector3(0f, 90f, 0f);

                break;
            case BaseEnemy.EnemyState.Chase:

                if (Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange +2)
                {
                    print("Chase player");
                    if(!jumpingBack)
                    {
                        print("Player RUn part");
                        runningParticle.Play();
                        enemyStats.stats.speed = normalSpeed;


                        agent.isStopped = false;

                        agent.SetDestination(player.transform.position);
                    }



                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange &&
                    Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange + 3)
                {

                    if(!jumpingBack)
                    {

                        frontAnim.SetBool("Walking", false);
                        backAnim.SetBool("Walking", false);
                        leftSideAnim.SetBool("Walking", false);
                        rightSideAnim.SetBool("Walking", false);

                        enemyStats.stats.speed = jumpSpeed;


                        print("Stop to attack");

                        //agent.isStopped = false;

                        //Vector3 toPlayer = player.transform.position - transform.position;
                        //Vector3 targetPosition = toPlayer.normalized * -5;

                        //agent.SetDestination(targetPosition);

                        //targetPosition = toPlayer.normalized * 8;

                        //agent.SetDestination(targetPosition);

                        transform.LookAt(player.transform.position);
                        if (!animationPlayed)
                        {

                            agent.isStopped = true;
                            animationPlayed = true;
                            PlayAttackAnimation();

                            agent.isStopped = false;
                            agent.SetDestination(player.transform.position);

                            //PerformAttack(enemyStats.stats.fireRate);
                            ExecuteAfterSeconds(enemyStats.stats.fireRate, () => ResetAttackAnimation());
                        }
                        
                    }
                   

                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange)
                {
                    runningParticle.Stop();

                    PauseMotion(1);
                    ExecuteAfterSeconds(1, () => jumpingBack = true);
                    
                    if(jumpingBack)
                    {
                        enemyStats.stats.speed = normalSpeed;


                        print("should go back");
                        agent.isStopped = false;
                        Vector3 toPlayer = player.transform.position - transform.position;
                        Vector3 targetPosition = toPlayer.normalized * -0.1f;

                        agent.SetDestination(targetPosition);

                        ExecuteAfterSeconds(3, () => jumpingBack = false);
                    }

                }




                break;
            case BaseEnemy.EnemyState.Die:

                BaseEnemy.Die();


                break;

            case BaseEnemy.EnemyState.Charmed:
                //print("CHAMRED");
                //agent.isStopped = true;
                agent.SetDestination(_PIA.enemyLineStart.transform.position);

                break;
        }




    }

    void ResetAttackAnimation()
    {

        print("Reset aniamtions");
        animationPlayed = false;

    }

    void PlayAttackAnimation()
    {
        print("Attack anim");
        frontAnim.SetBool("Walking", false);
        backAnim.SetBool("Walking", false);
        leftSideAnim.SetBool("Walking", false);
        rightSideAnim.SetBool("Walking", false);

        if (frontOB.activeSelf == true) frontAnim.SetTrigger("Attack");
        if (backOB.activeSelf == true) backAnim.SetTrigger("Attack");
        if (rightSideOB.activeSelf == true) rightSideAnim.SetTrigger("Attack");
        if (leftSideOB.activeSelf == true) leftSideAnim.SetTrigger("Attack");


    }

    private Vector3 SearchWalkPoint()
    {

        return transform.position + Random.insideUnitSphere * walkPointRange;

    }

    void PauseMotion(float _seconds)
    {
        print("pause");
        agent.isStopped = true;
        ExecuteAfterSeconds(_seconds, () => agent.isStopped = false);

    }

    public void PerformAttack(float _firerate)
    {
        if (!attacking)
        {

            if (canAttack)
            {
                print("Attack");
                //attack shit

                _PC.Hit(enemyStats.stats.dmg);
                attacking = true;
                ExecuteAfterSeconds(_firerate, () => attacking = false);

            }


        }
    }
}
