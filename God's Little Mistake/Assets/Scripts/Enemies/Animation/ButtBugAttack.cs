using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtBugAttack : GameBehaviour
{
    EnemyChomper EC;

    private void Start()
    {
        EC = GetComponentInParent<EnemyChomper>();
    }

    public void Attack()
    {
        EC.attackAudio.Play();

    }
    public void Hurt()
    {
        EC.hurtAudio.Play();

    }

    public void Walk()
    {
        EC.walkAudio.Play();

    }

    public void Death()
    {
        EC.deathAudio.Play();
    }
}
