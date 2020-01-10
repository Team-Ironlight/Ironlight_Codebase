using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_DashState : PLY_BaseState
{
    PLY_StateManager stateManager;

    public PLY_DashState(PLY_StateManager state) : base(state.gameObject)
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
