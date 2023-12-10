    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyShortRange : GameBehaviour
{

    [Header("Enemy Navigation")]
    bool projectileShot;
    GameObject firingPoint;
    public GameObject player;
    public NavMeshAgent agent;

    bool animationPlayed;

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

    [Header("Audio")]
    public AudioSource attackAudio;
    public AudioSource hurtAudio;
    public AudioSource deathAudio;

    [Header("Animation")]
    [SerializeField]
    Animator anim;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Enemy Sprites")]

    [SerializeField]
    float scaleIncrease = 0.1f;
    float currentScale = 1;

    public BaseEnemy enemyStats;
    BaseEnemy baseEnemy;

    Vector3 target;

    void Start()
    {

        enemyStats = GetComponent<BaseEnemy>();
        baseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");


        attackRange = enemyStats.stats.range;
        target = SearchWalkPoint();
        baseEnemy.childAnim = anim;

    }

    // Update is called once per frame
    void Update()
    {
        baseEnemy.FlipSprite(agent.destination);

        if (agent.velocity.magnitude > 0.5f)
        {
            anim.SetBool("Walking", true);
            //baseEnemy.walking.Play();
        }
        else anim.SetBool("Walking", false);


        agent.speed = enemyStats.stats.speed;

        if (_GM.gameState != GameManager.GameState.Dead)
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



        //#endregion


        switch (baseEnemy.enemyState)
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
                    print("Chase player");

                    agent.isStopped = false;

                    agent.SetDestination(player.transform.position);

                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange)
                {


                    print("Stop to attack");
                    agent.isStopped = true;

                    transform.LookAt(player.transform.position);
                    PerformAttack(enemyStats.stats.fireRate);




                }




                break;
            case BaseEnemy.EnemyState.Die:

                baseEnemy.Die();


                break;

            case BaseEnemy.EnemyState.Charmed:
                //print("CHAMRED");
                //agent.isStopped = true;
                agent.SetDestination(_PIA.enemyLineStart.transform.position);

                break;
        }




    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            print("Size increase");
            currentScale = Mathf.Clamp(currentScale +=scaleIncrease,1,2.5f);
            gameObject.transform.DOScale(new Vector3(currentScale,currentScale,currentScale), 1);
        }
    }

    private Vector3 SearchWalkPoint()
    {

        return transform.position + Random.insideUnitSphere * walkPointRange;

    }

    public void PerformAttack(float _firerate)
    {
        if (!attacking)
        {

            if (canAttack)
            {
                //baseEnemy.attack.Play();
                anim.SetTrigger("Attack");

                print("Attack");
                //attack shit

                _PC.Hit(enemyStats.stats.dmg);
                attacking = true;
                ExecuteAfterSeconds(_firerate, () => attacking = false);

            }


        }
    }


}