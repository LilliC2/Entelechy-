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
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        explosionAnim = explosionAnimOB.GetComponent<Animator>();
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //explosionAnimOB.SetActive(true);

            if(explosionSound != null) explosionSound.Play();

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
