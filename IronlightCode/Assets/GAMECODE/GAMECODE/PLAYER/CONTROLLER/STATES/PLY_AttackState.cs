using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_AttackState : PLY_BaseState
{
    PLY_StateManager stateManager;
    PLY_AttackComponent attackComponent;

    bool one = false;
    bool two = false;
    bool twoPointFive = false;
    bool three = false;

    public PLY_AttackState(PLY_StateManager state) : base(state.gameObject)
    {
        stateManager = state;
        attackComponent = stateManager.attack;
    }
    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");

        one = stateManager.orb;
        two = stateManager.beamStart;
        twoPointFive = stateManager.beamEnd;
        three = stateManager.radial;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
    }

    public override Type Tick()
    {
        Debug.Log("Currently in Attack State");


        if (one)
        {
            attackComponent.LaunchOrb();
        }

        if (two)
        {
            attackComponent.StartBeam();
        }

        if (twoPointFive)
        {
            attackComponent.EndBeam();
        }

        if (three)
        {
            attackComponent.LaunchRadial();
        }

        if (stateManager.vertical == 0 && stateManager.horizontal == 0)
        {
            return typeof(PLY_IdleState);
        }

        if (stateManager.vertical != 0 || stateManager.horizontal != 0)
        {
            return typeof(PLY_MoveState);
        }

        if (stateManager.orb || stateManager.beamStart || stateManager.beamEnd || stateManager.radial)
        {
            return typeof(PLY_AttackState);
        }

        return null;
    }
}
