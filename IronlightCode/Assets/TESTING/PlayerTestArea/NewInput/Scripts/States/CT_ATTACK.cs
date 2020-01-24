using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_ATTACK : CT_BaseState
{
    CT_StateManager manager;

    public CT_ATTACK(CT_StateManager _state) : base(_state.gameObject)
    {
        manager = _state;
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

        return typeof(CT_ATTACK);
    }
}
