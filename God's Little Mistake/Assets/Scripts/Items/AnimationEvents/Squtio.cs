using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squtio : GameBehaviour
{
    // Start is called before the first frame update
    public void Attack()
    {
        _AM.SquitoAttack.Play();
        _PAtk.SquitoAttack();
    }
}
