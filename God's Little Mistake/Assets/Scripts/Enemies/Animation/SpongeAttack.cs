using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAttack : GameBehaviour
{
    EnemySponge ESR;

    private void Start()
    {
        ESR = GetComponentInParent<EnemySponge>();
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
        ESR.walkAudio.Play();

    }

    public void Death()
    {
        ESR.deathAudio.Play();
    }
}
