using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linecastdown : MonoBehaviour
{
    public bool LillyPad;
    public LayerMask layerMask;
    Transform player;
    Vector3 startPoint;
    Vector3 endPoint;
    Color rayColor;
    public float addedDist = 8;
    
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
        LillyPad = Physics.Linecast(startPoint,endPoint, out RaycastHit hit,layerMask);
        if (LillyPad)
        {
            print("Lillypad hit!");
            rayColor = Color.green;
            hit.collider.gameObject.GetComponent<LillyPad>().falllillypad();
        }
        else
        {
            rayColor = Color.red;
            //hit.collider.gameObject.GetComponent<LillyPad>().riselillypad();
        }
    }
}
