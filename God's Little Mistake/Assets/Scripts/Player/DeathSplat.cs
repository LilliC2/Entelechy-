using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSplat : GameBehaviour
{
    public void DeathSplatter()
    {
        _PC.enableMovement = false;
        _AM.PlayerDeathExplosion();
        _PC.GetComponent<BoxCollider>().enabled = false;
        _PC.deathExplosionPS.Play();
        _PC.missyDeathAnim.SetActive(true);
        
    }

}
