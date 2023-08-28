using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using DG.Tweening;


public class PlayerEffects : Singleton<PlayerEffects>
{
    public Volume chromaticAbVC;
    public float speed;
    Tween speedTween;
    public bool chromABbool = false;
    bool resetSpeed = true;

    private void Update()
    {
        if(chromABbool)
        {
            resetSpeed = true;

            VolumeProfile profile = GetComponent<Volume>().sharedProfile;

            //get volume profile reference
            profile.TryGet<ChromaticAberration>(out var chromab);
            
                print("CHRROM");
                chromab.intensity.value = speed;
            TweenSpeed(0, 0.5f);
            ExecuteAfterSeconds(0.5f, () => chromABbool = false);
        }
    }

    public void ChromaticABFade()
    {
        print("Add");

        chromABbool = true;
        if(resetSpeed)
        {
            resetSpeed = false;
            speed = 1;
        }
    }

    private Tween TweenSpeed(float endValue, float time)
    {
        speedTween = DOTween.To(() => speed, (x) => speed = x, endValue, time);
        return speedTween;
    }

}
