using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    public float detectionRange = 10f;
    public float shootInterval = 2f;
    public float projectileSpeed = 10f;
    public float undergroundY = -1f;
    public GameObject projectilePrefab;

    private Transform turret;
    private Transform underground;
    private bool playerInRange = false;
    private float nextShootTime;
    private Vector3 initialPosition; 

    private void Start()
    {
        turret = transform.Find("Turret"); 
        underground = transform.Find("Underground");
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Time.time > nextShootTime)
            {
                ShootMultipleProjectiles(8); 
                nextShootTime = Time.time + shootInterval;
            }
        }
        else
        {
            GoUnderground();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            GoAboveGround();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
     
    private void ShootMultipleProjectiles(int numProjectiles)
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = i * 360f / numProjectiles;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * Vector3.forward;

            // instantiate the projectile on the turret gameobject
            GameObject newProjectile = Instantiate(projectilePrefab, turret.position, Quaternion.identity);

            // applying force to the projectiles to make move in the right directions
            Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = direction * projectileSpeed;

            Debug.Log("Plant has shot!");
        }
    }



    private void GoUnderground()
    {
        Vector3 newPosition = new Vector3(transform.position.x, undergroundY, transform.position.z);
        transform.position = newPosition;
    }

    private void GoAboveGround()
    {
        transform.position = initialPosition;
    }
}