using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    private RaycastHit ceilingCollision;
    [Header("Variables")]
    public float rayDistance = 1f;
    public LayerMask ignoreLayer;
    [Header("Ceiling Value")]
    public float maxY;
    [Tooltip("Desired distance to be from ceiling")]
    public float distanceFromCeiling;


    // Update is called once per frame
    void Update()
    {
        maxY = CheckCeiling();
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, maxY, transform.position.z), Color.red, 1f);
    }

    private float CheckCeiling()
    {
        //shoot raycast down certain distance as infite has more cost
        //ignoring certain layers such as self
        //create lowest y value player can be
        if (Physics.Raycast(transform.position, Vector3.up, out ceilingCollision, rayDistance, ~ignoreLayer))
        {
            //Debug.Log("<color=blue>Point: </color>" + groundCollision.point);
            //Debug.Log("<color=purple>Ground: </color>" + (groundCollision.point.y + transform.localScale.y));
            return ceilingCollision.point.y + distanceFromCeiling;
        }
        else
        {
            //if no collision than lowest is at least this far
            return transform.position.y + rayDistance;
        }
    }
}
