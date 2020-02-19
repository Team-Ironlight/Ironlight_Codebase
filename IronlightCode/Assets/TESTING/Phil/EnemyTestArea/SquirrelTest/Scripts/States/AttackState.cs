// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James    /  Alteration dates below
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using IronLight;


[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New AttackState")]
public class AttackState : StateMachine.BaseState
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif

    [Header("Target")]
    private Transform _mTarget;

    [Header("Components Entity")]
    //public Transform ComponentContainer;
    //private Rigidbody arcRigidBody;
    private NavMeshAgent _navMeshAgent;                             //reference to the navmesh agent.
    private Animator _aniMator;
    //private StateMachine mCurrentBaseState;

    [Header("Decision Making")]                                     //To Do:  Convert this to enum
    public string OnEnemyLostState = "ChaseState";                  //If the Player run-away then do Default/assign State

    private string OnEnemyAttackDistance = "AttackState";           //If the Player within Attack Perimeter
    private bool _playerRunAway = false;


    // [Header("Sensor Color Red")]
    // public bool ShowWireSphere_Sensor;
    // [Tooltip("Max - Red WireSphere.")]
    // [Range(0f, 15f)]
    private float _maxDistanceToAttack;                       //Circle Radius, Chase Distance
    //// private float current_Chase_Distance;
    // [Tooltip("Min - Red WireSphere.")]
    // [Range(0f, 5f)]
    private float _minDistanceToAttack;

    [Header("Movement")]
    [SerializeField] float _slerpSpeed = 5.0f;
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    //[Header("FOV Radar Limits")]
    //[SerializeField]
    //public float FacingMaxAngle = 45f;                              //Facing Angle allertness at Z axis
                               
    //[Range(0, 360)]
    //private const float ArcSize = 10.0f;
    //private bool isInFov = false;                                   //Field of View

    [Header("Special Movement")]
    public AI_SpecialMoveCoroutine SpecialMovement1;             
    public AI_SpecialMoveCoroutine SpecialMovement2;
    public AI_SpecialMoveCoroutine SpecialMovement3; 
    private AI_AbilityManager _updateMinMax;                         //Telling the Random Attacks about the MinMax Perimeter


    [HideInInspector]public bool isCharging;
    [HideInInspector]public float wait_Before_Attack = 2f;          //Cooling Attack
    private float attack_Timer;                                     //Cooling Attack
    
    //private Vector3 fovLine1;                                       //Local Variables
    //private Vector3 fovLine2;
    //private bool Bool_fovLine1;
    //private bool Bool_fovLine2;

    [HideInInspector]public bool isOnAttackMode = false;

    // private StateMachine _mStateMachine;
    private MonoBehaviour _mRunner;

    public override void OnEnter(MonoBehaviour runner)                                                                                                                          // This is called before the first frame 
    {
        _mRunner = runner;
        _mTarget = GameObject.FindWithTag("Player").transform;
        _navMeshAgent = runner.GetComponent<NavMeshAgent>();
        _aniMator = runner.GetComponent<Animator>();                                                                                                                            //Initialized      
        _updateMinMax = runner.GetComponent<AI_AbilityManager>();
       
        Name = this.GetType().ToString();
               
        _maxDistanceToAttack = runner.GetComponent<StateMachine>().Get_MaxDistanceAttack;
        _minDistanceToAttack = runner.GetComponent<StateMachine>().Get_MinDistanceAttack;

        _updateMinMax.Set_MaxDistance = _maxDistanceToAttack;
        _updateMinMax.Set_MinDistance = _minDistanceToAttack;
    }

    public override void Tick(MonoBehaviour runner)                                                                                                                             //Called every frame after the First Frame , Initiate by the StateMachine
    {
        //isInFov = inFOV(runner.transform, _mTarget, FacingMaxAngle, maxDistanceToAttack);
        if (_mTarget != null)
        {
            if (_navMeshAgent.enabled == true)
            {
                Vector3 destination = Vector3.zero;
                Vector3 dirToTarget = (_mTarget.position - runner.transform.position).normalized;                                                                            //* Danish Suggested this Solution

                // Generate a new Quaternion representing the rotation we should have
                Quaternion newRot = Quaternion.LookRotation(dirToTarget);                                                                                                    //* Danish Suggested this Solution

                // Smoothly rotate to that new rotation over time
                runner.transform.rotation = Quaternion.Slerp(runner.transform.rotation, newRot, Time.deltaTime * _slerpSpeed);                                               //* Danish Suggested this Solution
                destination = runner.transform.position + dirToTarget;
 
                // Validate if the distance between the player and the enemy
                // if the distance between enemy and player is less than attack distance
                if (Vector3.Distance(runner.transform.position, _mTarget.position) <= _maxDistanceToAttack)                                                                  //maintain in  <Attack State>
                {
                    _navMeshAgent.isStopped = false;                                                                                                                        // To tell the agent can move now
                    _navMeshAgent.speed = walk_Speed;
                    _navMeshAgent.SetDestination(destination);                                                                       
                    isCharging = _mTarget.GetComponentInChildren<LightCharging>().isCharging;
                        if (isCharging)
                        { runner.StartCoroutine(coroutineTrigger(isCharging,runner)); }
                }
                else if ((Vector3.Distance(runner.transform.position, _mTarget.position) >= _minDistanceToAttack))
                {
                    //  _navMeshAgent.isStopped = true;                                                                                                                     //Stop the agent when the player already outside the perimeter
                    _playerRunAway = true;
                }

            } 
        }
    }

    public override string CheckConditions(MonoBehaviour runner)                                                                        // Decisions has been made here -----------Called every frame after the First Frame ------------------
    {
        if (_mTarget == null) { return ""; }
        
        if (_playerRunAway) { _playerRunAway = false; return OnEnemyLostState; }                                                        // SetBack to the Previous allocated Location

        Collider[] overlapResults = new Collider[40];                                                                                   // we use physics and not a Trigger Components here for better GCA
        int numFound = Physics.OverlapSphereNonAlloc(runner.transform.position, _maxDistanceToAttack, overlapResults);

        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == _mTarget)
                {
                    if ((Vector3.Distance(runner.transform.position, _mTarget.position) >= _maxDistanceToAttack))
                    {                   
                        return OnEnemyAttackDistance;
                    }
                    else if (Vector3.Distance(runner.transform.position, _mTarget.position) <= _minDistanceToAttack)           // Switch to <Attack State>
                    {                       
                         return OnEnemyLostState;
                    }
                }
            }
        }

        overlapResults = new Collider[0];                                                                            // Helping the GCA   

        return "";                                                                                                   // Return empty String so that the StateMachine bypass validation check, and retained the current states
    }

    public override void OnExit(MonoBehaviour runner)
    {
        // TODO destroy Effects / Animation
    }

   
    IEnumerator coroutineTrigger(bool isCharging, MonoBehaviour runner)           
    {
            yield return runner.StartCoroutine(SpecialMovement1.Rotate_Coroutine(runner, _minDistanceToAttack, _maxDistanceToAttack, isCharging));

      yield return null;
    }


    //public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    //{
    //    Collider[] overlaps = new Collider[40];                                                                //assuming we are surrounded of 100 colliders e.g(mesh collider,sphere, cabsule, box, etc.)
    //    int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

    //    for (int i = 0; i < count + 1; i++)
    //    {
    //        if (overlaps[i] != null)
    //        {
    //            if (overlaps[i].transform == target)
    //            {
    //                Vector3 directionBetween = (target.position - checkingObject.position).normalized;
    //                directionBetween.y *= 0;

    //                float angle = Vector3.Angle(checkingObject.forward, directionBetween);

    //                if (angle <= maxAngle)
    //                {
    //                    Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
    //                    RaycastHit hit;

    //                    if (Physics.Raycast(ray, out hit, maxRadius))
    //                    {
    //                        if (hit.transform == target)
    //                        {
    //                            overlaps = new Collider[0];                                                                                                  // Helping the GCA 
    //                            return true;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    overlaps = new Collider[0];                                                                                                                         // Helping the GCA 

    //    return false;
    //}

    //private void OnDrawGizmos()
    //{
    //    if (ShowWireSphere_Sensor)
    //    {
    //        //Attack Radius WireSphere
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(_mRunner.transform.position, minDistanceToAttack);

    //        //Gizmos.color = Color.yellow;
    //        //Gizmos.DrawWireSphere(transform.position, chase_Distance);

    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(_mRunner.transform.position, maxDistanceToAttack);

    //        //Let us now Rotate the Vector in Horizontally by multiplying the Vector thru Quaternion
    //        Vector3 fovLine1 = Quaternion.AngleAxis(FacingMaxAngle, _mRunner.transform.up) * _mRunner.transform.forward * maxDistanceToAttack;
    //        Vector3 fovLine2 = Quaternion.AngleAxis(-FacingMaxAngle, _mRunner.transform.up) * _mRunner.transform.forward * maxDistanceToAttack;

    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawRay(_mRunner.transform.position, fovLine1);
    //        Gizmos.DrawRay(_mRunner.transform.position, fovLine2);
    //        //----
    //        Ray ray1 = new Ray(_mRunner.transform.position, fovLine1); RaycastHit hit1;                                          //Checking Left / Right if we hit something ?
    //        if (Physics.Raycast(ray1, out hit1, maxDistanceToAttack)) {
    //            Bool_fovLine1 = true;
    //        } else { Bool_fovLine1 = false; }

    //        Ray ray2 = new Ray(_mRunner.transform.position, fovLine2); RaycastHit hit2;
    //        if (Physics.Raycast(ray2, out hit2, maxDistanceToAttack)) {
    //            Bool_fovLine2 = true;
    //        } else { Bool_fovLine1 = false; }

    //        //----
    //        if (!isInFov)
    //        {
    //            Gizmos.color = Color.red;
    //            // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, target.transform.localEulerAngles.y, transform.localEulerAngles.z);
    //        }
    //        else
    //        {
    //            Gizmos.color = Color.green;
    //        }
    //       // transform.LookAt(target);
    //        Gizmos.DrawRay(_mRunner.transform.position, (_mTarget.position - _mRunner.transform.position).normalized * maxDistanceToAttack);
    //        Gizmos.color = Color.black;
    //        Gizmos.DrawRay(_mRunner.transform.position, _mRunner.transform.forward * maxDistanceToAttack);
    //    }

    //}
}



































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh