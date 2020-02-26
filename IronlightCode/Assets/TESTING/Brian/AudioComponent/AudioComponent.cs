using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    public AudioSource Player;
   
    public bool isPlaying;
    bool canPlay;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying&& canPlay)
        {
            playSound();
            canPlay = false;
        }
        if (!canPlay && !isPlaying)
        {
            stopSound();
            canPlay = true;
        }
    }

    void playSound()
    {
        Player.Play();
    }
    void stopSound()
    {
        Player.Stop();
    }
}
