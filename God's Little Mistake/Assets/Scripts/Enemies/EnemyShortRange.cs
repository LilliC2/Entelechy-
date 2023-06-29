using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortRange : GameBehaviour
{

    public float detectionRange = 10f; 
    public float movementSpeed = 3f; 
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 2f; 
    public Transform player;

    private bool isChasing = false; 
    private bool canAttack = true;

    BaseEnemy enemyStats;

    public enum EnemyState
    {
        Patrolling, Chase, Attacking, Die
    }

    public EnemyState enemyState;

    private void Start()
    {
        enemyStats = GetComponent<BaseEnemy>();
    }

    private void Update()
    {
        switch(enemyState)
        {
            case EnemyState.Patrolling:
                if (Vector3.Distance(transform.position, player.position) <= enemyStats.stats.range + 1)
                {
                    isChasing = true;

                    if (Vector3.Distance(transform.position, player.position) <= enemyStats.stats.range && canAttack)
                    {
                        Attack();
                    }
                }
                else
                {
                    isChasing = false;
                }

                if (isChasing)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.Translate(direction * enemyStats.stats.speed * Time.deltaTime);
                }
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attacking:
               
                break;
            case EnemyState.Die:
                //death animation etc
                print("Dead");
                Destroy(this.gameObject);
                break;
        }

        
    }

    void Hit()
    {
        if (enemyStats.stats.health != 0)
        {
            enemyStats.stats.health -= _PC.dmg;
            print(enemyStats.stats.health);
            StopAllCoroutines();

        }
        else
        {
            StopAllCoroutines();
            enemyState = EnemyState.Die;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            print("hit");
            //Add hit code here;
            Hit();

            //destroy bullet that hit it
            Destroy(collision.gameObject);
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy performs melee attack!");

        //player.GetComponent<Health>().TakeDamage(attackDamage);

        StartCoroutine(AttackCooldown());
    }

    private System.Collections.IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(enemyStats.stats.fireRate);

        canAttack = true;
    }
}
