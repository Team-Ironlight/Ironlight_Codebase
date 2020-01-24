//Programmer : Phil James
//Description :  Version 2 (January 16, 2020)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI Squirrel/Squirrel Attack 1")]
public class AI_Attack1_Coroutine : AI_AbilitySequence
{

    public float DestroyAfterTime = 1f;
    private Transform target;

    [Header("Rotating Behaviour")]
    [SerializeField]
    [Range(0f, 20f)]
    public float attack_Distance = 9.8f;
    private bool completed_OneCycle = false;                //Orbitting Flag
    public float rotationSpeed = 20f;                       //Request By Brian - Enemy Rotating Behaviour
    public float radiusSpeed = 0.5f;
    public float attackSpeed = 3f;

    private float myCollisionRadius;                        //This Collision
    private float targetCollisionRadius;                    //Target Collision

    public override IEnumerator Attack1_Coroutine(MonoBehaviour runner)
    {
        //   Instantiate(Effect, runner.transform.position, runner.transform.rotation);

        target = GameObject.FindWithTag("Player").transform;

        myCollisionRadius = runner.transform.GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = runner.transform.GetComponent<CapsuleCollider>().radius;

        runner.transform.LookAt(target);
        //Since we dont have Obstacle Avoidance Agent like NavAgent, then we have to suffer making our Own
        //we need to Check for any Obstacle in front.
        RaycastHit hit; int range = 5;

        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = runner.transform; Transform rightRay = runner.transform;

        // DrawLine for debugging.
        Debug.DrawRay(leftRay.position + (runner.transform.right * 2), runner.transform.forward * 2, Color.yellow);
        Debug.DrawRay(rightRay.position - (runner.transform.right * 2), runner.transform.forward * 2, Color.yellow);
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
        Debug.DrawRay(runner.transform.position - (runner.transform.forward * 2), runner.transform.right * 2, Color.yellow);
        Debug.DrawRay(runner.transform.position - (runner.transform.forward * 2), -runner.transform.right * 2, Color.yellow);

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
        if (sqrDstToTarget < Mathf.Pow(attack_Distance + myCollisionRadius + targetCollisionRadius, 2))
        {

            //We need to Get the precise/latest Transform Position
            Transform playerPosition = GameObject.FindWithTag("Player").transform;

            Vector3 dirToTarget = (playerPosition.position - runner.transform.position).normalized;
            Vector3 attackPosition = playerPosition.position - dirToTarget * (attack_Distance);


            //This part is important , we need to makesure our AI facing the Player and is in the correct distance
            Vector3 axis = Vector3.up;
            runner.transform.LookAt(playerPosition);
            runner.transform.position = attackPosition;

            //Forcing to reset the Rotation
            if (completed_OneCycle)
            { runner.transform.RotateAround(playerPosition.position, axis, -rotationSpeed * Time.deltaTime); }
            else
            { runner.transform.RotateAround(playerPosition.position, axis, rotationSpeed * Time.deltaTime); }

            runner.transform.position = Vector3.MoveTowards(runner.transform.position, attackPosition, Time.deltaTime * radiusSpeed);
            runner.transform.LookAt(target);


        }
        //  coroutineAllowed = true;

        //---------------------------
        yield return new WaitForSeconds(DestroyAfterTime);

        // Destroy(runner.gameObject);
    }


}

