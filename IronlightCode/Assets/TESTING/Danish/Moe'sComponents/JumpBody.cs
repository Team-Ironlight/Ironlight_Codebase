using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBody : MonoBehaviour
{
    public GameObject player;
	Rigidbody playerrb;
	public float jumpForce;
	public LineCastDown LCD;
	//public FloatingBody fb;
	public float delayTime = 1f;

	// Start is called before the first frame update
	void Start()
    {
		playerrb = player.GetComponent<Rigidbody>();
		//fb = GetComponent<FloatingBody>();
	}

    // Update is called once per frame
    void LateUpdate()
    {
		if(!LCD.LCD)
		{
			playerrb.useGravity = true;
		}
		else if(LCD.LCD)
		{
			playerrb.useGravity = false;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				jumpBody();
			}
		}
    }
	void jumpBody()
	{
		//fb.enabled = false;
		playerrb.AddForce(jumpForce*transform.up,ForceMode.Impulse);
		StartCoroutine(delayIdle());
	}

	IEnumerator delayIdle()
	{
		yield return new WaitForSeconds(delayTime);
		//fb.enabled = true;
	}
}
