// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Audio : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip scream_Clip, die_Clip, dash_Clip, jump_Clip, landing_Clip, roaming_Clip;

    [SerializeField]
    private AudioClip[] attack_Clips;

    // Use this for initialization
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Play_JumpSound()
    {
        audioSource.clip = jump_Clip;
        audioSource.Play();
    }
    public void Play_LandSound()
    {
        audioSource.clip = landing_Clip;
        audioSource.Play();
    }
    public void Play_DashSound()
    {
        audioSource.clip = dash_Clip;
        audioSource.Play();
    }
    public void Play_roamSound()
    {
        audioSource.clip = roaming_Clip;
        audioSource.Play();
    }
    public void Play_ScreamSound()
    {
        audioSource.clip = scream_Clip;
        audioSource.Play();
    }

    public void Play_AttackSound()
    {
        audioSource.clip = attack_Clips[Random.Range(0, attack_Clips.Length)];
        audioSource.Play();
    }

    public void Play_DeadSound()
    {
        audioSource.clip = die_Clip;
        audioSource.Play();
    }

} // class












