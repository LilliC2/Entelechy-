using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    private const string VolumePrefsKey = "VolumeLevel";

    private void Start()
    {
        // Load the volume level from player prefs if it exists
        if (PlayerPrefs.HasKey(VolumePrefsKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey);
            SetVolume(savedVolume);
        }
        else
        {
            // Set the default volume to the slider value
            SetVolume(volumeSlider.value);
        }

        // Add a listener to the slider's value change event
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volumeLevel)
    {
        // Set the volume using the slider value
        AudioListener.volume = volumeLevel;

        // Save the volume level to player prefs
        PlayerPrefs.SetFloat(VolumePrefsKey, volumeLevel);
        PlayerPrefs.Save();
    }
}