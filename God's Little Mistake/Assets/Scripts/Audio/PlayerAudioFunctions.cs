using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioFunctions : GameBehaviour
{
    public void SingleStep()
    {
        _AM.playerMovement.clip = _AM.playerSquishyStep;

        print("Step");
        _AM.SingleStep(_AM.playerMovement);
    }
    
    public void Hover()
    {
        _AM.playerMovement.clip = _AM.playerHover;
        _AM.playerMovement.Play();
    }
    
    public void SlugLegs()
    {
        _AM.playerMovement.clip = _AM.playerSlugLegs;
        _AM.playerMovement.Play();
    }
    
    public void PeaShootSound()
    {

        _AM.playerProjectileAudioSource.clip = _AM.playerPeaShoot;
        _AM.playerProjectileAudioSource.Play();
    }
}
