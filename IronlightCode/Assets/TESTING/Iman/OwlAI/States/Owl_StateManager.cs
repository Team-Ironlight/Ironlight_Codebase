using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Owl_StateManager : MonoBehaviour
{
    [Header("Movement Variables")]
    public float MovementSpeed =3;
    public float RotationSpeed =4;
    private Rigidbody rb;
    public GameObject TurnObject;

    [Header("Patrol Variables")]
    public GameObject[] WayPoints;
    [HideInInspector] public int CurrentWP;

    [Header("Agro Variables")]
    public float YPos;
    public float GroundPos;

    [Header("Player related Variables")]
    [HideInInspector] public Transform PLY_Transform;
    [HideInInspector] public float DisBetwnPLY;


    public Iman_StateMachine StateMachine;

    public void Init()
    {
        //initializing
        rb = GetComponent<Rigidbody>();
        //PLY_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StateMachine = GetComponent<Iman_StateMachine>();

        InitializeTraversalMachine();
    }

    public void Tick()
    {
        //DisBetwnPLY = Vector3.Distance(PLY_Transform.position, transform.position);
    }

    #region Setup Functions

    void InitializeTraversalMachine()
    {
        var states = new Dictionary<Type, ImanBaseState>()
        {
            {typeof(Owl_PatrolState), new Owl_PatrolState(_Manager:this) },
            {typeof(Owl_AgroState), new Owl_AgroState(_Manager:this) }
        };

        StateMachine.SetStates(states);
    }

    #endregion
}
