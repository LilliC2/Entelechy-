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
    public void BigEyesBlink()
    {
        _AM.playerAttackAudioSource.clip = _AM.playerBlink;
        _AM.playerAttackAudioSource.Play();
    } 

    public void BigEyesSparkle()
    {
        _AM.playerAttackAudioSource.clip = _AM.playerSparkle;
        _AM.playerAttackAudioSource.Play();
    }
    public void SlugEyes()
    {
        _AM.playerAttackAudioSource.clip = _AM.playerSlugEyes;
        _AM.playerAttackAudioSource.Play();
    }
    


    public void PeaShootSound()
    {

        _AM.playerAttackAudioSource.clip = _AM.playerPeaShoot;
        _AM.playerAttackAudioSource.Play();
    }
}
