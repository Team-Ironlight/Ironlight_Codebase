using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_AttackState : PLY_BaseState
{
    PLY_PlayerStateManager stateManager;

    public PLY_AttackState(PLY_PlayerStateManager state) : base(state.gameObject)
    {
        stateManager = state;
    }
    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
    }

    public override Type Tick()
    {
        Debug.Log("Currently in Attack State");


        return null;
    }
}
