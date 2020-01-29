using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWheelScroll : MonoBehaviour
{

	float scrollCount = 0f;


	// Start is called before the first frame update
	void Start()
    {
		// currentAngle = transform.eulerAngles;
	}

    // Update is called once per frame
    void Update()
    {


		if (Input.mouseScrollDelta.y>0)
		{
			print("CounterClockwise!");

			scrollCount += 1;

			StartCoroutine(RotateWheel());

		}

		if (Input.mouseScrollDelta.y<0)
		{
			print("Clockwise!");

			scrollCount -= 1;

			StartCoroutine(RotateWheel());

		}

		switch (scrollCount % 3)
		{
			case 0:
				print("Ability 1!");
				break;
			case 1:
				print("Ability 2!");
				break;
			case 2:
				print("Ability 3!");
				break;
		}


	}


	IEnumerator RotateWheel()
	{

		Quaternion target = Quaternion.AngleAxis(120 * (scrollCount%3), Vector3.forward);
		for (float t = 0f; t <= 1f; t += 5 * Time.deltaTime)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, target, t);
			yield return null;
		}

	}


}
