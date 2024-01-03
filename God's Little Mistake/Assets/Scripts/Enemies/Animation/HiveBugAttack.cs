using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveBugAttack : MonoBehaviour
{
    EnemyHiveBug EHB;

    private void Start()
    {
        EHB = GetComponentInParent<EnemyHiveBug>();
    }

    public void Hurt()
    {
        EHB.hurtAudio.Play();

    }

    public void Walk()
    {
        EHB.walkAudio.Play();

    }

    public void Death()
    {
        EHB.deathAudio.Play();
    }
    
    public void Spawn()
    {
        EHB.spawnAudio.Play();
    }
}
