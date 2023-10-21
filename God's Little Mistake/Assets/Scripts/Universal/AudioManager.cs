using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    AudioSource audioManagerSourcePlayerHurt;
    [SerializeField]
    AudioSource audioManagerSourcePlayerDead;
    public AudioSource audioManagerSourceItem;
    public AudioSource playerAttackAudioSource;
    public AudioClip[] playerHurtSounds;
    public AudioClip[] playerDeathSounds;

    public AudioSource playerMovement;

    public AudioClip playerDeathExplosionSound;
    public AudioClip playerSquishyStep;
    public AudioClip playerHover;
    public AudioClip playerSlugLegs;
    public AudioClip playerPeaShoot;
    public AudioClip playerBlink;
    public AudioClip playerSparkle;
    public AudioClip playerBigEyesExplosion;
    public AudioClip playerSlugEyes;
    public AudioClip playerRamHit;
    public AudioSource playerThud;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ItemPickUp()
    {
        audioManagerSourceItem.Play();
    }

    public void PlayerHurt()
    {
        var sound = playerHurtSounds[Random.Range(0, playerHurtSounds.Length)];

        audioManagerSourcePlayerHurt.clip = sound;
        audioManagerSourcePlayerHurt.Play();
    }
    
    public void PlayerDeathScream()
    {
        var sound = playerDeathSounds[Random.Range(0, playerDeathSounds.Length)];

        audioManagerSourcePlayerHurt.clip = sound;
        audioManagerSourcePlayerHurt.Play();
    }

    public void PlayerDeathExplosion()
    {
        audioManagerSourcePlayerDead.clip = playerDeathExplosionSound;
        audioManagerSourcePlayerDead.Play();
    }

    public void RamHit()
    {
        playerAttackAudioSource.clip = playerRamHit;
        playerAttackAudioSource.Play();
    }

    public void VaryPitch(AudioSource _audioSource)
    {
        _audioSource.pitch = Random.Range(128 - 30, 128 + 30);
    }

    public void SingleStep(AudioSource _audioSource)
    {
        //VaryPitch(_audioSource);
        _audioSource.Play();
    }
}
