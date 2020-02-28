using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_AgroState : ImanBaseState
{
    Owl_StateManager stateManager;

    private Vector3 AgroPos;
    //bankRotation
    private float Y1;
    private float Y2;

    public Owl_AgroState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Sweep Agro State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Sweep Agro State");
    }

    public override Type Tick()
    {
        calculateAgroPos();
        //stateManager.SlowingDown(AgroPos);
        //if owl havent reached position yet
        if (Vector3.Distance(AgroPos, stateManager.transform.position) > 0.3)
        {
            //get direction between point and owl
            var direction = AgroPos - stateManager.transform.position;
            //rotate towards point
            Y1 = stateManager.transform.eulerAngles.y;
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
            Y2 = stateManager.transform.eulerAngles.y;
            //move forward
            stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.MovementSpeed);
        }
        //if reached the position
        else
        {
            //get direction to player
            var PPos = stateManager.PLY_Transform.position;
            PPos.y = stateManager.transform.position.y;
            var direction = PPos - stateManager.transform.position;
            //rotate
            Y1 = stateManager.transform.eulerAngles.y;
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
            Y2 = stateManager.transform.eulerAngles.y;
            //move forward
            //stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.MovementSpeed);
        }

        //bank rotation
        stateManager.BankRotationCalc(Y1, Y2);

        //if player in close distance go to follow state
        if (stateManager.DisBetwnPLY > stateManager.DistToPatrol)
        {
            return typeof(Owl_PatrolState);
        }

        if(stateManager.SweepAttack)
        {
            return typeof(Owl_SweepAttackState);
        }

        return null;
    }

    //calculated the position Owl needs to get to
    private void calculateAgroPos()
    {
        //get pos of owl
        var OwlPos = stateManager.transform.position;
        //set y to players y
        OwlPos.y = stateManager.PLY_Transform.position.y;
        //get direction to the player
        AgroPos = -(stateManager.PLY_Transform.position - OwlPos);
        //normalize the direction and add the distant away from the player
        AgroPos = AgroPos.normalized * stateManager.Sweep_GroundPos;
        //add to players position
        AgroPos = AgroPos + stateManager.PLY_Transform.position;
        //add y displacement
        AgroPos.y = stateManager.PLY_Transform.position.y + stateManager.Sweep_YPos;
    }
}
