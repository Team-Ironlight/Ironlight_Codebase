using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this will take in changes to the animator and 
//decide what happens
public class AnimatorFacade : MonoBehaviour
{
    public static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            Debug.Log("Animator empty");
        }
        else
        {
            Debug.Log("Animotor Set");
            Debug.Log("Parameter count: " + anim.parameterCount);
            for (int i = 0; i < anim.parameterCount; i++)
            {
                Debug.Log("Parameter: " + anim.parameters[i].name);
            }
        }
    }

    //take in the parameter and the value
    //iterate to see if that parameter exists
    //check if value matches 
    //run accordingly
    public static void SetParameter(bool b, string parameterName)
    {
        bool check = false;
        for (int i = 0; i < anim.parameterCount; i++)
        {
            if(anim.parameters[i].name == parameterName)
            {
                check = true;
                anim.SetBool(parameterName, b);
                break;
            }
        }

        if(check == false)
        {
            Debug.Log("<color=red>ERROR: Animator doesn't have this parameter!!</color>: " + parameterName);
        }
    }

    public static void SetAnimation(string name)
    {
        anim.Play(name);
    }

    public static void SetAnimation(string name, int layer, float frame)
    {
        anim.Play(name, layer, frame);
    }
}
