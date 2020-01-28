using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_CReadyState : TestDanish_CombatBaseState
{
    TestDanish_Controller_StateManager_v1 stateManager;

    public TestDanish_CReadyState(TestDanish_Controller_StateManager_v1 _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Ready State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Ready State");
    }

    public override Type Tick()
    {
        Debug.Log("Ready State");

        if (stateManager.isAttacking)
        {
            return typeof(TestDanish_CAttackState);
        }

        return null;
    }
}
