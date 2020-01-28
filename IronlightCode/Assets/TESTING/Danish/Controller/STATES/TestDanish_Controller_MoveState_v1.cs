using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_Controller_MoveState_v1 : TestDanish_Controller_BaseState_v1
{
    TestDanish_Controller_StateManager_v1 stateManager;


    public TestDanish_Controller_MoveState_v1(TestDanish_Controller_StateManager_v1 state) : base(state.gameObject)
    {
        stateManager = state;
    }

    public override void OnEnter()
    {
        Debug.Log("MoveState: Enter");
    }

    public override void OnExit()
    {
        Debug.Log("MoveState: Exit");
    }

    public override void Tick()
    {
        Debug.Log("MoveState: Current");

        stateManager.currentState = this.GetType().ToString();

        CheckForNextState();
        

        MoveObject();

    }


    public override Type CheckForNextState()
    {
        if (stateManager.isDashing)
        {
            stateManager.machine.SwitchToNewState(typeof(TestDanish_Controller_DashState_v1));
        }
        else if (!stateManager.isMoving)
        {
            stateManager.machine.SwitchToNewState(typeof(TestDanish_Controller_IdleState_v1));
        }


        stateManager.machine.SwitchToNewState(this.GetType());

        return GetType();
    }




    private void MoveObject()
    {
        Vector3 moveAmount = Vector3.zero;

        moveAmount.x = stateManager.moveVector.x;
        moveAmount.z = stateManager.moveVector.y;

        moveAmount *= stateManager.moveSpeed;
        

        stateManager.playerObject.transform.position += moveAmount;
    }
}
