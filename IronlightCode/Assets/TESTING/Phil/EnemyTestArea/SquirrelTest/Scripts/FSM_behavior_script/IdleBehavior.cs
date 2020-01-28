// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/23/2020
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour {


    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;
    private Rigidbody _arcRigidBody;
    [Range(20f, 70f)] public float _angle;      // shooting angle

    public float jumpSpeed = 5f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timer = Random.Range(minTime, maxTime);
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _arcRigidBody = animator.GetComponent<Rigidbody>();
        
        
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

      

        if (timer <= 0)
        {
            animator.SetTrigger("jump");
        }
        else {
            timer -= Time.deltaTime;

        }

        //animator.applyRootMotion = false;
        //_arcRigidBody.isKinematic = false;
        //_arcRigidBody.AddForce(animator.transform.position * jumpSpeed, ForceMode.Force);
        //_arcRigidBody.isKinematic = true;
        //animator.applyRootMotion = true;

        //Note : the Algorithm below has been tested successfully in MonoBehaviour Class.

        //// source and target positions
        //Vector3 pos = animator.transform.position;
        //Vector3 target = playerPos.position;

        //// distance between target and source
        //float dist = Vector3.Distance(pos, playerPos.transform.position);

        //// rotate the object to face the target
        ////  animator.transform.LookAt(target);

        //// calculate initival velocity required to land the cube on target using the formula (9)
        //float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
        //float Vy, Vz;   // y,z components of the initial velocity

        //Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        //Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        //// Debug.Log(Vy);
        //// create the velocity vector in local space
        //Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        //// transform it to global vector
        //Vector3 globalVelocity = animator.transform.TransformVector(localVelocity);

        //// launch the cube by setting its initial velocity
        //_arcRigidBody.velocity = globalVelocity;


    }

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }


}

















































































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh