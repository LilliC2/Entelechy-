using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : GameBehaviour
{
    public AudioMixer audioMixer;
    public AudioType audioType;

    [Header("Slider")]
    public Slider volumeSlider;
    
    [Header("Audio Source")]
    public AudioSource audioSource;
    public AudioSource gameSource;
    public AudioSource bgmSource;
    public AudioSource squishySource;


    public enum AudioType { Master, Game, BGM, Squishy }

    void Start()
    {
        volumeSlider.value = GetAudioVolume();

        audioMixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
    }

    // Update is called once per frame
    float GetAudioVolume()
    {
        float volume = 1f;

        switch(audioType)
        {
            case AudioType.Master:
                volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
                break;
            case AudioType.Game:
                volume = PlayerPrefs.GetFloat("GameVolume", 1f);
                break;
            case AudioType.BGM:
                volume = PlayerPrefs.GetFloat("BGMVolume", 1f);
                break;
            case AudioType.Squishy:
                volume = PlayerPrefs.GetFloat("SquishyVolume", 1f);
                break;
        }

        return volume;
    }

    public void OnChangeSlider(float value)
    {
        value = Mathf.Clamp01(value);
        float audioVolume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("Volume", audioVolume);
        
        switch (audioType)
        {
            case AudioType.Master:
                PlayerPrefs.SetFloat("MasterVolume", value);
                break;

            case AudioType.Game:
                gameSource.volume = value;
                PlayerPrefs.SetFloat("GameVolume", value);
                break;

            case AudioType.BGM:
                bgmSource.volume = value;
                PlayerPrefs.SetFloat("BGMVolume", value);
                break;

            case AudioType.Squishy:
                squishySource.volume = value;
                PlayerPrefs.SetFloat("BGMVolume", value);
                break;
        }

        PlayerPrefs.Save();
    }
}
