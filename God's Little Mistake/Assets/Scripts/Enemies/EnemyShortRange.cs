using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortRange : MonoBehaviour
{

    public float detectionRange = 10f; 
    public float movementSpeed = 3f; 
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 2f; 
    public Transform player;

    private bool isChasing = false; 
    private bool canAttack = true;

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            isChasing = true;

            if (Vector3.Distance(transform.position, player.position) <= attackRange && canAttack)
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
            transform.Translate(direction * movementSpeed * Time.deltaTime);
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

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
