using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioManagerSource;
    public AudioClip[] playerHurtSounds;

    // Start is called before the first frame update
    void Start()
    {
        audioManagerSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHurt()
    {
        var sound = playerHurtSounds[Random.Range(0, playerHurtSounds.Length)];

        audioManagerSource.clip = sound;
        audioManagerSource.Play();
    }
}
