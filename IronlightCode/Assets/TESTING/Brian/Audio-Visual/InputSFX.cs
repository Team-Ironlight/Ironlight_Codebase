using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip Dash;
    public AudioClip Jump1;
    public AudioClip jump2;
    public AudioClip jump3;
    public float volume;
    int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playJump();
            //counter++;
            //if (counter > 2)
            //{
            //    counter = 0;
            //}
            print("counter is " + counter);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayDash();
        }
    }
    void PlayDash()
    {
        source.PlayOneShot(Dash, volume);
    }

    void playJump()
    {
        if (counter == 0)
        {
            counter++;
            source.PlayOneShot(Jump1, volume);

        }
        if (counter ==1)
        {
            counter++;
            source.PlayOneShot(jump2, volume);

        }
        if (counter==2)
        {
            counter = 0;
            source.PlayOneShot(jump3, volume);

        }
    }
}
