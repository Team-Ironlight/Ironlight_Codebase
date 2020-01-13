using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_IdleState : PLY_BaseState
{

    PLY_StateManager stateManager;

    public PLY_IdleState(PLY_StateManager state) : base(state.gameObject)
    {
        stateManager = state;
    }

    public override void OnEnter()
    {
        Debug.Log("IdleState: OnEnter");
        // TODO Toggle Camera Idle Bool
    }

    public override void OnExit()
    {
        Debug.Log("IdleState: OnExit");
        // TODO Toggle Camera Idle Bool
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
