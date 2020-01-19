using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// What it requires.
[RequireComponent(typeof(AudioSource))]

public class BG_Audio : MonoBehaviour
{
    // Access the Audio source.
    AudioSource MyAudioSource;

    // Background, Combat, Transition Music. 
    public AudioClip BGND;
    public AudioClip CMBT;
    public AudioClip TRNS;

    //bool checks for change in game state
    bool idleState;
    bool combatState;

    void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        MyAudioSource.clip = BGND;
        MyAudioSource.Play();

        if (combatState)
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(TRNS,1f);
            MyAudioSource.clip = CMBT;
            MyAudioSource.Play();
        }



    }
}
