using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlaySoundFXClip(AudioSource soundFX, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFX, spawnTransform.position, Quaternion.identity);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    
    public Action PlaySoundFXClipUntilEventInvoke(AudioSource soundFX, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFX, spawnTransform);
        audioSource.loop = true;
        audioSource.Play();
        
        return () => Destroy(audioSource.gameObject);
    }
}
