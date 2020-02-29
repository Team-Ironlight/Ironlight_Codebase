using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource myButton;
    public AudioClip hoverButton;
    public AudioClip clickButton;

    public void HoverSound()
    {
        myButton.PlayOneShot(hoverButton);
    }

    public void ClickSound()
    {
        myButton.PlayOneShot(clickButton);
                   
    }
}
