using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_TDashState : TestDanish_TraversalBaseState
{
    TestDanish_Controller_StateManager_v1 stateManager;

    public TestDanish_TDashState(TestDanish_Controller_StateManager_v1 _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Dash State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Dash State");
    }

    public override Type Tick()
    {
        Debug.Log("Dash State");

        if (!stateManager.isDashing)
        {
            if (stateManager.isMoving)
            {
                return typeof(TestDanish_TMoveState);
            }
            else if (!stateManager.isMoving)
            {
                return typeof(TestDanish_TIdleState);
            }
        }

        stateManager.dash.PerformDash(stateManager.dashVector);

        return null;
    }
}
