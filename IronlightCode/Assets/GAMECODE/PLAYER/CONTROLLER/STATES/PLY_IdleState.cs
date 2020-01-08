using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_IdleState : PLY_BaseState
{

    PLY_PlayerStateManager stateManager;

    public PLY_IdleState(PLY_PlayerStateManager state) : base(state.gameObject)
    {
        stateManager = state;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Idle State");
    }

    public override Type Tick()
    {
        //Debug.Log("Currently in Idle State");

        if (stateManager.vertical != 0 || stateManager.horizontal != 0)
        {
            return typeof(PLY_MoveState);
        }

        return null;
    }
}
