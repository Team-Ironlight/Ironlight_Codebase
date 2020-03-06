﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggers : MonoBehaviour
{
    AudioSource sound;
    public AudioClip soundToPlay;
    bool clipPlayed;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        clipPlayed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!clipPlayed)
            {
                sound.PlayOneShot(soundToPlay, volume);
                clipPlayed = true;
            }
        }
    }
}
