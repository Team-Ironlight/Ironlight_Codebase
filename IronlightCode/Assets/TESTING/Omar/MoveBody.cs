using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBody : MonoBehaviour
{
	Vector3 movement;
	float moveX;
	float moveZ;
	public float speed = 15;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		MoveObject();
	}


	void MoveObject()
	{
		moveX = Input.GetAxis("Horizontal");
		moveZ = Input.GetAxis("Vertical");
		movement = new Vector3(moveX, 0, moveZ);
		transform.position += movement * Time.deltaTime * speed;
	}
}
