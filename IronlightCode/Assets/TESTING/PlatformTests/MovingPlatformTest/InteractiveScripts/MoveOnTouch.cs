using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouch : MonoBehaviour
{

	
    public Transform MovingPlatform;
    
    public Transform startPosition;
    
    public Transform endPosition;
    
    public float moveSpeed;

	private bool moving;

	Vector3 direction;
    Transform destination;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			moving = true;
			collision.collider.transform.SetParent(transform);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			collision.collider.transform.SetParent(null);
			moving = false;
		}
	}

	private void FixedUpdate()
	{
        if(moving)
        {
            LoopOnce();
	    }
	}

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - this.transform.position).normalized;  //we want the exact value of  X Y Z ,the stable value that's why i used normalized here!
    }

	void LoopOnce()
    {
        SetDestination(endPosition);
        this.GetComponent<Rigidbody>().MovePosition(this.transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(this.transform.position, destination.position) < moveSpeed * Time.fixedDeltaTime)
        {
            moveSpeed = 0;
        }
    }
	
	void OnDrawGizmos()
    {
        if (startPosition.position != null)  //safety check to avoid Object null reference
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(startPosition.position, this.transform.localScale);
        }
        if (endPosition.position != null)   //safety check to avoid Object null reference
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(endPosition.position, this.transform.localScale);
        }
    }
}
