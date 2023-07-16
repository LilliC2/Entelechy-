using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    private static float volumeLevel = 1.0f;

    private void Start()
    {
        if (volumeLevel != 0.0f)
        {
            SetVolume(volumeLevel);
        }
        else
        {
            SetVolume(volumeSlider.value);
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        volumeLevel = volume;
    }
}