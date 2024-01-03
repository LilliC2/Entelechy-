using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : GameBehaviour
{

    [SerializeField]
    ParticleSystem spitPS;

    public void Attack()
    {
        _AM.peaShooterAttack.Play();
        _PAtk.PeaShooterAttack();
    }
}
