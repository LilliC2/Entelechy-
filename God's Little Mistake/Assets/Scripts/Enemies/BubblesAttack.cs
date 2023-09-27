using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesAttack : GameBehaviour
{
    EnemyLongRange ELR;

    private void Start()
    {
        ELR = GetComponentInParent<EnemyLongRange>();
    }

    public void Attack()
    {


        print("Animation played projectile ");
        ELR.FireProjectile(ELR.enemyStats.stats.projectilePF, ELR.enemyStats.stats.projectileSpeed, ELR.enemyStats.stats.fireRate, ELR.enemyStats.stats.range);

    }
}
