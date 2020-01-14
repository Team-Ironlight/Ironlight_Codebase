//Programmer : Phil James
//Description :  Version 1 (January 14, 2020)
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IronLight;

// Display Mode that the Custom Inspector of an AIWaypointNetwork
// component can be in
public enum PathDisplayMode { None, Connections, Paths }

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PatrolState : StateMachine.BaseState
{

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding

   
    private Transform target;
  

   
    [HideInInspector]
    public PathDisplayMode DisplayMode = PathDisplayMode.Connections;                                       // Current Display Mode
    [HideInInspector]
    public int UIStart = 0;                                                                                 // Start wayopoint index for Paths mode
    [HideInInspector]
    public int UIEnd = 0;                                                                                   // End waypoint index for Paths mode
    [HideInInspector]
    public UnityEngine.AI.NavMeshPathStatus PathStatus = UnityEngine.AI.NavMeshPathStatus.PathInvalid;      // Precaution Check

    [Header("Waypoints")]  
    public List<Transform> Waypoints = new List<Transform>();                                               // List of Transform references
    [Header("-------------")]

    [Header("Timing")]
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;
    public string PatrolFinishState = "FleeState";                                                          //To Do:  Convert this to enum
    private string StillPatrolling = "PatrolState";                                                         //To Do:  Convert this to enum

    [Header("Movement Control")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    //Waypoints
    [HideInInspector]
    public bool PathPending = false;
    [HideInInspector]
    public bool PathStale = false;
    public int CurrentIndex = 0;
    private int incStep;
    //  private int nextWaypoint;
    private float _originalMaxSpeed = 0;                                                    //Precaution - if the path was Stale ?


    public override void OnEnter()                                                          // This is called before the first frame Tick()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;

        patrol_Timer = patrol_For_This_Time;

 
    }
    public override void Tick()                                                             //Called every frame , Initiate by the StateMachine
    {
        // If not valid Waypoint Network has been assigned then return
        if (Waypoints == null) return;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time)
        {
           
            // If we don't have a path and one isn't pending then set the next
            // waypoint as the target, otherwise if path is stale regenerate path
            // if ((agent.remainingDistance <= agent.stoppingDistance && !PathPending) || PathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid /*|| PathStatus==NavMeshPathStatus.PathPartial*/)
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                SetNextDestination(true);

            }
            else if (agent.isPathStale)
            {
                SetNextDestination(false);
            }

            patrol_Timer = 0f;
        }


    }

    public override string CheckConditions()                                                 //Decisions has been made here
    {
        // -----------------------------------------------------
        // Programmer : Phil
        // Desc	:	
        //			In progress
        //			
        // -----------------------------------------------------

        //if (nextWaypoint <= Waypoints.Count)
        //{
        //    nextWaypoint = CurrentIndex + incStep;
        //}
        //if (CurrentIndex == Waypoints.Count)
        //{
        //    Debug.Log(CurrentIndex);
        //    return PatrolFinishState;
        //}
        //else
        return "";
    }
    public override void OnExit()
    {
        //To Do  Destroy effects / animation
    }

    // -----------------------------------------------------
    // Programmer : Phil
    // Desc	:	Optionally increments the current waypoint
    //			index and then sets the next destination
    //			for the agent to head towards.
    // -----------------------------------------------------
    void SetNextDestination(bool increment)
    {
        if (Waypoints == null) return;

        // tell nav agent that he can move
        agent.isStopped = false;
        agent.speed = walk_Speed;

        // Calculatehow much the current waypoint index needs to be incremented
        incStep = increment ? 1 : 0;
        Transform nextWaypointTransform = null;

        //  nextWaypoint = (CurrentIndex + incStep <= Waypoints.Count) ? 0 : CurrentIndex + incStep;
        // Calculate index of next waypoint factoring in the increment with wrap-around and fetch waypoint 
       int nextWaypoint = (CurrentIndex + incStep >= Waypoints.Count) ? 0 : CurrentIndex + incStep;          
        nextWaypointTransform = Waypoints[nextWaypoint];

        // Assuming we have a valid waypoint transform
        if (nextWaypointTransform != null)
        {
            // Update the current waypoint index, assign its position as the NavMeshAgents
            // Destination and then return
            CurrentIndex = nextWaypoint;
            agent.destination = nextWaypointTransform.position;
            return;
        }

        // We did not find a valid waypoint in the list for this iteration
        CurrentIndex = nextWaypoint;
    }
    // To Do: Replacement for this is coming soon
    void SetNewRandomDestination()
    {


       // enemy_State = EnemyState.PATROL;


        //float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        //Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        //randDir += transform.position;

        //NavMeshHit navHit;

        //NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        //navAgent.SetDestination(navHit.position);

        int turnOnSpot;

        // Copy NavMeshAgents state into inspector visible variables
      //  HasPath = navAgent.hasPath;
        PathPending = agent.pathPending;
        PathStale = agent.isPathStale;
        PathStatus = agent.pathStatus;

        // Perform corss product on forard vector and desired velocity vector. If both inputs are Unit length
        // the resulting vector's magnitude will be Sin(theta) where theta is the angle between the vectors.
        Vector3 cross = Vector3.Cross(transform.forward, agent.desiredVelocity.normalized);

        // If y component is negative it is a negative rotation else a positive rotation
        float horizontal = (cross.y < 0) ? -cross.magnitude : cross.magnitude;

        // Scale into the 2.32 range for our animator
        horizontal = Mathf.Clamp(horizontal * 2.32f, -2.32f, 2.32f);

        // If we have slowed down and the angle between forward vector and desired vector is greater than 10 degrees 
        if (agent.desiredVelocity.magnitude < 1.0f && Vector3.Angle(transform.forward, agent.steeringTarget - transform.position) > 10.0f)
        {
            // Stop the nav agent (approx) and assign either -1 or +1 to turnOnSpot based on sign on horizontal
            agent.speed = 10f;
            turnOnSpot = (int)Mathf.Sign(horizontal);
        }
        else
        {
            // Otherwise it is a small angle so set Agent's speed to normal and reset turnOnSpot
            agent.speed = _originalMaxSpeed;
            turnOnSpot = 0;
        }


    }
}
