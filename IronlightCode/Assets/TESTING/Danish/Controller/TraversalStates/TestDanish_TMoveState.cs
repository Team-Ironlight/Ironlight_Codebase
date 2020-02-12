using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_TMoveState : TestDanish_TraversalBaseState
{
    TestDanish_Controller_StateManager_v1 stateManager;

    public TestDanish_TMoveState(TestDanish_Controller_StateManager_v1 _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
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
        Debug.Log("Move State");

        if (stateManager.isDashing)
        {
            return typeof(TestDanish_TDashState);
        }

        if (!stateManager.isMoving)
        {
            return typeof(TestDanish_TIdleState);
        }

        stateManager.movement_V1.MoveObject(stateManager.moveVector);

        return null;
    }
}
