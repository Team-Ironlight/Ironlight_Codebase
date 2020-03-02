using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linecastdown : MonoBehaviour
{
	public bool LillyPad;
	public bool Ground;
	public bool FallingLeaf;
	public LayerMask layerMaskGround;
	public LayerMask layerMaskLillyPad;
	public LayerMask layerMaskFallingLeaf;
	Transform player;
	Vector3 startPoint;
	Vector3 endPoint;
	Color rayColor;
	RaycastHit hit;

	public float addedDist = 0.5f;

	// Start is called before the first frame update
	void Start()
	{
		rayColor = Color.white;
	}

	// Update is called once per frame
	void Update()
	{
		startPoint = this.transform.position;
		endPoint = new Vector3(startPoint.x, startPoint.y - addedDist, startPoint.z);
		Debug.DrawLine(startPoint, endPoint, rayColor);
		LillyPad = Physics.Linecast(startPoint, endPoint, out hit, layerMaskLillyPad);
		Ground = Physics.Linecast(startPoint, endPoint, out hit, layerMaskGround);
		FallingLeaf = Physics.Linecast(startPoint, endPoint, out hit, layerMaskFallingLeaf);

		if (LillyPad || FallingLeaf || Ground)
		{
			print("LineCast hit!");
			rayColor = Color.green;
			//hit.collider.gameObject.GetComponent<LillyPad>().falllillypad();
		}
		else
		{
			rayColor = Color.red;
			// NOT GROUNDED HERE
		}
	}
}
