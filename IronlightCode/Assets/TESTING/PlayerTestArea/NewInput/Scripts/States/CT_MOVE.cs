using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_MOVE : CT_BaseState
{
    CT_StateManager manager;

    public CT_MOVE(CT_StateManager _state) : base(_state.gameObject)
    {
        manager = _state;
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

        return typeof(CT_MOVE);
    }
}
