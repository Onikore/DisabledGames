using System;
using UnityEngine;

public class ScissorsBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource ScissorsSound;
    [SerializeField] public LeafBehaviour LeafInContact;
    public void OnButtonPress()
    {
        SoundFXManager.Instance.PlaySoundFXClip(ScissorsSound, transform, 1);
        if (LeafInContact != null)
            LeafInContact.FallFromStem();
    }
}
