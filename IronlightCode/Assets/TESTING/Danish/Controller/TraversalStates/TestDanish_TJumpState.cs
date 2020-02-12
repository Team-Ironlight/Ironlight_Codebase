using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_TJumpState : TestDanish_TraversalBaseState
{
    TestDanish_Controller_StateManager_v1 stateManager;

    tDanish_Jump_Component tDanish_Jump_;


    public TestDanish_TJumpState(TestDanish_Controller_StateManager_v1 _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
        tDanish_Jump_ = stateManager.tDanish_Jump;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Jump State");

        tDanish_Jump_.StartJump();
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Jump State");
    }

    public override Type Tick()
    {
        Debug.Log("Jump State");

        if (stateManager.isDashing)
        {
            return typeof(TestDanish_TDashState);
        }

        //if (!stateManager.isMoving)
        //{
        //    return typeof(TestDanish_TIdleState);
        //}

        //stateManager.movement_V1.MoveObject(stateManager.moveVector);

        tDanish_Jump_.Jump();

        return typeof(TestDanish_TIdleState);
    }
}
