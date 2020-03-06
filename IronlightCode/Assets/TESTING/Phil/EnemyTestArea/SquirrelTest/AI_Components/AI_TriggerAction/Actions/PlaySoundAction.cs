// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class PlaySoundAction : Phil_ActionBase
{
    public AudioSource AudioSource;

    public override void Act() {
        AudioSource.Play();
    }
}
