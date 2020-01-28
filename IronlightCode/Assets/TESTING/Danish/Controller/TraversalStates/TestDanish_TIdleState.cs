using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_TIdleState : TestDanish_TraversalBaseState
{
    TestDanish_Controller_StateManager_v1 stateManager;

    public TestDanish_TIdleState(TestDanish_Controller_StateManager_v1 _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
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
        Debug.Log("Idle State");

        if (stateManager.isDashing)
        {
            return typeof(TestDanish_TDashState);
        }

        if (stateManager.isMoving)
        {
            return typeof(TestDanish_TMoveState);
        }

        return null;
    }
}
