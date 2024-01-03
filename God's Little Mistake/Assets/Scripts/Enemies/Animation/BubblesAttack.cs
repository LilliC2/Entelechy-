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
        ELR.attackAudio.Play();

    }
    public void Hurt()
    {
        ELR.hurtAudio.Play();

    }

    public void Step()
    {
        ELR.stepAudio.Play();

    }

    public void Death()
    {
        ELR.deathAudio.Play();
    }
}
