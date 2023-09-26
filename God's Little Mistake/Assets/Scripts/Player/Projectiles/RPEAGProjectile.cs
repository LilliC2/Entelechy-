using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPEAGProjectile : GameBehaviour
{
    [SerializeField]
    GameObject explosionAnimOB;
    [SerializeField]
    GameObject image;
    [SerializeField]
    Animator explosionAnim;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        explosionAnim = explosionAnimOB.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {

            //get enemies in radius
            var enemyCol = Physics.OverlapSphere(transform.position, _PIA.RPEAGexplosionRadius, _PIA.enemyMask);

            foreach (var col in enemyCol)
            {
                col.gameObject.GetComponent<BaseEnemy>().Hit(_PIA.RPEAGexplosionDmg);
            }

            //explosionAnimOB.SetActive(true);

            image.SetActive(false);
            print("play anim");
            //ooze animation
            explosionAnim.SetTrigger("Death");


            rb.velocity = Vector3.zero;
            ExecuteAfterSeconds(0.1f, () => Destroy(this.gameObject));

            //get dmg from enemy
            //Hit(collision.collider.gameObject.GetComponent<BaseEnemy>().stats.dmg);
        }
    }
}
