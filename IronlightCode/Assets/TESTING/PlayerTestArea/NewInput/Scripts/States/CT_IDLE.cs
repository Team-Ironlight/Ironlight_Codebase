using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CT_IDLE : CT_BaseState
{
    CT_StateManager manager;
    InputAction.CallbackContext ctx;

    public CT_IDLE(CT_StateManager _state) : base(_state.gameObject)
    {
        manager = _state;
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
        Debug.Log("Currently in Idle State");

        return typeof(CT_IDLE);
    }
}
