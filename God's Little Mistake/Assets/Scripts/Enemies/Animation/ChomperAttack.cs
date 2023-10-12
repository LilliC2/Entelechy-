using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperAttack : MonoBehaviour
{
    EnemyChomper EC;

    // Start is called before the first frame update
    void Start()
    {
        EC = GetComponentInParent<EnemyChomper>();
    }

    public void Attack()
    {
        EC.PerformAttack(EC.enemyStats.stats.fireRate);

    }
}
