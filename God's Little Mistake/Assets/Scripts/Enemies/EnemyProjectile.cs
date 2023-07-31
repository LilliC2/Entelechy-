using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : GameBehaviour
{
    public float dmg;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            print("player has been hit in collision");
            _PC.Hit(dmg);

            Destroy(this.gameObject);
            //get dmg from enemy
            //Hit(collision.collider.gameObject.GetComponent<BaseEnemy>().stats.dmg);
        }
    }
}
