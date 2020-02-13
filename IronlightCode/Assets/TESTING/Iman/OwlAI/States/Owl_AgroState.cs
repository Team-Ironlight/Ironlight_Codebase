using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_AgroState : ImanBaseState
{
    Owl_StateManager stateManager;

    private Vector3 DistToAgro;

    public Owl_AgroState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Owl Agro State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Owl Agro State");
    }

    public override Type Tick()
    {
        calculateDist();
        //if owl havent reached position yet
        if (Vector3.Distance(DistToAgro, stateManager.transform.position) > 0.1)
        {
            //get direction between point and owl
            var direction = DistToAgro - stateManager.transform.position;
            //rotate towards point
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
            //move forward
            stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.MovementSpeed);
        }
        //if reached the position
        else
        {
            //get direction to player
            var direction = stateManager.PLY_Transform.position - stateManager.transform.position;
            //rotate
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
        }


        //if player in close distance go to follow state
        //if (Vector3.Distance(stateManager.PLY_Transform.position, stateManager.transform.position) < 10.0f)
        //{
        //    return typeof(TestDanish_TDashState);
        //}

        return null;
    }

    //calculated the position Owl needs to get to
    private void calculateDist()
    {
        //get pos of owl
        var OwlPos = stateManager.transform.position;
        //set y to players y
        OwlPos.y = stateManager.PLY_Transform.position.y;
        //get direction to the player
        DistToAgro = -(stateManager.PLY_Transform.transform.position - OwlPos);
        //normalize the direction and add the distant away from the player
        DistToAgro = DistToAgro.normalized * stateManager.GroundPos;
        //add y displacement
        DistToAgro.y = stateManager.PLY_Transform.position.y + stateManager.YPos;
    }
}
