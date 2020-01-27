using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_Controller_DashState_v1 : TestDanish_Controller_BaseState_v1
{
    TestDanish_Controller_StateManager_v1 stateManager;

    Vector3 dashDirection = Vector3.zero;

    public TestDanish_Controller_DashState_v1(TestDanish_Controller_StateManager_v1 state) : base(state.gameObject)
    {
        stateManager = state;
    }

    public override void OnEnter()
    {
        Debug.Log("DashState: Enter");

        dashDirection.x = stateManager.dodgeVector.x;
        dashDirection.z = stateManager.dodgeVector.y;
    }

    public override void OnExit()
    {
        Debug.Log("DashState: Exit");
    }

    public override void Tick()
    {
        Debug.Log("DashState: Current");

        PerformDash();

        stateManager.currentState = this.GetType().ToString();

        stateManager.isDashing = false;
        CheckForNextState();

    }

    public override Type CheckForNextState()
    {
        if (stateManager.isDashing)
        {
            stateManager.machine.SwitchToNewState(this.GetType());
        }

        else if (stateManager.isMoving)
        {
            stateManager.machine.SwitchToNewState(typeof(TestDanish_Controller_MoveState_v1));
        }
        else if (!stateManager.isMoving)
        {
            stateManager.machine.SwitchToNewState(typeof(TestDanish_Controller_IdleState_v1));
        }

    }




    void PerformDash()
    {
        stateManager.playerObject.transform.position += (dashDirection * 5);
    }
}
