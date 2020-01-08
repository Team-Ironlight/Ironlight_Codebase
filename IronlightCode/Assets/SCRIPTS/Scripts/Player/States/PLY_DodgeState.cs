using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_DodgeState : PLY_BaseState
{
    PLY_PlayerStateManager stateManager;

    public PLY_DodgeState(PLY_PlayerStateManager state) : base(state.gameObject)
    {
        stateManager = state;
    }
    public override void OnEnter()
    {
        Debug.Log("Entering Dodge State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Dodge State");
    }

    public override Type Tick()
    {
        Debug.Log("Currently in Dodge State");


        return null;
    }
}
