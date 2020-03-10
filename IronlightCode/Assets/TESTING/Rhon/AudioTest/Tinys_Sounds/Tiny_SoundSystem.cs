using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Viet.Components;


public class Tiny_SoundSystem : SFX_System
{
    public AudioSource MyAudioSource = null;
   // public AudioSource OverLapAudioSource = null;  // Use when using overlap sound.
    public override void PlaySoundById(int id)
    {
        foreach (var sfx in soundEffects)
        {
            if (id == sfx.id)
            {
                MyAudioSource.clip = sfx.Audio;
                MyAudioSource.Play();

               // OverLapAudioSource.clip = soundEffects[1].Audio;
               // OverLapAudioSource.Play();

                Debug.Log("Tiny_Orb");
            }
            else
            {
                Debug.Log("Clip with ID not found");
            }
        }
    }


}
