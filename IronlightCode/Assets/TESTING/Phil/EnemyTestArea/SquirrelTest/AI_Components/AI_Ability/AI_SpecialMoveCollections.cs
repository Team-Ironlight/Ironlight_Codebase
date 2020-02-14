//Programmer : Phil James
//Description :  Version 3 (January 23, 2020)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/Special Movement/New AI Special Movement")]
public class AI_SpecialMoveCollections : AI_SpecialMoveCoroutine
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