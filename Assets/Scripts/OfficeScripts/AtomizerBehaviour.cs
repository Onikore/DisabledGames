using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomizerBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource AtomizerSound;
    public void OnButtonPress()
    {
        particleSystem.Play();
        SoundFXManager.Instance.PlaySoundFXClip(AtomizerSound, transform, 1);
    }
}
