using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBody : MonoBehaviour
{
	Vector3 _newPosition;
	//Vector3 _startPosition;
	public float amplitude = 2f;
	public float frequency = 2f;
	public float offset = 1.5f;

	float floatyFactor;

	//Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
		//rb.GetComponent<Rigidbody>();
		//_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		floaty();
	}

	public void floaty()
	{
		floatyFactor = offset + (Mathf.Sin(Time.time * frequency) * amplitude);

		_newPosition = transform.position;
		_newPosition.y = floatyFactor;
		transform.position = _newPosition;

		//rb.AddForce(Vector3.up * (floatyFactor), ForceMode.Acceleration);

	}

	public float floatyness()
	{
		return floatyFactor;
	}
}
