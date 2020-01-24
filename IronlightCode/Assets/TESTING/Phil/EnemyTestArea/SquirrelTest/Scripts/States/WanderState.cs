//Programmer : Phil James
//Description :  Version 2 (January 16, 2020)
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
   

    [Header("Sensor Color Yellow.")]
    public bool ShowWireSphere_Sensor;
    [Tooltip("Min - Yellow WireSphere.")]
    [Range(0f, 20f)]
    public float minDistanceToWander = 5.0f;
    [Tooltip("Max - Yellow WireSphere.")]
    [Range(0f, 30f)]
    public float maxDistanceToWander = 20.0f;

    private UnityEngine.AI.NavMeshAgent agent;                                  //reference to the navmesh agent.
   

    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    [Header("FOV Radar Limits")]
    [SerializeField]
    public float FacingMaxAngle = 45f;                                         //Facing Angle allertness at Z axis
    private bool isInFov = false;                                                //Field of View
                                                                                 //Local Variables
                                                                                 //private Renderer renderer;                                                  //Temporary Variable
    private float timer;
    public float wanderTimer = 20f;

    public override void OnEnter()                                              // This is called before the first frame Tick()
    {
        Name = this.GetType().ToString();
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag("Player").transform;

     

        isAware = false;

        wanderPoint = RandomWanderPoint();                                      //Destination to locate

        timer = wanderTimer;
    }

    public override void Tick()                                                 //Called every frame , Initiate by the StateMachine
    {
   
        if (wanderPoint != null)
        {

            // tell nav agent that he can move
            agent.isStopped = false;
            agent.speed = walk_Speed;                                                   //Agent Speed
            Vector3 dirToTarget = Vector3.zero;
            Vector3 destination = Vector3.zero;

            if (isAware)
            {
                isInFov = inFOV(transform, target, FacingMaxAngle, maxDistanceToWander);

                dirToTarget = (target.position + transform.position).normalized;

                // Turn the enemy facing to the Player
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(dirToTarget),
                                   1.0f * Time.deltaTime);

                destination = transform.position + dirToTarget;

                try
                {
                    agent.SetDestination(destination);
                }
                catch
                {
                }


            }
            else
            {
                timer += Time.deltaTime;

                if (timer >= wanderTimer)
                {

                    //Vector3 newPos = RandomNavSphere(transform.position, maxDistanceToWander, -1);

                // dirToTarget = (wanderPoint - transform.position).normalized;
                //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dirToTarget.x, 0, dirToTarget.z));    // flattens the vector3
                //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);

                    //destination = transform.position + dirToTarget;
                  
                isInFov = inFOV(transform, agent.transform, FacingMaxAngle, maxDistanceToWander);

               

                try
                {
                    agent.SetDestination(wanderPoint);
                }
                catch
                {
                }

                    timer = 0;
                }
            }
        }
    }

    public override string CheckConditions()                                        //Decisions has been made here
    {
                                                                                               

        if (target == null) { return ""; }

        if(SearchForPlayer())
        {
            return OnEnemyLostState;
        }
        else if (Vector3.Distance(transform.position, wanderPoint) < minDistanceToWander)            //  Has been reached ?
        {
            wanderPoint = RandomWanderPoint();                                                       //  Generate Destination

            return OnEnemyWanderDistance;                                                           //  Update the State
        }
        else
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
            agent.SetDestination(wanderPoint);
        }

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
            if (minDistanceToWander >= maxDistanceToWander)
            {
                Debug.LogWarning("Please assign less than value to the minDistanceToWander: " + gameObject.name);
            }
            //Minimum Distance WireSphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minDistanceToWander);

            //Maximum Distance WireSphere
            Gizmos.color = Color.yellow;
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
