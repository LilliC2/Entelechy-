using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttack : MonoBehaviour
{
    EnemyShortRange ESR;
    [SerializeField]
    ParticleSystem leftParticle;
    [SerializeField]
    ParticleSystem rightParticle;

    private void Start()
    {
        ESR = GetComponentInParent<EnemyShortRange>();
    }

    public void Attack()
    {
        ESR.PerformAttack(ESR.enemyStats.stats.fireRate);
    }

    public void LeftParticle()
    {
        leftParticle.Play();
    }
    public void RightParticle()
    {
        rightParticle.Play();
    }
}
