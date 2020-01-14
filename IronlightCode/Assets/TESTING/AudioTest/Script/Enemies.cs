using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Enemies : MonoBehaviour
{

         // Access the Audio source.
    AudioSource MyAudioSource;

        // bool checks if enemies are attacking.
    public bool Squirrel_IsAttacking;
    public bool Squirrel_IsChasing;

        // Squirrel fleeing
    public bool Squirrel_IsFleeing;
    public bool Squirrel_IsWondering;

        // Snake Attack
    public bool Snake_IsAttacking;
    public bool Snake_IsIdling;
    

     // Squirrel Section
         // List of Audio clips  
    public AudioClip S_SquirrelDash;
    public AudioClip S_SquirrelPounce;

        // clips
    public AudioClip S_SquirrelChase;
    public AudioClip S_SquirrelFleeing;
    public AudioClip S_SquirrelWondering;    

      // Snake Section
        // Snake Attack
    public AudioClip S_SnakeSpit;

        // clips (Ambient when player gets close)
    public AudioClip S_SnakeRattle;
    public AudioClip S_SnakeIdle;

    void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
    }

 
    void Update()
    {
          // Squirrel Section
            // Attacking
        if (Squirrel_IsAttacking)
        {
            MyAudioSource.Stop();
            // Squirrel in cave (Dashes)
            MyAudioSource.PlayOneShot(S_SquirrelDash, 1f);
            // Squirrel Pounce attack (Play once)
            MyAudioSource.PlayOneShot(S_SquirrelPounce, 1f);
        }    

            // When Squirrel wondering
        if(Squirrel_IsWondering)
        {
            MyAudioSource.clip = S_SquirrelWondering;
        }
        else

            // When Squirrel Chase
        if (Squirrel_IsChasing)
        {
            MyAudioSource.Stop();
            MyAudioSource.clip = S_SquirrelChase;
        }
        else

            // When Squirrel is fleeing
        if (Squirrel_IsFleeing)
        {
            MyAudioSource.Stop();
            MyAudioSource.clip = S_SquirrelFleeing;

        }

          // Snake Section
            // When snake idle hissing sound on and off.
        if (Snake_IsIdling)
        {
            MyAudioSource.Stop();
            MyAudioSource.clip = S_SnakeIdle;
        }

            // Snake Attack
        if (Snake_IsAttacking)
        {
            MyAudioSource.Stop();
                
                // Play once.
            MyAudioSource.PlayOneShot(S_SnakeSpit, 1f);
                // Play clips
            MyAudioSource.clip = S_SnakeRattle;
        }
    }

}
