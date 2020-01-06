using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateManager stateManager;

    public AttackState(PlayerStateManager state) : base(state.gameObject)
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
