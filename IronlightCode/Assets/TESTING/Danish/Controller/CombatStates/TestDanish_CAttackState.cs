using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_CAttackState : TestDanish_CombatBaseState
{
    TestDanish_Controller_StateManager_v1 stateManager;

    public TestDanish_CAttackState(TestDanish_Controller_StateManager_v1 _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
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
        Debug.Log("Attack State");


        if (!stateManager.isAttacking)
        {
            return typeof(TestDanish_CReadyState);
        }

        return null;
    }
}
