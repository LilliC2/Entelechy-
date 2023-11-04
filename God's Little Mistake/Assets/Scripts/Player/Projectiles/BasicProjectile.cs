using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : GameBehaviour
{
    [SerializeField]
    GameObject explosionAnimOB;
    [SerializeField]
    public GameObject image;
    [SerializeField]
    Animator explosionAnim;
    Rigidbody rb;
    [SerializeField]
    AudioSource explosionSound;

    public float initialDamage;
    public float maxDistance = 10.0f; 
    private Vector3 startPosition;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        explosionAnim = explosionAnimOB.GetComponent<Animator>();
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the distance traveled
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);

        // Calculate the damage based on distance
        float currentDamage = initialDamage * (1.0f - distanceTraveled / maxDistance);

        // Ensure damage doesn't go below 0
        currentDamage = Mathf.Max(currentDamage, 0.0f);

        // Debug.Log the damage for testing purposes
        Debug.Log("Damage dealt: " + currentDamage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {

            float distanceTraveled = Vector3.Distance(startPosition, transform.position);
            float currentDamage = initialDamage * (1.0f - distanceTraveled / maxDistance);
            currentDamage = Mathf.Max(currentDamage, 0.0f);

            Debug.Log("Damage dealt: " + currentDamage);

            //explosionAnimOB.SetActive(true);

            if (explosionSound != null) explosionSound.Play();

            image.SetActive(false);
            print("Destroy Projectile");
            //ooze animation
            explosionAnim.SetTrigger("Death");


            rb.velocity = Vector3.zero;
            Destroy(this.gameObject);

            //get dmg from enemy
            //Hit(collision.collider.gameObject.GetComponent<BaseEnemy>().stats.dmg);
        }
    }
}
