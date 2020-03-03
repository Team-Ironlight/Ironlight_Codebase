using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCollision : MonoBehaviour
{
    Animator anim;
    public bool isOn = false;
    public string collisionTag;
    public string collisionBool;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == collisionTag)
        {
            anim.SetBool(collisionBool, true);
        }        
    }
}
