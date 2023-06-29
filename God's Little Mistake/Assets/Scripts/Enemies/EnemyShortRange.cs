using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortRange : MonoBehaviour
{
    public float detectionRange = 10f;
    public float chaseSpeed = 3f;
    public float roamSpeed = 1.5f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 2f;
    public int maxHealth = 100;
    public float roamingRadius = 5f;
    public float rotationSpeed = 5f;

    private int currentHealth;
    private bool isChasing = false; 
    private bool canAttack = true;
    private Transform player;
    private Vector3 roamingPosition;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateRoamingPosition();
    }

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

            Roam();
        }

        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy performs melee attack!");
        //player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        // Start the attack cooldown
        StartCoroutine(AttackCooldown());
    }

    private System.Collections.IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy defeated!");

        Destroy(gameObject);
    }

    private void Roam()
    {
        if (Vector3.Distance(transform.position, roamingPosition) <= 0.1f)
        {
            GenerateRoamingPosition();
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(roamingPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 direction = (roamingPosition - transform.position).normalized;
            transform.Translate(direction * roamSpeed * Time.deltaTime);
        }
    }

    private void GenerateRoamingPosition()
    {
        roamingPosition = transform.position + Random.insideUnitSphere * roamingRadius;
        roamingPosition.y = transform.position.y;
    }
}