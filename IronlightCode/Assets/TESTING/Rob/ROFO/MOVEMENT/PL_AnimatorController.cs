using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ROFO
{ 
public class PL_AnimatorController : MonoBehaviour
{
    private Animator anim;
    private MoveControlY moveY;
    private MoveControlXZ moveXZ;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        foreach(Transform child in transform.parent)
        {
            if(child.name == "PL_Movement")
            {
                moveY = child.GetComponent<MoveControlY>();
                moveXZ = child.GetComponent<MoveControlXZ>();
                break;
            }
        }
    }

    MoveControlY.YState pState = MoveControlY.YState.None;
    MoveControlY.JumpPhase pJump = MoveControlY.JumpPhase.None;
    private void Update()
    {
        //update anim 
        if(pState != moveY.currentYstate)
        {
            pState = moveY.currentYstate;
            AnimSetBool(pState.ToString(), true);
        }

        //check jump as 2 phases
        if(pState != MoveControlY.YState.Grounded)
        {
            if(moveY.currentJumpPhase == MoveControlY.JumpPhase.Ascending)
            {
                AnimSetBool("Jumping", true);
            }
            else if(moveY.currentJumpPhase == MoveControlY.JumpPhase.Descending)
            {
                AnimSetBool("Falling", true);
            }
        }

        Vector2 input = moveXZ.input;
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.y);
    }


        //cycle through parameters (bools for now) and set param true, others false
        public void AnimSetBool(string paramName, bool b)
        {
            //Debug.Log("Param count: " + anim.parameterCount);
            for (int i = 0; i < anim.parameterCount; i++)
            {
                //check if param is bool            
                if (anim.parameters[i].type.ToString() == "Bool")
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
}
