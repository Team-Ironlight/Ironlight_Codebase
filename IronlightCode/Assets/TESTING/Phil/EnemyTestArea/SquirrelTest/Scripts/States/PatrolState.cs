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

[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New PatrolState")]
public class PatrolState : StateMachine.BaseState
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif
    [Header("Target")]
    private Transform target;

    [Header("Decision Making")]
    public string OnEnemyLostState = "PatrolState";                             //To Do:  Convert this to enum
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

    [Header("Components")]
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;                          //reference to the navmesh agent.
    private NavMeshPath _path;                                                  //meant for Calculate Path
    private float _elapsedPath = 0.0f;                                          //Timer for updating the Calculate path
    private Animator _aniMator;

    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    [Header("FOV Radar Limits")]
    [SerializeField]
    public float FacingMaxAngle = 45f;                                           //Facing Angle allertness at Z axis
    private bool isInFov = false;                                                //Field of View
    private MonoBehaviour _mRunner;                                              // Local use


    public override void OnEnter(MonoBehaviour runner)                                              // This is called before the first frame Tick()
    {
        _mRunner = runner;
        _navMeshAgent = runner.GetComponent<NavMeshAgent>();                                        // Initialized
        _aniMator = runner.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;

        if (!target)
            Application.Quit();

        Name = this.GetType().ToString();                                                           // Naming the Class
        _path = new NavMeshPath();                                                                  // Initialized these variables meant for Calculating the Path
        _elapsedPath = 0.0f;                                    
    }

    public override void Tick(MonoBehaviour runner)                                                 //Called every frame , Initiate by the StateMachine
    {
        Vector3 dirToTarget = Vector3.zero;
        Vector3 destination = Vector3.zero;

        if (target != null)
        {
            if (_navMeshAgent.enabled != true) { return; }

            if (isAware)
            {
                
                isInFov = inFOV(runner.transform, target, FacingMaxAngle, maxDistanceToPatrol);                                         //Check the Field of View
                dirToTarget = (target.position - runner.transform.position).normalized;

                // Turn the enemy facing to the Player
                runner.transform.rotation = Quaternion.Slerp(runner.transform.rotation,
                                    Quaternion.LookRotation(dirToTarget),
                                    1.0f * Time.deltaTime);

                destination = runner.transform.position + dirToTarget;
                if (_navMeshAgent.enabled == true)
                {
                    // Validate if the distance between the player and the enemy
                    // if the distance between enemy and player is less than attack distance
                    if (Vector3.Distance(runner.transform.position, target.position) <= maxDistanceToPatrol)                            // Switch to <Attack State>
                    {
                        // tell nav agent that he can move
                        _navMeshAgent.isStopped = false;
                        _navMeshAgent.speed = walk_Speed;

                        _navMeshAgent.SetDestination(destination);
                    }
                    else if (Vector3.Distance(runner.transform.position, target.position) > maxDistanceToPatrol)
                    {
                        _navMeshAgent.isStopped = true;
                        isAware = false;
                    }
                }
            }
            else
            {
               
                patrolPoint = Patrol();
                dirToTarget = (patrolPoint - runner.transform.position).normalized;

                // Turn the enemy facing to the Player
                runner.transform.rotation = Quaternion.Slerp(runner.transform.rotation,
                                    Quaternion.LookRotation(dirToTarget),
                                   walk_Speed * Time.deltaTime);

                destination = runner.transform.position + dirToTarget;
                isInFov = inFOV(runner.transform, _navMeshAgent.transform, FacingMaxAngle, maxDistanceToPatrol);                           //Check the Field of View

                    try
                    {
                        if (_navMeshAgent.enabled == true)
                        {
                            _navMeshAgent.isStopped = false;
                            _navMeshAgent.speed = walk_Speed;
                            _navMeshAgent.SetDestination(destination);
                        }
                    }
                    catch
                    {
                    }

            }
        }

        
    }


    public override string CheckConditions(MonoBehaviour runner)                                        //Decisions has been made here
    {

        if (target == null) { return ""; }
     
        Collider[] overlapResults = new Collider[20];
        int numFound = Physics.OverlapSphereNonAlloc(runner.transform.position, maxDistanceToPatrol, overlapResults);
       
        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == target)                                                          // Check if we can
                {
                    Debug.DrawLine(runner.transform.position, overlapResults[i].transform.position, Color.red);
                    if (Vector3.Distance(runner.transform.position, target.position) >= maxDistanceToPatrol)
                    {
                        if (Vector3.Distance(runner.transform.position, target.position) >= minDistanceToPatrol)           // Current State <Patrol State>
                        {
                            OnAware();                                      
                            return OnEnemyPatrolDistance;
                        }
                    }
                    else if (Vector3.Distance(runner.transform.position, target.position) <= maxDistanceToPatrol)
                    {
                        if (Vector3.Distance(runner.transform.position, target.position) <= minDistanceToPatrol)           // Switch to <Attack State>
                        {
                            return OnEnemyLostState;                                                        
                        }
                    }

                }
            }
        }

        return "";
    }


    public override void OnExit(MonoBehaviour runner)
    {
        //To Do:  Destroy effects / animation
    }

    public void OnAware()
    {
        isAware = true;
    }
    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * maxDistanceToPatrol) + _mRunner.transform.position;
        
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, maxDistanceToPatrol, UnityEngine.AI.NavMesh.AllAreas);  //-1
                
        return new Vector3(navHit.position.x, _mRunner.transform.position.y, navHit.position.z);
    }

    private bool SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, _mRunner.transform.InverseTransformPoint(target.transform.position)) < FacingMaxAngle / 2f)
        {
            if (Vector3.Distance(target.transform.position, _mRunner.transform.position) < maxDistanceToPatrol)
            {
                RaycastHit hit;
                if (Physics.Linecast(_mRunner.transform.position, target.transform.position, out hit, -1))
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
            if (Vector3.Distance(waypoints[waypointIndex].position, _mRunner.transform.position) < 2f)
            {
                if (waypointIndex == waypoints.Length - 1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex++;
                }
                _navMeshAgent.isStopped = false;
                _mRunner.StartCoroutine(coroutineAnimator());
            }
        }
        else
        {
            Debug.LogWarning("Please assign more than 1 waypoint to the AI: " + _mRunner.gameObject.name);
        }

        
        return new Vector3(waypoints[waypointIndex].position.x, waypoints[waypointIndex].position.y, waypoints[waypointIndex].position.z);
    }
    IEnumerator coroutineAnimator()
    {
        _aniMator.enabled = true;
        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;
       // _aniMator.SetTrigger("introOne");

        Name = "PATROL_ANIMATE";
        yield return new WaitForSeconds(1f);

        _aniMator.SetTrigger("introOne");
        //Tweak the name so that it allows you maintained Active NavAgent.
        yield return new WaitForSeconds(_aniMator.GetCurrentAnimatorStateInfo(0).length + _aniMator.GetCurrentAnimatorStateInfo(0).normalizedTime);


        Name = this.GetType().ToString();     //Get back the original Class name
        //_navMeshAgent.enabled = true;
        //_aniMator.enabled = false;


        yield return null;                              //return next frame


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

    private void OnDrawGizmos()                                                                                                 // Our WireSphere Guide Draw here
    {
        if (ShowWireSphere_Sensor)
        {
            if (minDistanceToPatrol >= maxDistanceToPatrol)
            {
                Debug.LogWarning("Please assign less than value to the minDistanceToWander: " + _mRunner.gameObject.name);
            }
            //Minimum Distance WireSphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_mRunner.transform.position, minDistanceToPatrol);

            //Maximum Distance WireSphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_mRunner.transform.position, maxDistanceToPatrol);

            //Let us now Rotate the Vector in Horizontally by multiplying the Vector thru Quaternion
            Vector3 fovLine1 = Quaternion.AngleAxis(FacingMaxAngle, _mRunner.transform.up) * _mRunner.transform.forward * maxDistanceToPatrol;
            Vector3 fovLine2 = Quaternion.AngleAxis(-FacingMaxAngle, _mRunner.transform.up) * _mRunner.transform.forward * maxDistanceToPatrol;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_mRunner.transform.position, fovLine1);
            Gizmos.DrawRay(_mRunner.transform.position, fovLine2);

            if (!isInFov)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.DrawRay(_mRunner.transform.position, (patrolPoint - _mRunner.transform.position).normalized * maxDistanceToPatrol);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(_mRunner.transform.position, _mRunner.transform.forward * maxDistanceToPatrol);
        }


    }
}































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh