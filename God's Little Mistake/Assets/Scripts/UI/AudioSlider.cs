using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : GameBehaviour
{
    public enum AudioType { Master, Game, BGM, Environment }

    [Header("Slider")]
    public Slider volumeSlider;

    [Header("Audio Mixers")]
    public AudioMixer backgroundMixer;
    public AudioMixer enemyMixer;
    public AudioMixer environmentMixer;
    public AudioMixer gameMixer;
    public AudioMixer playerMixer;

    [Header("Audio Mixers Group")]
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup ambianceMixer;

    [Header("Audio Types")]
    public AudioType audioType;

    private string volumeParameter;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        switch (audioType)
        {
            case AudioType.Master:
                volumeParameter = "MasterVolume";
                break;
            case AudioType.Game:
                volumeParameter = "GameVolume";
                break;
            case AudioType.BGM:
                volumeParameter = "BGMVolume";
                break;
            case AudioType.Environment:
                volumeParameter = "EnvironmentVolume";
                break;
        }

        volumeSlider.value = GetAudioVolume();
        ApplyVolume();
    }

    private float GetAudioVolume()
    {
        return PlayerPrefs.GetFloat(volumeParameter, 1f);
    }

    public void OnVolumeSliderChanged()
    {
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat(volumeParameter, volume);
        ApplyVolume();
        print("volume is " + volume);
    }

    private void ApplyVolume()
    {
        float volume = GetAudioVolume();

        switch (audioType)
        {
            case AudioType.Master:
                backgroundMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                enemyMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                environmentMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                gameMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                playerMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                break;
            case AudioType.Game:
                gameMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                enemyMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                playerMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                break;
            case AudioType.BGM:
                musicMixer.audioMixer.SetFloat("VolumeMusic", Mathf.Log10(volume) * 20);
                break;
            case AudioType.Environment:
                environmentMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                ambianceMixer.audioMixer.SetFloat("VolumeAmbiance", Mathf.Log10(volume) * 20);
                break;
        }
    }
}
