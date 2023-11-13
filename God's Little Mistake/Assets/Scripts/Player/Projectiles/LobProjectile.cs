using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveProjectile : GameBehaviour
{
    [SerializeField]
    GameObject explosionAnimOB;
    [SerializeField]
    public GameObject image;
    [SerializeField]
    Animator explosionAnim;
    [SerializeField]
    AudioSource explosionSound;

    [SerializeField]
    LayerMask ground;

    [SerializeField]
    GameObject antlerTrap;
    private void Start()
    {


    }

    private void Update()
    {
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            print("on ground");
            //spawn antler
            Instantiate(antlerTrap,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            explosionAnimOB.SetActive(true);

            image.SetActive(false);
            print("Destroy Projectile");
            //ooze animation
            //explosionAnim.SetTrigger("Death");


            Destroy(this.gameObject);

            collision.gameObject.GetComponent<BaseEnemy>().Hit(_IM.itemDataBase[3].dmg);

            //get dmg from enemy
            //collision.gameObject.GetComponent<BaseEnemy>().Hit(collision.collider.gameObject.GetComponent<BaseEnemy>().stats.dmg);
        }
    }


}
