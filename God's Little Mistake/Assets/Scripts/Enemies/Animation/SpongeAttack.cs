using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAttack : GameBehaviour
{
    EnemyPlant ESR;

    private void Start()
    {
        ESR = GetComponentInParent<EnemyPlant>();
    }

    public void Attack()
    {
        ESR.attackAudio.Play();

    }
    public void Hurt()
    {
        ESR.hurtAudio.Play();

    }

    public void Walk()
    {
        ESR.spawnAudio.Play();

    }

    public void Death()
    {
        ESR.deathAudio.Play();
    }
}
