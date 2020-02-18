using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        Setup();
    }
    
    //gets animator from obj, checks shit
    private void Setup()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Animator empty");
        }
        else
        {
            //Debug.Log("Animotor Set");
            //Debug.Log("Parameter count: " + anim.parameterCount);
            for (int i = 0; i < anim.parameterCount; i++)
            {
                //Debug.Log("Parameter: " + anim.parameters[i].name);
            }
        }
    }

    //cycle through parameters (bools for now) and set param true, others false
    public void AnimSetBool(string paramName, bool b)
    {
        //Debug.Log("Param count: " + anim.parameterCount);
        for (int i = 0; i < anim.parameterCount; i++)
        {
            //check if param is bool            
            if(anim.parameters[i].type.ToString() == "Bool")
            {
                if (anim.parameters[i].name == paramName)
                {
                    //Debug.Log("Param name: " + anim.parameters[i].name + " " + true);
                    anim.SetBool(anim.parameters[i].name, true);
                }
                else
                {
                    //Debug.Log("Param name: " + anim.parameters[i].name + " " + false);
                    anim.SetBool(anim.parameters[i].name, false);
                }
            }            
        }
    }
}
