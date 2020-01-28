//Programmer : Phil James
//Description :  Version 3 (January 23, 2020)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[CreateAssetMenu(menuName = "AI System/New AI Ability")]
public class AI_Abilities : AI_CoroutineManager
{
       
    private Transform target;

    private float myCollisionRadius;                         //This Collision
    private float targetCollisionRadius;                     //Target Collision

    public float attackSpeed = 3f;

    public float wait_Before_Attack = 2f;                    //Cooling Attack
    private float attack_Timer;                              //Cooling Attack
    
    [SerializeField]
    [Range(0f, 20f)]
    public float attack_Distance = 9.8f;
    private bool completed_OneCycle = false;                 //Orbitting Flag
    public float rotationSpeed = 20f;                        
    public float radiusSpeed = 0.5f;

    public AnimationCurve JumpCurve = new AnimationCurve();
    private NavMeshAgent _playerAgent;
    private Rigidbody _arcRigidBody;
    [HideInInspector] [Range(20f, 70f)] public float _angle;      // shooting angle

   [HideInInspector] public float h = 25;
    [HideInInspector] public float gravity = -18;

    [HideInInspector] public bool debugPath;

    [HideInInspector] public Vector3 point1;
    [HideInInspector] public Transform point2;
    [HideInInspector] private RaycastHit hit;
    [HideInInspector] public LayerMask damageLayer;

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public override IEnumerator Jump(MonoBehaviour runner, float duration, float minDistanceToAttack, float maxDistanceToAttack)
    {
        // Get the current OffMeshLink data
        target = GameObject.FindWithTag("Player").transform;
        _playerAgent = runner.GetComponent<NavMeshAgent>();
        _arcRigidBody = runner.GetComponent<Rigidbody>();


        Vector3 dirToTarget = (target.position - runner.transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (minDistanceToAttack);
  

        // Start Position is agent current position
        Vector3 startPos = target.position - dirToTarget * (maxDistanceToAttack);


        // End position is fetched from OffMeshLink data and adjusted for baseoffset of agent
        Vector3 endPos = target.transform.position + (0.2f * Vector3.up);

        // Used to keep track of time
        float time = 0.0f; float t = 0.0f;

        // Keeo iterating for the passed duration
        while (time <= duration)
        {
            // Calculate normalized time
             t = time / duration;

            // Lerp between start position and end position and adjust height based on evaluation of t on Jump Curve
            runner.transform.position = Vector3.Lerp(startPos, endPos, t) + (JumpCurve.Evaluate(t) * Vector3.up).normalized;

            // Accumulate time and yield each frame
            time += Time.deltaTime;

            yield return null;

        }
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\     




        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        // NOTE : Added this for a bit of stability to make sure the
        //        Agent is EXACTLY on the end position of the off mesh
        //		  link before completeing the link.
        runner.transform.position = endPos;

        // All done so inform the agent it can resume control
        // navAgent.CompleteOffMeshLink();

        //time = 0.0f;

        //// Keeo iterating for the passed duration
        //while (time <= duration)
        //{
        //    // Calculate normalized time
        //    t = time / duration;

        //    // Lerp between start position and end position and adjust height based on evaluation of t on Jump Curve
        //    _playerAgent.transform.position = Vector3.Lerp(endPos, startPos, t) + (JumpCurve.Evaluate(t) * Vector3.up).normalized;

        //    // Accumulate time and yield each frame
        //    time += Time.deltaTime;

        //    yield return null;
        //}

        // NOTE : Added this for a bit of stability to make sure the
        //        Agent is EXACTLY on the end position of the off mesh
        //		  link before completeing the link.
        //    _playerAgent.transform.position = startPos;
        // runner.transform.position = Vector3.MoveTowards(runner.transform.position, startPos, Time.deltaTime * radiusSpeed);
     //   yield return null;
        yield return new WaitForSeconds(2f);
    }


    //On Going Development / Not Tested
    public override IEnumerator Dash_Coroutine(MonoBehaviour runner, Vector3 source, int x, Vector3 fovline1, Vector3 fovline2, float minDistanceToAttack, float maxDistanceToAttack)
    {
        Debug.Log("Coroutine Jump Created.");

        Vector3 velocity = Vector3.zero;
        Vector3 firstDestination = Vector3.zero;
        Vector3 secondDestination = Vector3.zero;
        int Dot1 = 0; int Dot2 = 0; int Dot3 = 0;
        Vector3 originalPosition = source;

        target = GameObject.FindWithTag("Player").transform;
        if(x == 1){
            firstDestination = fovline1;

            Dot1 = (int)Mathf.Sign(Vector3.Dot(source, fovline1));
            Dot2 = Dot1;

            velocity = (fovline1 - source).normalized * attackSpeed;

            while (Dot1 == Dot2)
            {
                runner.transform.position += velocity * Time.deltaTime;
                Dot2 = (int)Mathf.Sign(Vector3.Dot(runner.transform.position, fovline1));
                yield return null;
            }

            runner.transform.position = Vector3.MoveTowards(runner.transform.position, fovline1, Time.deltaTime * radiusSpeed);
        }
        else if(x == 2){
            secondDestination = fovline2;
                       
            Dot1 = (int)Mathf.Sign(Vector3.Dot(source, fovline2));
            Dot2 = Dot1;

            velocity = (fovline2 - source).normalized * attackSpeed;

            while (Dot1 == Dot2)
            {
                runner.transform.position += velocity * Time.deltaTime;
                Dot2 = (int)Mathf.Sign(Vector3.Dot(runner.transform.position, fovline2));
                yield return null;
            }

            runner.transform.position = Vector3.MoveTowards(runner.transform.position, fovline1, Time.deltaTime * radiusSpeed);
        }
        else if(x == 3){
            firstDestination = fovline1;
            secondDestination = fovline2;
        }



         yield return new WaitForEndOfFrame();
        Debug.Log("Ability Swag enables coroutineTrigger to run.");

    }

    // TO DO:  Trajectory Jump
    //Development is In-Progress
    public override IEnumerator Jump_Coroutine(MonoBehaviour runner, float minDistanceToAttack, float maxDistanceToAttack)
    {
        //  Debug.Log("Coroutine Jump Created.");

        //target = GameObject.FindWithTag("Player").transform;
        //_playerAgent = runner.GetComponent<NavMeshAgent>();
        //_arcRigidBody = runner.GetComponent<Rigidbody>();
        //_arcRigidBody.isKinematic = false;
        //_playerAgent.enabled = false;




        //if (Physics.Raycast(runner.transform.position, runner.transform.forward, out hit, 100f, damageLayer))
        //{
        //point1 = hit.point;
        //point2 = hit.transform;
        //Launch();
        //}


        //if (debugPath)
        //{
        //    DrawPath();
        //}

        //Vector3 moveDirection = new Vector3(0, 0, target.position.z);

        //Physics.gravity = Vector3.up * gravity;
        //_arcRigidBody.useGravity = true;

        //_arcRigidBody.velocity = CalculateLaunchData().initialVelocity;

        //_arcRigidBody.AddForce(runner.transform.up * 11 + moveDirection, ForceMode.Force);




        yield return new WaitForSeconds(1f);
        //_arcRigidBody.isKinematic = false;
        //    runner.GetComponent<AttackState>().isOnAnimation = false;
        //   Debug.Log("Ability Swag enables coroutineTrigger to run.");

    }

    // TO DO:  Testing / Debugging
    private void Launch2()
    {

        // // source and target positions
        // Vector3 pos = runner.transform.position;
        //// target = target.position;

        // // distance between target and source
        // float dist = Vector3.Distance(pos, target.transform.position);

        // // rotate the object to face the target
        // runner.transform.LookAt(target);

        // // calculate initival velocity required to land the cube on target using the formula (9)
        // float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
        // float Vy, Vz;   // y,z components of the initial velocity

        // Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        // Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        //// Debug.Log(Vy);
        // // create the velocity vector in local space
        // Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        // // transform it to global vector
        // Vector3 globalVelocity = runner.transform.TransformVector(localVelocity);

        // // launch the cube by setting its initial velocity
        // _arcRigidBody.velocity = globalVelocity;
    }
    private void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        _arcRigidBody.isKinematic =false;

        _arcRigidBody.velocity = CalculateLaunchData().initialVelocity;

      _arcRigidBody.AddForce(Vector3.up * 11, ForceMode.Impulse);

      
    }
    LaunchData CalculateLaunchData()
    {
        float displacementY = point2.position.y - _arcRigidBody.position.y;
        Vector3 displacementXZ = new Vector3(point2.position.x - _arcRigidBody.position.x, 0, point2.position.z - _arcRigidBody.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = _arcRigidBody.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = _arcRigidBody.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }
    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }


    //Tested
    public override IEnumerator Swag_Coroutine(MonoBehaviour runner, float minDistanceToAttack, float maxDistanceToAttack)
    {
      
     //   Debug.Log("Coroutine Swag Created.");
     
        target = GameObject.FindWithTag("Player").transform;                                                         //Initialized

        // runner.transform.LookAt(target);                                                                         //we need to ensure our AI is facing to our Target
        myCollisionRadius = runner.transform.GetComponent<CapsuleCollider>().radius;



        //Get the position value, use this to show the Player was stepping backward 
        Vector3 originalPosition = runner.transform.position;
        //Get the difference
        Vector3 dirToTarget = (target.position - runner.transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (minDistanceToAttack);
        Vector3 newPosition = target.position - dirToTarget * (maxDistanceToAttack);

        runner.transform.position = attackPosition;
             
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 5;
            runner.transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            runner.transform.LookAt(target);

            yield return null;
         
        }
                 
        runner.transform.position = Vector3.MoveTowards(runner.transform.position, newPosition, Time.deltaTime * radiusSpeed);

        yield break;             

        //Debug.Log("Ability Swag enables coroutineTrigger to run.");

    }

    //TO DO :  Testing / Debugging
    public override IEnumerator Rotate_Coroutine(MonoBehaviour runner, float minDistanceToAttack, float maxDistanceToAttack, bool isCharging)
    {
     //   Debug.Log("Coroutine Orbit Created.");

            target = GameObject.FindWithTag("Player").transform;                                //Initialized
        
           
            runner.transform.LookAt(target);                                                     //we need to ensure our AI is facing to our Target


            myCollisionRadius = runner.transform.GetComponent<CapsuleCollider>().radius;
           // targetCollisionRadius = target.transform.GetComponent<CapsuleCollider>().radius;
               

                //Since we dont have Obstacle Avoidance Agent like NavAgent, then we have to suffer making our Own
                //we need to Check for any Obstacle in front.
                 RaycastHit hit; int range = 5;

                // Two rays left and right to the object to detect the obstacle.
                Transform leftRay = runner.transform; Transform rightRay = runner.transform;

                // DrawLine for debugging.
                Debug.DrawRay(leftRay.position + (runner.transform.right * 2), runner.transform.forward * 2, Color.green);
                Debug.DrawRay(rightRay.position - (runner.transform.right * 2), runner.transform.forward * 2, Color.green);
                //Use Phyics.RayCast to detect the obstacle
                if (Physics.Raycast(leftRay.position + (runner.transform.right * 2), runner.transform.forward * 2, out hit, range))
                {
                    // this gonna be reduntant switching this boolean flag, but it works and so that it will easily to understand the logic behind here
                    completed_OneCycle = false;

                }
                else if (Physics.Raycast(rightRay.position - (runner.transform.right * 2), runner.transform.forward * 2, out hit, range))
                {

                    completed_OneCycle = true;

                }
                // Use to debug the Physics.RayCast.
                Debug.DrawRay(runner.transform.position - (runner.transform.forward * 2), runner.transform.right * 2, Color.green);
                Debug.DrawRay(runner.transform.position - (runner.transform.forward * 2), -runner.transform.right * 2, Color.green);
            
                // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
                // Just making this boolean variable false it means there is nothing in front of object.
                if (Physics.Raycast(runner.transform.position - (runner.transform.forward * 2), runner.transform.right * 2, out hit, range))
                {

                    completed_OneCycle = false;

                }
                else if (Physics.Raycast(runner.transform.position - (runner.transform.forward * 2), -runner.transform.right * 2, out hit, range))
                {

                    completed_OneCycle = true;

                }

                //Get the Latest Magnitude Distance
                float sqrDstToTarget = (target.position - runner.transform.position).sqrMagnitude;
     

            //attack_Distance //Mathf.Pow(maxDistanceToAttack + myCollisionRadius + targetCollisionRadius, 2))
            if (sqrDstToTarget < Mathf.Pow(maxDistanceToAttack + myCollisionRadius + myCollisionRadius, 2))
            {
         
            //We need to Get the precise/latest Transform Position
            //  Transform playerPosition = GameObject.FindWithTag("Player").transform;

            Vector3 dirToTarget = (target.position - runner.transform.position).normalized;
                Vector3 attackPosition = target.position - dirToTarget * (maxDistanceToAttack);

                //This part is important , we need to makesure our AI facing the Player and is in the correct distance
                Vector3 axis = Vector3.up;
                    runner.transform.LookAt(target);
                    runner.transform.position = attackPosition;
              

                    //Forcing to reset the Rotation
                    if (completed_OneCycle)
                    { runner.transform.RotateAround(target.position, axis, -rotationSpeed * Time.deltaTime); }
                    else
                    { runner.transform.RotateAround(target.position, axis, rotationSpeed * Time.deltaTime); }

                
                  runner.transform.position = Vector3.MoveTowards(runner.transform.position, attackPosition, Time.deltaTime * radiusSpeed);
                  runner.transform.LookAt(target);


             
            }

         yield return null;
      
    //   Debug.Log("Ability Orbit enables coroutineTrigger to run.");
    }

}



























































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh