using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script requires the GameObject to have a BoxCollider component
//[RequireComponent(typeof(BoxCollider))]
public class Interactive_Obj : MonoBehaviour {

    public bool activate = false;

    // public new Rigidbody rigidbody;

    [SerializeField]
    Transform rock_OBJ;
    [SerializeField]
    Transform startPosition;
    [SerializeField]
    Transform endPosition;
    [SerializeField]
    float moveSpeed;

    Vector3 direction;
    Transform destination;


    // We will used rigidBody to move, coz we dont want the Object to be move like a teleport !!
    // Docu @ https://docs.unity3d.com/ScriptReference/Rigidbody.MovePosition.html

    public enum LoopType
    {
        Once,
        PingPong
    }
    public LoopType loopType;  // let us set this Script into Dynamic use

    void Start()
    {
        SetDestination(startPosition);
    }


    // This Script is Dynamic , can be re-use in any interactive platform
    void FixedUpdate()
    {
        if (activate)
        {

            switch (loopType)
            {
                case LoopType.Once:
                    LoopOnce();
                    break;
                case LoopType.PingPong:
                    LoopPingPong();
                    break;
            }

        }


    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - rock_OBJ.position).normalized;  //we want the exact value of  X Y Z ,the stable value that's why i used normalized here!
    }
    void LoopOnce()
    {
        SetDestination(endPosition);
        rock_OBJ.GetComponent<Rigidbody>().MovePosition(rock_OBJ.position + direction * moveSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(rock_OBJ.position, destination.position) < moveSpeed * Time.fixedDeltaTime)
        {
            moveSpeed = 0;
        }
    }
    void LoopPingPong()
    {

        rock_OBJ.GetComponent<Rigidbody>().MovePosition(rock_OBJ.position + direction * moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(rock_OBJ.position, destination.position) < moveSpeed * Time.fixedDeltaTime)
        {
            //This simply the IF THEN ELSE , if desti equal to start then set destination as endPosition, otherwise  startPosition.
            SetDestination(destination == startPosition ? endPosition : startPosition);
        }
    }

    //This is for tracing/positioning the start and end 
    void OnDrawGizmos()
    {
        if (startPosition.position != null)  //safety check to avoid Object null reference
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(startPosition.position, rock_OBJ.localScale);
        }
        if (endPosition.position != null)   //safety check to avoid Object null reference
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(endPosition.position, rock_OBJ.localScale);
        }
    }
}








