using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_SweepAttackState : ImanBaseState
{
    Owl_StateManager stateManager;

    private Vector3 SweepEndPos;
    private Vector3 SweepPlayerPos;
    private Vector3 SweepTarget;

    public Owl_SweepAttackState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Owl Sweep Attack State");
        calculateAttackPositions();
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Owl Sweep Attack State");
    }

    public override Type Tick()
    {
        //if owl reached player go to end pos
        if (Vector3.Distance(SweepPlayerPos, stateManager.transform.position) < 0.1)
        {
            SweepTarget = SweepEndPos;
        }
        var direction = SweepTarget - stateManager.transform.position;
        stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.SweepRotateSpeed * Time.deltaTime);
        //move forward
        stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.SweepMoveSpeed);

        //if player in close distance go to follow state
        //if (Vector3.Distance(stateManager.PLY_Transform.position, stateManager.transform.position) < 10.0f)
        //{
        //    return typeof(TestDanish_TDashState);
        //}

        if (Vector3.Distance(SweepEndPos, stateManager.transform.position) < 0.4)
        {
            //change state
        }
        return null;
    }

    private void calculateAttackPositions()
    {
        //End owl Pos
        var PPos = stateManager.PLY_Transform.position;
        PPos.y = stateManager.transform.position.y;
        SweepEndPos = ((PPos - stateManager.transform.position).normalized * stateManager.GroundPos) + stateManager.PLY_Transform.position;
        SweepEndPos.y = stateManager.PLY_Transform.position.y + stateManager.YPos;

        //PlayerPos
        SweepPlayerPos = stateManager.PLY_Transform.position;

        //set first target
        SweepTarget = SweepPlayerPos;
    }
}
