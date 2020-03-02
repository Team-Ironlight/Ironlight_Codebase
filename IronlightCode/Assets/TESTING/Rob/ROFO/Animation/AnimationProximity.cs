using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//generic script to flip a bool based on trigger
//requires trigger
public class AnimationProximity : MonoBehaviour
{
    Animator anim;
    public bool isOn = false;
    public string proximityBool;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool(proximityBool, true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool(proximityBool, false);
    }

    public void Activate()
    {
        isOn = !isOn;
        anim.SetBool(proximityBool, isOn);
    }
}
