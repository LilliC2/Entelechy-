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

    bool turn = true;

    Vector3 playerPosWhenThrown;

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

        playerPosWhenThrown = _PC.transform.position;

        print("total distance: " + totalDistance);
        print("half distance: " + halfway);
    }

    // Update is called once per frame
    void Update()
    {
        if(turn && Vector3.Distance(playerPosWhenThrown, transform.position) > halfway)
        {
            print("we half");
            turn = false;
        }

        if (!turn)
        {
            //travel back

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_PC.transform.position.x, _PC.transform.position.y + 1, _PC.transform.position.z), Time.deltaTime * _PC.projectileSpeed/50);
        }
        if(!turn && Vector3.Distance(_PC.transform.position, transform.position) < 1.5f)
        {
            Destroy(this.gameObject);
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
