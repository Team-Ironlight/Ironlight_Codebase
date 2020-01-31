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
public class WanderState : StateMachine.BaseState
{
    [Header("Target")]
    private Transform target;
 
    [Header("Decision Making")]
    public string OnEnemyLostState = "FleeState";                               //To Do:  Convert this to enum
    private string OnEnemyWanderDistance = "WanderState";                       //To Do:  Convert this to enum

    private bool isAware = false;
    private Vector3 wanderPoint;
   

    [Header("Sensor Color Green.")]
    public bool ShowWireSphere_Sensor;
    [Tooltip("Min - Green WireSphere.")]
    [Range(0f, 20f)]
    public float minDistanceToWander = 5.0f;
    [Tooltip("Max - Green WireSphere.")]
    [Range(0f, 30f)]
    public float maxDistanceToWander = 20.0f;

    [Header("Components")]
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;                                        //reference to the navmesh agent.
    private Animator _aniMator;
    private AI_AbilityManager _executeAbility;

    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    [Header("FOV Radar Limits")]
    [SerializeField]
    public float FacingMaxAngle = 45f;                                           //Facing Angle allertness at Z axis
    private bool isInFov = false;                                                //Field of View
                                                                                 //Local Variables
                                                                                 //private Renderer renderer;                                                  //Temporary Variable
    private float timer;
    public float wanderTimer = 20f;
    private bool _playerRunAway = false;
    private bool _isMoving = false;
 
    public override void OnEnter()                                              // This is called before the first frame Tick()
    {
        Name = this.GetType().ToString();                                       // Get the name of this Class
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _executeAbility = GetComponent<AI_AbilityManager>();
        target = GameObject.FindWithTag("Player").transform;

     

        isAware = false;

        wanderPoint = RandomWanderPoint();                                      //Destination to locate

        timer = wanderTimer;
    }

    public override void Tick()                                                 //Called every frame , Initiate by the StateMachine
    {
       

        if (target != null)
        {
           
            if (_navMeshAgent.enabled == true)
            {
                if (isAware)
                {

                  
                    //isInFov = inFOV(transform, target, FacingMaxAngle, maxDistanceToWander);

                    Vector3 dirToTarget = (target.position - transform.position).normalized;

                    // Turn the enemy facing to the Player
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                                        Quaternion.LookRotation(dirToTarget),
                                        1.0f * Time.deltaTime);

                    Vector3 destination = transform.position + dirToTarget;

                  //  _executeAbility.enabled = false;
                    // Validate if the distance between the player and the enemy
                    // if the distance between enemy and player is less than attack distance
                    if ((Vector3.Distance(transform.position, target.position) <= maxDistanceToWander))
                    {
                      
                        _navMeshAgent.isStopped = false;
                        _navMeshAgent.speed = run_Speed;
                        _navMeshAgent.SetDestination(destination);

                    }
                    else if ((Vector3.Distance(transform.position, target.position) >= minDistanceToWander))
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
                 
                        isInFov = inFOV(transform, _navMeshAgent.transform, FacingMaxAngle, maxDistanceToWander);

                    
                        if ((!_navMeshAgent.isPathStale) && (_navMeshAgent.remainingDistance == 0) && (!_navMeshAgent.pathPending) && (_navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete))
                        {
                            Vector3 newPos = RandomNavSphere(transform.position, maxDistanceToWander, -1);
                           
                                    _navMeshAgent.SetDestination(newPos);
                        }
                        timer = 0;
                    }

                    if (_navMeshAgent.velocity.sqrMagnitude > 0)
                    {
                        _isMoving = true;
                    //    Name = "WanderState";
                    }
                    else
                    {
                   //     Name = "WANDER_ANIMATE";
                   //     GetComponent<Animator>().enabled = true;
                        _isMoving = false;
                      //  StartCoroutine(coroutineTrigger());

                                           

                    }

                }
            }
        }
    }
    IEnumerator coroutineTrigger()                                                                  //if Not Moving , then do Animation
    {
        if (!_isMoving)
        {
                   
            _aniMator.SetTrigger("Jump");
            yield return new WaitForSeconds(_aniMator.GetCurrentAnimatorStateInfo(0).length + _aniMator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return new WaitForSeconds(1f);
        }
        yield break;                                    //turn off
    }
    public override string CheckConditions()                                                        //Decisions has been made here
    {                                                                                           

        if (target == null) { return ""; }

        if(_playerRunAway)
        {
            _playerRunAway = false;
            return OnEnemyWanderDistance;
        }

        Collider[] overlapResults = new Collider[50];
        int numFound = Physics.OverlapSphereNonAlloc(transform.position, maxDistanceToWander, overlapResults);

        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == target)
                {

         

                    Debug.DrawLine(transform.position, overlapResults[i].transform.position, Color.yellow);
                    if ((Vector3.Distance(transform.position, target.position) < maxDistanceToWander))
                    {

                        if (Vector3.Distance(transform.position, target.position) > minDistanceToWander)           // Current State <Patrol State>
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
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
      

        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    public Vector3 RandomWanderPoint()
    {
        //if (Vector3.Distance(transform.position, wanderPoint) < minDistanceToWander)            //  Has been reached ?
        //{
        Vector3 randomPoint = (Random.insideUnitSphere * maxDistanceToWander) + transform.position;


        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, maxDistanceToWander, UnityEngine.AI.NavMesh.AllAreas);  //-1
        



        return navHit.position;
    }
   

    private bool SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(target.transform.position)) < FacingMaxAngle / 2f)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < maxDistanceToWander)
            {
                RaycastHit hit;
                if(Physics.Linecast(transform.position, target.transform.position, out hit, -1))
                if (hit.transform.CompareTag("Player"))
                {        
                        OnAware();
                        return true;
                       
                }
            }
        }
        return false;
    }

    public void Wander()                                                                             //To Do:  next version will remove this, not needed
    {

        if (Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            _navMeshAgent.SetDestination(wanderPoint);
        }

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

    private void OnDrawGizmos()
    {
        if (ShowWireSphere_Sensor)
        {
            if (minDistanceToWander >= maxDistanceToWander)
            {
                Debug.LogWarning("Please assign less than value to the minDistanceToWander: " + gameObject.name);
            }
            //Minimum Distance WireSphere
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minDistanceToWander);

            //Maximum Distance WireSphere
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, maxDistanceToWander);

            //Let us now Rotate the Vector in Horizontally by multiplying the Vector thru Quaternion
            Vector3 fovLine1 = Quaternion.AngleAxis(FacingMaxAngle, transform.up) * transform.forward * maxDistanceToWander;
            Vector3 fovLine2 = Quaternion.AngleAxis(-FacingMaxAngle, transform.up) * transform.forward * maxDistanceToWander;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);

            if (!isInFov)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (wanderPoint - transform.position).normalized * maxDistanceToWander);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistanceToWander);
        }


    }
}













































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh