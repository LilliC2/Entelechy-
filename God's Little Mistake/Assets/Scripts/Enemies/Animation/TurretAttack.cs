using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    EnemyPlant EP;

    private void Start()
    {
        EP = GetComponentInParent<EnemyPlant>();
    }

    public void Attack()
    {
        EP.attackAudio.Play();

    }
    public void Hurt()
    {
        EP.hurtAudio.Play();

    }

    public void Spawn()
    {
        EP.spawnAudio.Play();

    }

    public void Death()
    {
        EP.deathAudio.Play();
    }
}
