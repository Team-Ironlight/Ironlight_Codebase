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


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New WanderState")]
public class WanderState : StateMachine.BaseState
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif
    [Header("Target")]
    private Transform _mTarget;
 
    [Header("Decision Making")]
    public string OnEnemyLostState = "FleeState";                                                               //To Do:  Convert this to enum
    private string OnEnemyWanderDistance = "WanderState";                                                       //To Do:  Convert this to enum

    private bool isAware = false;
  //  private Vector3 wanderPoint;

    [Header("Components")]
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;                                                          //reference to the navmesh agent.
    private Animator _aniMator;
    private AI_AbilityManager _executeAbility;

    [Header("Movement")]
    [SerializeField] float _slerpSpeed = 5.0f;
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

   // [Header("FOV Radar Limits")]
   // [SerializeField]
   // public float FacingMaxAngle = 45f;                                           //Facing Angle allertness at Z axis
   //// private bool isInFov = false;                                                //Field of View
                                                                                 //Local Variables
                                                                                 //private Renderer renderer;                                                  //Temporary Variable
    private float timer;
    public float wanderTimer = 20f;
    private bool _playerRunAway = false;
    private bool _isMoving = false;

    private float _maxDistanceToWander;
    private float _minDistanceToWander;

    private AI_AbilityManager _updateMinMax;                                                                    //Tell the Random Attacks about the MinMax value for Perimeter check

    public override void OnEnter(MonoBehaviour runner)                                                          // This is called before the first frame Tick()
    {
        _mTarget = GameObject.FindWithTag("Player").transform;
        _navMeshAgent = runner.GetComponent<NavMeshAgent>();
        _executeAbility = runner.GetComponent<AI_AbilityManager>();
        _updateMinMax = runner.GetComponent<AI_AbilityManager>();

        Name = this.GetType().ToString();                                                                       // Get the name of this Class

        _maxDistanceToWander = runner.GetComponent<StateMachine>().Get_MaxDistanceWander;
        _minDistanceToWander = runner.GetComponent<StateMachine>().Get_MinDistanceWander;

        _updateMinMax.Set_MaxDistance = _maxDistanceToWander;
        _updateMinMax.Set_MinDistance = _minDistanceToWander;

        isAware = false;
        timer = wanderTimer;
    }

    public override void Tick(MonoBehaviour runner)                                                             //Called every frame , Initiate by the StateMachine
    {
        if (_mTarget != null)
        {
            if (_navMeshAgent.enabled == true)
            {
                if (isAware)
                {
                    Vector3 dirToTarget = (_mTarget.position - runner.transform.position).normalized;

                 
                    // Generate a new Quaternion representing the rotation we should have
                    Quaternion newRot = Quaternion.LookRotation(dirToTarget);                                                                                   

                    // Smoothly rotate to that new rotation over time
                    runner.transform.rotation = Quaternion.Slerp(runner.transform.rotation, newRot, Time.deltaTime * _slerpSpeed);                              

                    Vector3 destination = runner.transform.position + dirToTarget;
                                      
                    // Validate if the distance between the player and the enemy
                    // if the distance between enemy and player is less than attack distance
                    if ((Vector3.Distance(runner.transform.position, _mTarget.position) <= _maxDistanceToWander))
                    {
                        _navMeshAgent.isStopped = false;
                        _navMeshAgent.speed = run_Speed;
                        _navMeshAgent.SetDestination(destination);
                    }
                    else if ((Vector3.Distance(runner.transform.position, _mTarget.position) >= _minDistanceToWander))
                    {
                        //if the Player run away from the wander perimeter
                        _playerRunAway = true;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                
                    if (timer >= wanderTimer)
                    {
                                      
                        if ((!_navMeshAgent.isPathStale) && (_navMeshAgent.remainingDistance == 0) && (!_navMeshAgent.pathPending) && (_navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete))
                        {
                            Vector3 newPos = RandomNavSphere(runner.transform.position, _maxDistanceToWander, -1);
                            
                            _navMeshAgent.SetDestination(newPos);
                            // Generate a new Quaternion representing the rotation we should have
                            Quaternion newRot = Quaternion.LookRotation(newPos);                                                                                                

                            // Smoothly rotate to that new rotation over time 
                            runner.transform.rotation = Quaternion.Slerp(runner.transform.rotation, newRot, Time.deltaTime * _slerpSpeed);                                      
                        }
                        timer = 0;
                    }

                    if (_navMeshAgent.velocity.sqrMagnitude > 0)
                    {
                        _isMoving = true;
                    }
                    else
                    {
                        _isMoving = false;
                    }

                }
            }
        }
    }
    IEnumerator coroutineTrigger()                                                                                          //if Not Moving , then do Animation
    {
        if (!_isMoving)
        {
             _aniMator.SetTrigger("Jump");
            yield return new WaitForSeconds(_aniMator.GetCurrentAnimatorStateInfo(0).length + _aniMator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return new WaitForSeconds(1f);
        }
        yield break;                                                                                                        //turn off
    }
    public override string CheckConditions(MonoBehaviour runner)                                                            //Decisions has been made here
    {                                                                                           

        if (_mTarget == null) { return ""; }

        if(_playerRunAway)
        {
      
            _playerRunAway = false;
            return OnEnemyWanderDistance;
        }

        Collider[] overlapResults = new Collider[50];
        int numFound = Physics.OverlapSphereNonAlloc(runner.transform.position, _maxDistanceToWander, overlapResults);

        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == _mTarget)
                {
                    // Debug.DrawLine(runner.transform.position, overlapResults[i].transform.position, Color.yellow);
                    if ((Vector3.Distance(runner.transform.position, _mTarget.position) < _maxDistanceToWander))
                    {
                        if (Vector3.Distance(runner.transform.position, _mTarget.position) > _minDistanceToWander)               // Current State <Patrol State>
                        {
                            OnAware();
                            return "";
                        }
                        else
                        {
                            //Going to the minimum Distance , switch <Attack State>
                            return OnEnemyLostState;
                        }

                    }

                }

            }

        }

        overlapResults = new Collider[0];

        return "";
    }

 
    public override void OnExit(MonoBehaviour runner)
    {
        //To Do:  Destroy effects / animation
    }

    public void OnAware() { isAware = true; }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
 

    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[50];
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


}













































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh