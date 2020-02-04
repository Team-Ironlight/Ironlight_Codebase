// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/23/2020
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using IronLight;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AI_AbilityManager))]
public class AttackState : StateMachine.BaseState
{
    [Header("Target")]
    public Transform target;

    [Header("Shared Components")]
    public Rigidbody arcRigidBody;
    public UnityEngine.AI.NavMeshAgent _navMeshAgent;            //reference to the navmesh agent.
    public Animator _aniMator;


    [Header("Decision Making")]                                 //To Do:  Convert this to enum
    public string OnEnemyLostState = "ChaseState";             //If the Player run-away then do Default/assign State

    private string OnEnemyAttackDistance = "AttackState";        //If the Player within Attack Perimeter
    private bool _playerRunAway = false;


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
    public float FacingMaxAngle = 45f;                                          //Facing Angle allertness at Z axis
                               
    [Range(0, 360)]
    private const float ArcSize = 10.0f;
    private bool isInFov = false;                                               //Field of View

    [Header("Abilities")]
    public bool multipleAttack;
    public AI_CoroutineManager Ability1;                                        //TO DO : Change this to ArrayList
    public AI_CoroutineManager Ability2;
    public AI_CoroutineManager Ability3;
    public AI_CoroutineManager Default_Ability;
    private AI_AbilityManager _executeAbility;


    [HideInInspector]public bool isCharging;
    [HideInInspector]public float wait_Before_Attack = 2f;                    //Cooling Attack
    private float attack_Timer;                                               //Cooling Attack


    
    private Vector3 fovLine1;                                                   //Local Variables
    private Vector3 fovLine2;
    private bool Bool_fovLine1;
    private bool Bool_fovLine2;

    [HideInInspector]public bool isOnAttackMode = false;

    public override void OnEnter()                                               // This is called before the first frame Tick()
    {
        //Validation Check, to ensure all components below are not empty.
        if(arcRigidBody == null) { Debug.LogWarning("Please assign the RigidBody on this Field."); return; }
        if (_navMeshAgent == null) { Debug.LogWarning("Please assign the NavmeshAgent on this Field."); return; }
        if (_aniMator == null) { Debug.LogWarning("Please assign the NavmeshAgent on this Field."); return; }
        
        
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();             //Initialized        
        _executeAbility = GetComponent<AI_AbilityManager>();
        target = GameObject.FindWithTag("Player").transform;
        Name = this.GetType().ToString();

        _executeAbility.Set_MaxDistance = maxDistanceToAttack;
        _executeAbility.Set_MinDistance = minDistanceToAttack;
    }

    public override void Tick()                                                  //Called every frame , Initiate by the StateMachine
    {
        isInFov = inFOV(transform, target, FacingMaxAngle, maxDistanceToAttack);
        if (target != null)
        {

            if (_navMeshAgent.enabled == true)
            {
                Vector3 destination = Vector3.zero;

                Vector3 dirToTarget = (target.position - transform.position).normalized;

                // Turn the enemy facing to the Player
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(dirToTarget),
                                    1.0f * Time.deltaTime);

                destination = transform.position + dirToTarget;

                

                // Validate if the distance between the player and the enemy
                // if the distance between enemy and player is less than attack distance
                if (Vector3.Distance(transform.position, target.position) <= maxDistanceToAttack)          //  <Attack State>
                {
                    _navMeshAgent.isStopped = false;

                    _navMeshAgent.speed = walk_Speed;
                    _navMeshAgent.SetDestination(destination);

                    isCharging = target.GetComponentInChildren<LightCharging>().isCharging;
                    if(isCharging)
                    {
                        _executeAbility.enabled = false;
                        StartCoroutine(coroutineTrigger(isCharging, multipleAttack));
                      
                    }

                }
                else if ((Vector3.Distance(transform.position, target.position) >= minDistanceToAttack))
                {
                    _navMeshAgent.isStopped = true;                     //Stop the agent when the player already outside the perimeter
                    _playerRunAway = true;
                }

            } //End if (_navMeshAgent.enabled == true)
        }
    }

    public override string CheckConditions()                                                                     // Decisions has been made here
    {
        if (target == null) { return ""; }

        if (_playerRunAway)                                                                                     // ToDo: SetBack to the Previous allocated Location
        {

            _playerRunAway = false;

          
            return OnEnemyLostState;
        }

        Collider[] overlapResults = new Collider[20];
        int numFound = Physics.OverlapSphereNonAlloc(transform.position, maxDistanceToAttack, overlapResults);

        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == target)
                {

                    if ((Vector3.Distance(transform.position, target.position) >= maxDistanceToAttack))
                    {
                      
                        return OnEnemyAttackDistance;
                    }
                    else if (Vector3.Distance(transform.position, target.position) <= minDistanceToAttack)           // Switch to <Attack State>
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

   
    IEnumerator coroutineTrigger(bool isCharging, bool multiple)              //Synchronous Coroutine , Multiple Attack will do here
    {

        //*- Execute Orbit Ability
        if (isCharging)
        {
            _executeAbility.enabled = false;
            yield return StartCoroutine(Ability1.Rotate_Coroutine(this, minDistanceToAttack, maxDistanceToAttack, isCharging));
            _executeAbility.enabled = true;
            yield break;    //turn off
        }


        isCharging = target.GetComponentInChildren<LightCharging>().isCharging;

        //*- Execute Swag Ability
        attack_Timer += Time.deltaTime;                   // Cooling Attack
        if (attack_Timer > wait_Before_Attack)
        {
            if (!isCharging) yield return StartCoroutine(Default_Ability.Swag_Coroutine(this, minDistanceToAttack, maxDistanceToAttack));

            attack_Timer = 0f;
            yield return null;                              //return next frame
        }

   
        yield return null;                                  //return next frame



        //*- Execute Jump Attack
        //attack_Timer += Time.deltaTime;                    // Cooling Attack

        //if (attack_Timer > wait_Before_Attack)
        //{
        //    isCharging = target.GetComponentInChildren<LightCharging>().isCharging;

        //    if ((multiple) && (!isCharging)) yield return StartCoroutine(Ability2.Jump(this, 1f, minDistanceToAttack, maxDistanceToAttack));
                 
        //        attack_Timer = 0f;
        
        //    yield return null;                             //return next frame
        //}




        yield break;                                    //turn off
    }


    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[100];  //assuming we are surrounded of 100 colliders e.g(mesh collider,sphere, cabsule, box, etc.)
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
            //----
            Ray ray1 = new Ray(transform.position, fovLine1); RaycastHit hit1;                                          //Checking Left / Right if we hit something ?
            if (Physics.Raycast(ray1, out hit1, maxDistanceToAttack)) {
                Bool_fovLine1 = true;
            } else { Bool_fovLine1 = false; }

            Ray ray2 = new Ray(transform.position, fovLine2); RaycastHit hit2;
            if (Physics.Raycast(ray2, out hit2, maxDistanceToAttack)) {
                Bool_fovLine2 = true;
            } else { Bool_fovLine1 = false; }

            //----

            if (!isInFov)
            {
                Gizmos.color = Color.red;
                // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, target.transform.localEulerAngles.y, transform.localEulerAngles.z);
             
            }
            else
            {
                Gizmos.color = Color.green;
                
            }
            transform.LookAt(target);
            Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized * maxDistanceToAttack);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistanceToAttack);
        }

    }
}



































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh