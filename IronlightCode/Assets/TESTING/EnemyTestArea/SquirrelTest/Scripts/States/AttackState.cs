//Programmer : Phil James
//Description :  Version 2 (January 16, 2020)
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using IronLight;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AttackState : StateMachine.BaseState
{
    [Header("Target")]
    public Transform target;

    [Header("Decision Making")]                                 //To Do:  Convert this to enum
    public string OnEnemyLostState = "ChaseState";             //If the Player run-away then do Default/assign State

    private string OnEnemyAttackDistance = "AttackState";        //If the Player within Attack Perimeter

    private UnityEngine.AI.NavMeshAgent agent;                  //reference to the navmesh agent.

    [Header("Sensor Color Red")]
    public bool ShowWireSphere_Sensor;
    [Tooltip("Max - Red WireSphere.")]
    [Range(0f, 15f)]
    public float maxDistanceToAttack = 10.0f;                  //Circle Radius, Chase Distance
    private float current_Chase_Distance;
    [Tooltip("Min - Red WireSphere.")]
    [Range(0f, 5f)]
    public float minDistanceToAttack = 2.0f;

    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    [Header("FOV Radar Limits")]
    [SerializeField]
    public float FacingMaxAngle = 45f;                        //Facing Angle allertness at Z axis
                               
    [Range(0, 360)]
    private const float ArcSize = 10.0f;
    private bool isInFov = false;                             //Field of View

    [Header("Abilities")]
    public AI_AbilitySequence Ability;


    public override void OnEnter()                                               // This is called before the first frame Tick()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        Name = this.GetType().ToString();
    }

    public override void Tick()                                                  //Called every frame , Initiate by the StateMachine
    {
        isInFov = inFOV(transform, target, FacingMaxAngle, maxDistanceToAttack);
        if (target != null)
        {

            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // Turn the enemy facing to the Player
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                Quaternion.LookRotation(dirToTarget),
                                1.0f * Time.deltaTime);

            Vector3 destination = transform.position + dirToTarget;


            // Validate if the distance between the player and the enemy
            // if the distance between enemy and player is less than attack distance
            if (Vector3.Distance(transform.position, target.position) <= maxDistanceToAttack)          // Switch to <Attack State>
            {


                if (Vector3.Distance(transform.position, target.position) <= minDistanceToAttack)          // Switch to <Attack State>
                {
                    agent.isStopped = true;
                    StartCoroutine(Ability.Attack1_Coroutine(this));
                }
                else
                {
                    // tell nav agent that he can move
                    agent.isStopped = false;
                    agent.speed = walk_Speed;

                    agent.SetDestination(destination);
                }
            }


        }
    }

    public override string CheckConditions()                                                       // Decisions has been made here
    {
        if (target == null) { return ""; }
        
        Collider[] overlapResults = new Collider[10];
        int numFound = Physics.OverlapSphereNonAlloc(transform.position, maxDistanceToAttack, overlapResults);

        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == target)
                {
                    Debug.DrawLine(transform.position, overlapResults[i].transform.position, Color.red);
                    if (Vector3.Distance(transform.position, target.position) <= maxDistanceToAttack)
                    {
                     
                        return OnEnemyAttackDistance;
                    }
                    else
                    {
                     
                        return OnEnemyLostState;
                    }

                }
            }
        }
 

       return "";

    }

    public override void OnExit()
    {
        // TODO destroy Effects / Animation
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
            //Attack Radius WireSphere
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minDistanceToAttack);

            //Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(transform.position, chase_Distance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxDistanceToAttack);

            //Let us now Rotate the Vector in Horizontally by multiplying the Vector thru Quaternion
            Vector3 fovLine1 = Quaternion.AngleAxis(FacingMaxAngle, transform.up) * transform.forward * maxDistanceToAttack;
            Vector3 fovLine2 = Quaternion.AngleAxis(-FacingMaxAngle, transform.up) * transform.forward * maxDistanceToAttack;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);

            if (!isInFov)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized * maxDistanceToAttack);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistanceToAttack);
        }

    }
}
