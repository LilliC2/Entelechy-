using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEyes : GameBehaviour
{

    GameObject bigEyesPS;

    private void Start()
    {
        bigEyesPS = GameObject.Find("BigEyes_Explosion");
    }
    public void BigEyePS()
    {
        _AM.playerAttackAudioSource.clip = _AM.playerBigEyesExplosion;
        _AM.playerAttackAudioSource.Play();
        bigEyesPS.GetComponent<ParticleSystem>().Play();
    }
}
