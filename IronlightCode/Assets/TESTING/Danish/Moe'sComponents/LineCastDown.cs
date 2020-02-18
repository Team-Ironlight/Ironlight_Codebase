using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCastDown : MonoBehaviour
{
    //public GameObject parentPlayer;
	Vector3 startPoint;
	Vector3 endPoint;
	public LayerMask layerMask;
	Color rayColor;
	public float addedDist = 4f;
	public bool LCD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//startPoint = parentPlayer.transform.position;
        startPoint = transform.position;
        endPoint = startPoint;
		endPoint.y = startPoint.y - addedDist;
		LCD = Physics.Linecast(startPoint,endPoint,layerMask);
		if (LCD)
		{
			rayColor = Color.red;
		}
		else
		{
			rayColor = Color.green;
		}
		Debug.DrawLine(startPoint, endPoint, rayColor);
    }
}
