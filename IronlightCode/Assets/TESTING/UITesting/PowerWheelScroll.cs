using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWheelScroll : MonoBehaviour
{
	public GameObject RotatorObj;
	float scrollCount = 0f;
	public float scrollSpeed = 5;
    public float activeAbility = 0;

	// Start is called before the first frame update
	void Start()
    {
		// currentAngle = transform.eulerAngles;
	}

    // Update is called once per frame
    void Update()
    {


		//if (Input.mouseScrollDelta.y>0)
		//{
		//	print("CounterClockwise!");

		//	scrollCount += 1;

		//	StartCoroutine(RotateWheel());

		//}

		//if (Input.mouseScrollDelta.y<0)
		//{
		//	print("Clockwise!");

		//	scrollCount -= 1;

		//	StartCoroutine(RotateWheel());

		//}

        //activeAbility = Mathf.Abs(scrollCount % 3);
        //print(activeAbility);

	}

	public void RotateWheelFunc(float activeAbility)
	{
		StartCoroutine(RotateWheel(activeAbility));
	}

	IEnumerator RotateWheel(float activeAbility)
	{

		Quaternion target = Quaternion.AngleAxis(120 * (activeAbility), Vector3.forward);
		for (float t = 0f; t <= 1f; t += scrollSpeed * Time.deltaTime)
		{
			transform.rotation = Quaternion.Slerp(RotatorObj.transform.rotation, target, t);
			yield return null;
		}

	}


}
