using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCastFloating : MonoBehaviour
{
    //public GameObject parentPlayer;
    Vector3 startPoint;
    Vector3 endPoint;
    public LayerMask layerMask;
    Color rayColor;
    public float addedDist = 2f;
    public bool LCF;


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
        LCF = Physics.Linecast(startPoint, endPoint, layerMask);
        if (LCF)
        {
            rayColor = Color.red;
            //rb.AddForce(-transform.up * 100, ForceMode.Acceleration);
           // rb.useGravity = true;
        }
        else
        {
            rayColor = Color.green;
        }
        Debug.DrawLine(startPoint, endPoint, rayColor);
    }


}
