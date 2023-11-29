using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : GameBehaviour
{
    public float dmg;
    bool hit = false;
    public GameObject image;

    [SerializeField]
    ParticleSystem impactPS;
    bool playedPS;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(!hit)
            {
                hit = true;
                print("player has been hit in collision");
                _PC.Hit(dmg);
                if (impactPS != null)
                {
                    if (!playedPS)
                    {
                        playedPS = true;
                        impactPS.Play();

                        ExecuteAfterSeconds(impactPS.main.duration, () => Destroy(gameObject));
                    }
                }
                else Destroy(this.gameObject);

            }

        }
    }
}
