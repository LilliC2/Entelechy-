using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : GameBehaviour
{
    public ParticleSystem impactPS;

    [SerializeField]
    public GameObject image;

    bool playedPS;

    Rigidbody rb;
    [SerializeField]
    AudioSource explosionSound;

    public float initialDamage = 50f; 
    public float damageDecayRate = 0.5f;

    public float projectileDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            
            // how much damage falls off
            float currentDamage = initialDamage * (1.0f - damageDecayRate);

            // so it doesnt do 0 damage
            currentDamage = Mathf.Max(currentDamage, 0.0f);
            Debug.Log("Damage dealt: " + currentDamage);

            //explosionAnimOB.SetActive(true);

            if (explosionSound != null) explosionSound.Play();

            image.SetActive(false);
            print("Destroy Projectile");


            rb.velocity = Vector3.zero;

            //get dmg from enemy
            collision.collider.gameObject.GetComponent<BaseEnemy>().Hit(projectileDamage);

            if (impactPS != null)
            {
                if (!playedPS)
                {
                    playedPS = true;
                    impactPS.Play();

                    ExecuteAfterSeconds(impactPS.main.duration, () => Destroy(gameObject));
                }
            }
            else Destroy(gameObject);


        }
    }
}
