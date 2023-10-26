using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using DG.Tweening;


public class PlayerEffects : Singleton<PlayerEffects>
{


    [Header("Player Particle Systems")]
    public ParticleSystem deathExplosionPS;
    public GameObject missyDeathAnim;
    public ParticleSystem missyHitParticle;
    public ParticleSystem landingPS;

    [Header("Chromatic Abberation")]
    public Volume vignetteVC;
    public bool vigABbool = false;
    public float speedVig;
    bool resetSpeedVig = true;

    [Header("Pea Shooter Particle")]
    public ParticleSystem peaShooterPS;

    [Header("Big Eye Particle")]
    public GameObject bigEyePS;


    private void Start()
    {
        //make sure it starts at 0
        VolumeProfile profile = GetComponent<Volume>().sharedProfile;
        profile.TryGet<Vignette>(out var vig);
        if (vig != null)
        {
            vig.intensity.value = 0;

        }
    }

    private void Update()
    {

        
        if(vigABbool)
        {
            resetSpeedVig = true;

            VolumeProfile profile = GetComponent<Volume>().sharedProfile;

            //get volume profile reference
            profile.TryGet<Vignette>(out var vig);
            if(vig!= null)
            {
                print("vig");
                vig.intensity.value = speedVig;
                VigTweenSpeed(0, 0.5f);
                ExecuteAfterSeconds(0.5f, () => vigABbool = false);
            }
                
        }
    }


    public void VignetteFade()
    {
        vigABbool = true;
        if (resetSpeedVig)
        {
            resetSpeedVig = false;
            speedVig = 0.4f;
        }
    }


    
    private Tween VigTweenSpeed(float endValue, float time)
    {
        var speedTween = DOTween.To(() => speedVig, (x) => speedVig = x, endValue, time);
        return speedTween;
    }

}
