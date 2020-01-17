//Programmer : Phil James
//Description :  Version 2 (January 16, 2020)
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using IronLight;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PatrolState : StateMachine.BaseState
{
    [Header("Target")]
    private Transform target;

    [Header("Decision Making")]
    public string OnEnemyLostState = "PatrolState";                               //To Do:  Convert this to enum
    private string OnEnemyPatrolDistance = "PatrolState";                       //To Do:  Convert this to enum

    [Header("Patrol Waypoints")]
    public Transform[] waypoints;                                               //Array of waypoints is only used when waypoint wandering is selected
    private Vector3 patrolPoint;
    private int waypointIndex;
    private bool isAware = false;
   


    [Header("Sensor Color Blue.")]
    public bool ShowWireSphere_Sensor;
    [Tooltip("Min - Blue WireSphere.")]
    [Range(0f, 20f)]
    public float minDistanceToPatrol = 5.0f;
    [Tooltip("Max - Blue WireSphere.")]
    [Range(0f, 40f)]
    public float maxDistanceToPatrol = 20.0f;

    private UnityEngine.AI.NavMeshAgent agent;                                  //reference to the navmesh agent.


    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    [Header("FOV Radar Limits")]
    [SerializeField]
    public float FacingMaxAngle = 45f;                                         //Facing Angle allertness at Z axis
    private bool isInFov = false;                                                //Field of View
                                                                                 //Local Variables
  



    public override void OnEnter()                                              // This is called before the first frame Tick()
    {
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag("Player").transform;

        Name = this.GetType().ToString();
    }

    public override void Tick()                                                 //Called every frame , Initiate by the StateMachine
    {
        Vector3 dirToTarget = Vector3.zero;
        Vector3 destination = Vector3.zero;


        if (target != null)
        {
            if (isAware)
            {
                isInFov = inFOV(transform, target, FacingMaxAngle, maxDistanceToPatrol);

               dirToTarget = (target.position - transform.position).normalized;

                // Turn the enemy facing to the Player
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(dirToTarget),
                                    1.0f * Time.deltaTime);

               destination = transform.position + dirToTarget;


                // Validate if the distance between the player and the enemy
                // if the distance between enemy and player is less than attack distance
                if (Vector3.Distance(transform.position, target.position) <= maxDistanceToPatrol)          // Switch to <Attack State>
                {
                    // tell nav agent that he can move
                    agent.isStopped = false;
                    agent.speed = walk_Speed;

                    agent.SetDestination(destination);
                }
                else if (Vector3.Distance(transform.position, target.position) > maxDistanceToPatrol)
                {
                    agent.isStopped = true;
                    isAware = false;
                }
            }
            else
            {
                patrolPoint = Patrol();
                dirToTarget = (patrolPoint - transform.position).normalized;

                // Turn the enemy facing to the Player
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(dirToTarget),
                                   walk_Speed * Time.deltaTime);

                destination = transform.position + dirToTarget;

                isInFov = inFOV(transform, agent.transform, FacingMaxAngle, maxDistanceToPatrol);

                try
                {
                    agent.isStopped = false;
                    agent.speed = walk_Speed;

                    agent.SetDestination(destination);
                }
                catch
                {
                }
            }

        }

        
    }


    public override string CheckConditions()                                        //Decisions has been made here
    {

        if (target == null) { return ""; }

        Collider[] overlapResults = new Collider[10];
        int numFound = Physics.OverlapSphereNonAlloc(transform.position, maxDistanceToPatrol, overlapResults);

        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == target)
                {
                    Debug.DrawLine(transform.position, overlapResults[i].transform.position, Color.red);
                    if (Vector3.Distance(transform.position, target.position) < minDistanceToPatrol)
                    {
                       
                        return OnEnemyPatrolDistance;
                    }
                    else
                    {
                        OnAware();
                        return OnEnemyLostState;
                    }

                }
            }
        }


        return "";
    }


    public override void OnExit()
    {
        //To Do:  Destroy effects / animation
    }

    public void OnAware()
    {
        isAware = true;
    }
    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * maxDistanceToPatrol) + transform.position;
        
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, maxDistanceToPatrol, UnityEngine.AI.NavMesh.AllAreas);  //-1
                
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    private bool SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(target.transform.position)) < FacingMaxAngle / 2f)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < maxDistanceToPatrol)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, target.transform.position, out hit, -1))
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                        return true;

                    }
            }
        }
        return false;
    }
    public Vector3 Patrol()                                                                             //To Do:  next version will remove this, not needed
    {
        if (waypoints.Length >= 2)
        {
            if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 2f)
            {
                if (waypointIndex == waypoints.Length - 1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex++;
                }
            }
        }
        else
        {
            Debug.LogWarning("Please assign more than 1 waypoint to the AI: " + gameObject.name);
        }

        
        return new Vector3(waypoints[waypointIndex].position.x, waypoints[waypointIndex].position.y, waypoints[waypointIndex].position.z);
    }
    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (ShowWireSphere_Sensor)
        {
            if (minDistanceToPatrol >= maxDistanceToPatrol)
            {
                Debug.LogWarning("Please assign less than value to the minDistanceToWander: " + gameObject.name);
            }
            //Minimum Distance WireSphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minDistanceToPatrol);

            //Maximum Distance WireSphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxDistanceToPatrol);

            //Let us now Rotate the Vector in Horizontally by multiplying the Vector thru Quaternion
            Vector3 fovLine1 = Quaternion.AngleAxis(FacingMaxAngle, transform.up) * transform.forward * maxDistanceToPatrol;
            Vector3 fovLine2 = Quaternion.AngleAxis(-FacingMaxAngle, transform.up) * transform.forward * maxDistanceToPatrol;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);

            if (!isInFov)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (patrolPoint - transform.position).normalized * maxDistanceToPatrol);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistanceToPatrol);
        }


    }
}
