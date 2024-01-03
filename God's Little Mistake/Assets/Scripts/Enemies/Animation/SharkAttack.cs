using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttack : MonoBehaviour
{
    EnemyPlant ESR;
    [SerializeField]
    ParticleSystem leftParticle;
    [SerializeField]
    ParticleSystem rightParticle;

    private void Start()
    {
        ESR = GetComponentInParent<EnemyPlant>();
    }

    public void Attack()
    {
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
