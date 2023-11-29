using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChomper : GameBehaviour
{
    [Header("Enemy Navigation")]
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

    public Vector3 walkPoint;
    public float walkPointRange;

    [Header("Enemy Visuals")]

    [SerializeField]
    GameObject runningParticleGO;
    [SerializeField]
    ParticleSystem attackPS;

    [SerializeField]
    ParticleSystem runningParticle;

    public LayerMask whatIsGround, whatIsPlayer;




    public BaseEnemy enemyStats;
    BaseEnemy baseEnemy;

    void Start()
    {

        enemyStats = GetComponent<BaseEnemy>();
        baseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        //frontAnim = frontOB.GetComponentInChildren<Animator>();
        //backAnim = backOB.GetComponentInChildren<Animator>();
        //rightSideAnim = rightSideOB.GetComponentInChildren<Animator>();
        //leftSideAnim = leftSideOB.GetComponentInChildren<Animator>();

        attackRange = enemyStats.stats.range;

        normalSpeed = enemyStats.stats.speed;
        jumpSpeed = enemyStats.stats.speed*1.5f;

    }

    // Update is called once per frame
    void Update()
    {
        baseEnemy.FlipSprite(agent.destination);

        if (agent.velocity.magnitude > 0.5f) baseEnemy.walking.Play();

        agent.speed = enemyStats.stats.speed;

        //if player isnt dead
        if(_GM.gameState != GameManager.GameState.Dead)
        {
            ////check for the sight and attack range
            if (baseEnemy.enemyState != BaseEnemy.EnemyState.Charmed)
            {
                if (baseEnemy.enemyState != BaseEnemy.EnemyState.Die)
                {
                    canSee = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                    canAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                    //if cant see player, patrol
                    if (!canSee) baseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
                    else if (canSee) baseEnemy.enemyState = BaseEnemy.EnemyState.Chase;
                }
            }
        }
        //just patrol if player is dead
        else baseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;


       

        #region Animating Sprites

        ////check if walking
        //if (agent.velocity.magnitude > 0.1f)
        //{
        //    frontAnim.SetBool("Walking", true);
        //    backAnim.SetBool("Walking", true);
        //    leftSideAnim.SetBool("Walking", true);
        //    rightSideAnim.SetBool("Walking", true);
        //}
        //else
        //{
        //    frontAnim.SetBool("Walking", false);
        //    backAnim.SetBool("Walking", false);
        //    leftSideAnim.SetBool("Walking", false);
        //    rightSideAnim.SetBool("Walking", false);

        //}


        #endregion

        //Visual indicator for health
        //HealthVisualIndicator(enemyStats.stats.health, enemyStats.stats.maxHP);

        switch (baseEnemy.enemyState)
        {
            case BaseEnemy.EnemyState.Patrolling:
                runningParticle.Stop();

                enemyStats.stats.speed = normalSpeed;

                agent.isStopped = true;

                gameObject.transform.localEulerAngles = new Vector3(0f, 90f, 0f);

                break;
            case BaseEnemy.EnemyState.Chase:

                if (Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange)
                {
                    print("Chase player");
                    //print("Player RUn part");
                    runningParticle.Play();
                    enemyStats.stats.speed = jumpSpeed;


                    agent.isStopped = false;

                    agent.SetDestination(player.transform.position);



                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 3)
                {
                    runningParticle.Stop();
                    enemyStats.stats.speed = normalSpeed;

                    agent.isStopped = true;

                    PerformAttack(enemyStats.stats.fireRate);


                }




                break;
            case BaseEnemy.EnemyState.Die:
                runningParticle.Stop();
                baseEnemy.Die();


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

                attackPS.Play();

                baseEnemy.attack.Play();

                print("Attack");
                //attack shit

                _PC.Hit(enemyStats.stats.dmg);
                attacking = true;
                ExecuteAfterSeconds(_firerate, () => attacking = false);

            }


        }
    }
}
