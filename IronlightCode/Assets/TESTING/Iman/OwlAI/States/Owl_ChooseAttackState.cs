using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_ChooseAttackState : ImanBaseState
{
    Owl_StateManager stateManager;

    private int AttackChoose;

    System.Random rnd = new System.Random();

    public Owl_ChooseAttackState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }



    public override void OnEnter()
    {
        Debug.Log("Entering Choose Attack State");
        AttackChoose = rnd.Next(1, 3);
        Debug.Log(AttackChoose);
    }

    public override void OnExit()
    {
        Debug.Log("Exiting choose Attack State");

    }

    public override Type Tick()
    {

        //if player in close distance go to follow state
        if (AttackChoose == 1)
        {
            return typeof(Owl_AgroState);
        }

        if (AttackChoose == 2)
        {
            return typeof(Owl_WindAgroState);
        }

        return null;
    }
}
