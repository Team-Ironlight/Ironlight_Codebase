using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages ground
//ensure obj is always at least x distance above collision down
public class GroundCheck : MonoBehaviour
{
    private RaycastHit groundCollision;  
    [Header("Variables")]
    public float rayDistance = 1f;
    public LayerMask ignoreLayer;
    [Header("Ground Value")]
    public float minY;
    [Tooltip("Desired distance to be from ground")]
    public float distanceFromGround;

    
    // Update is called once per frame
    void Update()
    {
        minY = CheckGround();
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, minY, transform.position.z), Color.green, 1f);
    }

    private float CheckGround()
    {
        //shoot raycast down certain distance as infite has more cost
        //ignoring certain layers such as self
        //create lowest y value player can be
        if (Physics.Raycast(transform.position, Vector3.down, out groundCollision, rayDistance, ~ignoreLayer, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log("<color=blue>Point: </color>" + groundCollision.point);
            //Debug.Log("<color=purple>Ground: </color>" + (groundCollision.point.y + transform.localScale.y));
            
            return groundCollision.point.y + distanceFromGround;
        }
        else
        {
            //if no collision than lowest is at least this far
            return transform.position.y - rayDistance;
        }
    }
}
