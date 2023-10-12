using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSplat : GameBehaviour
{
    public void DeathSplatter()
    {
        _AM.PlayerDeathExplosion();
        _PC.GetComponent<BoxCollider>().enabled = false;
        _PC.deathExplosionPS.Play();
    }

}
