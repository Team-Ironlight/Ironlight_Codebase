using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWheelScroll : MonoBehaviour
{
	Vector3 rotationEuler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.rotation = Quaternion.Euler(rotationEuler);

		if (Input.mouseScrollDelta.y>0)
		{
			rotationEuler += Vector3.forward * 120 * Input.mouseScrollDelta.y;
		}

		if (Input.mouseScrollDelta.y<0)
		{
			rotationEuler += Vector3.forward * 120 * Input.mouseScrollDelta.y;
		}


    }
}
