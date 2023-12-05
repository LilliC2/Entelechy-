using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : GameBehaviour
{
    [SerializeField]
    LayerMask enemy;

    private void OnCollisionEnter(Collision collision)
    {
        var enemyCol = Physics.OverlapSphere(transform.position, 3, enemy);
        print("trying to hit something");
        print(enemyCol.Length);
        foreach (var col in enemyCol)
        {
            print("hit in explosion");
            col.gameObject.GetComponent<BaseEnemy>().Hit(_IM.itemDataBase[7].dmg);

        }

    }

}
