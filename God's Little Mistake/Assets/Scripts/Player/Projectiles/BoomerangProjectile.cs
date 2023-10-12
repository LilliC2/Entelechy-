using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : GameBehaviour
{
    float halfway;
    float totalDistance;

    [SerializeField]
    GameObject explosionAnimOB;
    [SerializeField]
    public GameObject image;
    [SerializeField]
    Animator explosionAnim;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        explosionAnim = explosionAnimOB.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = _PC.projectileRange;
        halfway = totalDistance / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_PC.transform.position, gameObject.transform.position) > halfway)
        {
            //turn around
            gameObject.transform.eulerAngles = Vector3.back;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //explosionAnimOB.SetActive(true);

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
