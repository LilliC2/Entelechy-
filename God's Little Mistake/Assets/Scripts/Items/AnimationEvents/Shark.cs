using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : GameBehaviour
{
    [SerializeField]
    ParticleSystem explosionPS;
    public void Attack()
    {
        print("anim attack");
        _PAtk.SharkAttack();
    }

    public void Explosion()
    {
        explosionPS.Play();
    }

}
