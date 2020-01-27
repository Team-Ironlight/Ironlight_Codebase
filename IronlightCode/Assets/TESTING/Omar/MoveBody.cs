using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBody : MonoBehaviour
{
    Rigidbody rb;
	Vector3 movement;
	float moveX;
	float moveZ;
	public float speed = 15;
    float gravity;
    public float jumpforce;

	// Start is called before the first frame update
	void Start()
	{
        rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		MoveObject();

        //gravity = -1;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vector3 jump = new Vector3(0,1,0);
        //    //transform.position += jump * jumpforce * Time.deltaTime;

        //    rb.AddForce(jump * jumpforce * Time.deltaTime,ForceMode.Impulse);
        //}
	}


	void MoveObject()
	{
		moveX = Input.GetAxis("Horizontal");
		moveZ = Input.GetAxis("Vertical");
		movement = new Vector3(moveX, 0, moveZ);
		transform.position += movement * Time.deltaTime * speed;
	}


}
