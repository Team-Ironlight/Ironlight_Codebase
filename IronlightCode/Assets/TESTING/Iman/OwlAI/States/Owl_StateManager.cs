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
    public float BankRotIntensity;
    public float BankRotSpeed;
    private float ZChange;

    [Header("Patrol Variables")]
    public GameObject[] WayPoints;
    [HideInInspector] public int CurrentWP;
    public float DistToAgro;

    [Header("Agro Variables")]
    public float YPos;
    public float GroundPos;
    public float DistToPatrol;

    [Header("Sweep Attack related Variables")]
    public float SweepMoveSpeed;
    public float SweepRotateSpeed;
    public bool SweepAttack;

    [Header("Player related Variables")]
    [HideInInspector] public Transform PLY_Transform;
    [HideInInspector] public float DisBetwnPLY;


    public Iman_StateMachine StateMachine;
    public GameObject TurnObject;

    public void Init()
    {
        //initializing
        rb = GetComponent<Rigidbody>();
        PLY_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StateMachine = GetComponent<Iman_StateMachine>();
        SweepAttack = false;
        FindWaypoint();

        InitializeTraversalMachine();
    }

    public void Tick()
    {
        //distance between owl and the player
        DisBetwnPLY = Vector3.Distance(PLY_Transform.position, transform.position);
        //print(DisBetwnPLY);
    }

    //BankRotation calculation
    public void BankRotationCalc(float Y1, float Y2)
    {
        //get the change
        ZChange = Y2 - Y1;
        //increase by set amount
        ZChange *= BankRotIntensity;

        var rot = new Vector3(0, 0, ZChange);
        //add the changes
        TurnObject.transform.localRotation = Quaternion.Slerp(TurnObject.transform.localRotation, Quaternion.Euler(rot), BankRotSpeed * Time.deltaTime);
    }

    //function to find the closest waypoint
    public void FindWaypoint()
    {
        //shuffle through all waypoints
        for (int i = 0; i < WayPoints.Length; i++)
        {
            //find the closest waypoint and set it to current waypoint
            if (Vector3.Distance(WayPoints[i].transform.position, transform.position) < Vector3.Distance(WayPoints[CurrentWP].transform.position, transform.position))
            {
                CurrentWP = i;
            }
        }
    }

    #region Setup Functions

    void InitializeTraversalMachine()
    {
        var states = new Dictionary<Type, ImanBaseState>()
        {
            {typeof(Owl_PatrolState), new Owl_PatrolState(_Manager:this) },
            {typeof(Owl_AgroState), new Owl_AgroState(_Manager:this) },
            {typeof(Owl_SweepAttackState), new Owl_SweepAttackState(_Manager:this) }
        };

        StateMachine.SetStates(states);
    }
    #endregion
}
