using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_Controller_IdleState_v1 : TestDanish_Controller_BaseState_v1
{
    TestDanish_Controller_StateManager_v1 stateManager;


    public TestDanish_Controller_IdleState_v1(TestDanish_Controller_StateManager_v1 state) : base(state.gameObject)
    {
        stateManager = state;
    }


    public override void OnEnter()
    {
        Debug.Log("IdleState: Enter");
    }

    public override void OnExit()
    {
        Debug.Log("IdleState: Exit");
    }

    public override void Tick()
    {
        Debug.Log("IdleState: Current");

        stateManager.currentState = this.GetType().ToString();

        CheckForNextState();

    }
    public override Type CheckForNextState()
    {
        if (stateManager.isDashing)
        {
            stateManager.machine.SwitchToNewState(typeof(TestDanish_Controller_DashState_v1));
        }
        else if (stateManager.isMoving)
        {
            stateManager.machine.SwitchToNewState(typeof(TestDanish_Controller_MoveState_v1));
        }

        return GetType();
    }
}
