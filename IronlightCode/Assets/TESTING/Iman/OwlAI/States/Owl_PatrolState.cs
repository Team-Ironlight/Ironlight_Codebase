using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_PatrolState : ImanBaseState
{
    Owl_StateManager stateManager;

    public Owl_PatrolState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Owl Patrol State");
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
        stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
        //move forward
        stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.MovementSpeed);

        //rotation
        //stateManager.TurnObject.transform.eulerAngles = Vector3.forward * (90 * Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime));

        //if player in close distance go to follow state
        //if (Vector3.Distance(stateManager.PLY_Transform.position, stateManager.transform.position) < 10.0f)
        //{
        //    return typeof(TestDanish_TDashState);
        //}

        return null;
    }
}
