using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squtio : GameBehaviour
{
    [SerializeField]
    GameObject redDot;
    [SerializeField]
    ParticleSystem explosionPS;

    private void Start()
    {
        if (_PE.redDotGO == null) _PE.redDotGO = redDot;

    }

    // Start is called before the first frame update
    public void Attack()
    {
        _AM.SquitoAttack.Play();
        _PAtk.SquitoAttack();
    }

    public void Explosion()
    {
        explosionPS.Play();
    }

    public void RedDot()
    {

        _PE.SquitoRedDot();
    }
}
