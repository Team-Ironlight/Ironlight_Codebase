using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWheelScroll : MonoBehaviour
{

	private Vector3 currentAngle;

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

			// currentAngle = new Vector3 (0, 0, Mathf.LerpAngle(currentAngle.z, currentAngle.z + 120 * Input.mouseScrollDelta.y, Time.deltaTime));

            Quaternion newRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w); ;
            newRotation *= Quaternion.Euler(0, 0, 360 * Input.mouseScrollDelta.y); // this add a 120 degrees Z rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 20 * Time.deltaTime);

            // transform.eulerAngles = currentAngle;
		}

		if (Input.mouseScrollDelta.y<0)
		{
			print("Clockwise!");

			// currentAngle = new Vector3(0, 0, Mathf.LerpAngle(currentAngle.z, currentAngle.z + 120 * Input.mouseScrollDelta.y, Time.deltaTime));

            Quaternion newRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w); ;
            newRotation *= Quaternion.Euler(0, 0, 120 * Input.mouseScrollDelta.y); // this add a 120 degrees Z rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 20 * Time.deltaTime);

            // transform.eulerAngles = currentAngle;
		}
    }	
}
