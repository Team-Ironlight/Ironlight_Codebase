using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Owl_StateManager : MonoBehaviour
{
    [Header("Movement Variables")]
    public float MovementSpeed =3;
    private float OGMovementSpeed;
    public float RotationSpeed =4;
    private float OGRotationSpeed;
    private Rigidbody rb;
    public float BankRotIntensity;
    public float BankRotSpeed;
    private float ZChange;

    private float startTime;

    [Header("Patrol Variables")]
    public GameObject[] WayPoints;
    [HideInInspector] public int CurrentWP;
    public float DistToAgro;

    [Header("General Agro Variables")]
    public float DistToReAgro;
    public float TimeTillWarning;

    [Header("sweep Agro Variables")]
    public float Sweep_YPos;
    public float Sweep_GroundPos;
    public float DistToPatrol;
    
    [Header("Sweep Attack related Variables")]
    public float SweepMoveSpeed;
    public float SweepRotateSpeed;
    public bool SweepAttack;

    [Header("Wind Agro Variables")]
    public float Wind_YPos;
    public float Wind_GroundPos;

    [Header("Wind Attack Variables")]
    public float WindForce;
    public float SphereRadius;
    public float MaxRange;
    public float WindAttackDuration;
    public LayerMask Windinteractable;
    public bool WindAttack;

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
        WindAttack = false;
        FindWaypoint();
        OGMovementSpeed = MovementSpeed;
        OGRotationSpeed = RotationSpeed;

        startTime = Time.time;

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

    public void SlowingDown(Vector3 Target)
    {
        if (Vector3.Distance(Target, transform.position) < 0.3)
        {
           // MovementSpeed = Mathf.Lerp(OGMovementSpeed, 0, 3);

            // Set our position as a fraction of the distance between the markers.
            MovementSpeed = Mathf.Lerp(OGMovementSpeed, 0, (Vector3.Distance(Target, transform.position)/0.3f));
        }
        else
        {
            MovementSpeed = OGMovementSpeed;
        }
    }

    IEnumerator SlowRotation()
    {
        RotationSpeed = 1;

        yield return new WaitForSeconds(3);
        RotationSpeed = OGRotationSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SphereRadius);
    }

    #region Setup Functions

    void InitializeTraversalMachine()
    {
        var states = new Dictionary<Type, ImanBaseState>()
        {
            {typeof(Owl_PatrolState), new Owl_PatrolState(_Manager:this) },
            {typeof(Owl_ChooseAttackState), new Owl_ChooseAttackState(_Manager:this) },
            {typeof(Owl_WindAgroState), new Owl_WindAgroState(_Manager:this) },
            {typeof(Owl_WindAttackState), new Owl_WindAttackState(_Manager:this) },
            {typeof(Owl_AgroState), new Owl_AgroState(_Manager:this) },
            {typeof(Owl_SweepAttackState), new Owl_SweepAttackState(_Manager:this) }
        };

        StateMachine.SetStates(states);
    }
    #endregion
}
