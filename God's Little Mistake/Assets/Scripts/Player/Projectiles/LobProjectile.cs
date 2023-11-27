using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CurveProjectile : GameBehaviour
{
    public ParticleSystem impactPS;

    public ParticleSystem endOfRangePS;

    [SerializeField]
    GameObject explosionAnimOB;
    [SerializeField]
    public GameObject image;
    [SerializeField]
    Animator explosionAnim;
    [SerializeField]
    AudioSource explosionSound;

    bool playedPS;


    [SerializeField]
    LayerMask ground;
    [SerializeField]
    LayerMask enemy;




    void PlayAnimation()
    {
        playedPS = true;
        endOfRangePS.Play();

        image.SetActive(false);
        ExecuteAfterSeconds(endOfRangePS.main.duration, () => Destroy(gameObject));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            print("on ground");

            if (endOfRangePS != null)
            {

                if (!playedPS)
                {
                    ExecuteAfterSeconds(1, () => PlayAnimation());
                }

            }
            else
            {
                Destroy(gameObject);
            }


        }


        

        if (collision.collider.CompareTag("Enemy"))
        {
            explosionAnimOB.SetActive(true);

            image.SetActive(false);
            print("Destroy Projectile");
            //ooze animation
            //explosionAnim.SetTrigger("Death");

            

            if (impactPS != null)
            {
                if (!playedPS)
                {
                    var enemyCol = Physics.OverlapSphere(transform.position, 3, enemy);

                    foreach (var col in enemyCol)
                    {
                        col.gameObject.GetComponent<BaseEnemy>().Hit(_IM.itemDataBase[3].dmg);

                    }

                    playedPS = true;
                    impactPS.Play();

                    ExecuteAfterSeconds(impactPS.main.duration, () => Destroy(gameObject));
                }
            }
            else Destroy(gameObject);

            //get dmg from enemy
            //collision.gameObject.GetComponent<BaseEnemy>().Hit(collision.collider.gameObject.GetComponent<BaseEnemy>().stats.dmg);
        }
    }


}
