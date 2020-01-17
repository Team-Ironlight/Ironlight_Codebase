using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // What it requires.
[RequireComponent(typeof(AudioSource))]

public class Combat_Audio : MonoBehaviour
{
        // Access the Audio source.
    AudioSource MyAudioSource;


        // List of Audio clips used that use oneShot (Only play once)    
    public AudioClip S_Orb;
    public AudioClip S_Dash;
    public AudioClip S_Jump;

    public AudioClip S_PlayerDMGReceived;
    public AudioClip S_WaterImpact;
    public AudioClip S_LandingImpact;

        // List of Audio clips used that use 
    public AudioClip S_BlastCharge;
    public AudioClip S_BeamCharge;
    public AudioClip S_OnReleaseBlastCharge;
    public AudioClip S_OnReleaseBeamCharge;
     

    void Start()
    {      // Access audio source.
        MyAudioSource = GetComponent<AudioSource>();   
    }

    void Update()
    {
            // On key press play the Audio sound attach.
            // Use this for Hold attack.

            // Blast charge.
        if (Input.GetKeyDown(KeyCode.J))
        {
            MyAudioSource.Stop();
            MyAudioSource.clip = S_BlastCharge;
            MyAudioSource.Play();
        }
            // Blast Attack on release.
        if (Input.GetKeyUp(KeyCode.J))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_OnReleaseBlastCharge, 1f);
        }

            // Beam charge.
        if (Input.GetKeyDown(KeyCode.K))
        {
            MyAudioSource.Stop();
            MyAudioSource.clip = S_BeamCharge;
            MyAudioSource.Play();
        }
            // Beam attack on release.
        if (Input.GetKeyUp(KeyCode.K))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_OnReleaseBeamCharge, 1f);
        }

            // Orb Attack only play once. (OneShot) 
        if (Input.GetKeyDown(KeyCode.L))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_Orb, 1f);
        }

        // When player Dash. //TODO finish for all direction dash.
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_Dash, 1f);
        }

        // When player Jump.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_Jump, 1f);
        }
    }


         // Impact section
    private void OnTriggerEnter(Collider other)
    {
            // When player hits the water.
        if(other.gameObject.tag.Contains("Water"))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_WaterImpact, 1f);
        }

            // When player lands on ground.
        if (other.gameObject.tag.Contains("Land"))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_LandingImpact, 1f);
        }

            // When player gets damaged.
        if (other.gameObject.tag.Contains("DMGSource"))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_PlayerDMGReceived, 1f);
        }
    }



}
