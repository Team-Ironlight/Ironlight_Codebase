using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    PlayerStateManager stateManager;

    public MoveState(PlayerStateManager state) : base(state.gameObject)
    {
        stateManager = state;
    }
    public override void OnEnter()
    {
        Debug.Log("Entering Move State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Move State");
    }

    public override Type Tick()
    {
        Debug.Log("Currently in Move State");

        if(stateManager.vertical == 0 && stateManager.horizontal == 0)
        {
            return typeof(IdleState);
        }


        return null;
    }
}
