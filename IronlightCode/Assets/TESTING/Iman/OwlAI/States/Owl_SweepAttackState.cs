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

    float timer;

    //bankRotation
    private float Y1;
    private float Y2;

    public Owl_SweepAttackState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Owl Sweep Attack State");
        calculateSweepAttackPositions();
        timer = 1000000000;
        
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Owl Sweep Attack State");
        stateManager.SweepRotateSpeed = 6;
    }

    public override Type Tick()
    {
        //if owl reached player go to end pos
        if (Vector3.Distance(SweepPlayerPos, stateManager.transform.position) < 0.1)
        {
            SweepTarget = SweepEndPos;
            stateManager.SweepRotateSpeed = 2;
            timer = Time.time + 3;
            stateManager.OwlAnim.SetBool("Idle", false);
            stateManager.OwlAnim.SetTrigger("FlyUp");
            stateManager.OwlAnim.SetBool("Dive", false);
        }
        var direction = SweepTarget - stateManager.transform.position;
        Y1 = stateManager.transform.eulerAngles.y;
        stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.SweepRotateSpeed * Time.deltaTime);
        Y2 = stateManager.transform.eulerAngles.y;
        //move forward
        stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.SweepMoveSpeed);

        //bank rot
        stateManager.BankRotationCalc(Y1, Y2);

        //when reached end attack pos switch to agro state
        //if (Vector3.Distance(SweepEndPos, stateManager.transform.position) < 0.6)
        if(timer < Time.time)
        {
            stateManager.SweepAttack = false;
            return typeof(Owl_ChooseAttackState);
        }
        return null;
    }

    private void calculateSweepAttackPositions()
    {
        //End owl Pos
        var PPos = stateManager.PLY_Transform.position;
        PPos.y = stateManager.transform.position.y;
        SweepEndPos = ((PPos - stateManager.transform.position).normalized * stateManager.Sweep_GroundPos) + stateManager.PLY_Transform.position;
        SweepEndPos.y = stateManager.PLY_Transform.position.y + stateManager.Sweep_YPos;

        //PlayerPos
        SweepPlayerPos = stateManager.PLY_Transform.position;

        //set first target
        SweepTarget = SweepPlayerPos;
    }
}
