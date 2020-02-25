using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_PatrolState : ImanBaseState
{
    Owl_StateManager stateManager;

    //bankRotation
    private float Y1;
    private float Y2;

    public Owl_PatrolState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Owl Patrol State");
        stateManager.FindWaypoint();
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Owl Patrol State");
    }

    public override Type Tick()
    {
        //checked if reached the waypoint if yes set the next way point
        if (Vector3.Distance(stateManager.WayPoints[stateManager.CurrentWP].transform.position, stateManager.transform.position) < 1.0f)
        {
            stateManager.CurrentWP++;
            if (stateManager.CurrentWP >= stateManager.WayPoints.Length)
            {
                stateManager.CurrentWP = 0;
            }
        }

        //rotate towards next point
        var direction = stateManager.WayPoints[stateManager.CurrentWP].transform.position - stateManager.transform.position;
        Y1 = stateManager.transform.eulerAngles.y;
        stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
        Y2 = stateManager.transform.eulerAngles.y;
        //move forward
        stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.MovementSpeed);

        //bank rotation
        stateManager.BankRotationCalc(Y1, Y2);

        //if player in close distance go to follow state
        if (stateManager.DisBetwnPLY < stateManager.DistToAgro)
        {
            return typeof(Owl_AgroState);
        }

        return null;
    }
}
