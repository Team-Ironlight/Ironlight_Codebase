using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    public AudioSource SoundonClick;
    public AudioSource SoundOnRelease;


    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            SoundonClick.Play();
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            SoundonClick.Stop();
            SoundOnRelease.Play();
        }

    }
}
