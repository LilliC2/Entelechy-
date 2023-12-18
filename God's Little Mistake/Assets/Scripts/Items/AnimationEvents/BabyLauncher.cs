using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyLauncher : GameBehaviour
{
    [SerializeField]
    ParticleSystem explosionPS;
    public void Attack()
    {
        explosionPS.Play();
        _PAtk.BabyAttack();
    }
}
