using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KinematicJumper : MonoBehaviour
{
	public float jumpDistForward = 3.0f;
	public float maxJumpHeight = 3.0f;
	float groundHeight;
	Vector3 groundPos;
	Vector3 targetPos;
	Vector3 jumpDir = (Vector3.up + Vector3.forward);
	float jumpSpeed = 7.0f;
	float fallSpeed = 12.0f;
	public bool inputJump = false;
	public bool grounded = true;
	Rigidbody rb;


	void Start()
	{
		rb = GetComponent<Rigidbody>();
		groundPos = transform.position;
		groundHeight = transform.position.y;
		maxJumpHeight = transform.position.y + maxJumpHeight;
		targetPos = -Vector3.forward * jumpDistForward;

	}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (grounded)
			{
				groundPos = transform.position;
				inputJump = true;
				StartCoroutine(Jump());
			}
		}
		if (transform.position == groundPos)
			grounded = true;
		else
			grounded = false;
	}

	IEnumerator Jump()
	{

		jumpDir *= jumpDistForward / 2;
		while (true)
		{
			if (transform.position.y >= maxJumpHeight)
				inputJump = false;
			if (inputJump)
				rb.MovePosition(transform.position + (transform.up * jumpSpeed * Time.smoothDeltaTime));
			//transform.Translate(jumpDir * jumpSpeed * Time.smoothDeltaTime);
			//transform.Translate(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
			else if (!inputJump)
			{
				jumpDistForward =  10;
				targetPos = -Vector3.forward * jumpDistForward;
				//transform.position = Vector3.Slerp(transform.position, (groundPos + targetPos), fallSpeed * Time.smoothDeltaTime);
				//Vector3 newDirection = Vector3.Lerp(transform.position, (groundPos + targetPos), fallSpeed * Time.smoothDeltaTime);
				Vector3 directionToGround = (groundPos + targetPos) - transform.position;
				directionToGround.Normalize();

				rb.MovePosition(transform.position + (directionToGround * fallSpeed * Time.smoothDeltaTime));
				if (transform.position == groundPos)
					StopAllCoroutines();

			}

			yield return new WaitForEndOfFrame();
		}
	}

	

	
}