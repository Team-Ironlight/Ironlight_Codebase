using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Owl_WindAttackState : ImanBaseState
{
    Owl_StateManager stateManager;

    private Vector3 WindPlayerPos;
    private RaycastHit hit;

    private float timer;

    public Owl_WindAttackState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Wind Attack State");
        calculateWindAttackPositions();
        timer = Time.time + stateManager.WindAttackDuration;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Wind Choose State");

    }

    public override Type Tick()
    {
        var direction = WindPlayerPos - stateManager.transform.position;
        stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.SweepRotateSpeed * Time.deltaTime);
        if(Physics.SphereCast(stateManager.transform.position, stateManager.SphereRadius, direction.normalized , out hit, stateManager.MaxRange, stateManager.Windinteractable))
        {
            if(hit.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("HittinPlayer");
            }
        }
        else
        {
            
        }

        if (timer < Time.time)
        {
            stateManager.WindAttack = false;
            stateManager.StartCoroutine("SlowRotation");
            return typeof(Owl_ChooseAttackState);
        }

        return null;
    }

    private void calculateWindAttackPositions()
    {
        //PlayerPos
        WindPlayerPos = stateManager.PLY_Transform.position;
    }
}
