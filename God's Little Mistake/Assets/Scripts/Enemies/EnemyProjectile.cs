using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : GameBehaviour
{
    public float dmg;
    bool hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(!hit)
            {
                hit = true;
                print("player has been hit in collision");
                _PC.Hit(dmg);


                Destroy(this.gameObject);
            }

        }
    }
}
