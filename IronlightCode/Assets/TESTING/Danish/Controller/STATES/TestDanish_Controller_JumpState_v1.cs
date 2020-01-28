using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_Controller_JumpState_v1 : TestDanish_Controller_BaseState_v1
{
    TestDanish_Controller_StateManager_v1 stateManager;


    public TestDanish_Controller_JumpState_v1(TestDanish_Controller_StateManager_v1 state) : base(state.gameObject)
    {
        stateManager = state;
    }

    public override void OnEnter()
    {
        Debug.Log("JumpState: Enter");
    }

    public override void OnExit()
    {
        Debug.Log("JumpState: Exit");
    }

    public override void Tick()
    {
        Debug.Log("JumpState: Current");

    }

    public override Type CheckForNextState()
    {
        return GetType();
    }
}
