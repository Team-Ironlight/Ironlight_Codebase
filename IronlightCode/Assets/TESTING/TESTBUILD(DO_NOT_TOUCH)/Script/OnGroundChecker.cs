using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundChecker : MonoBehaviour
{
    //[SerializeField] private LayerMask layerMask;
    //float distToGround;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // get the distance to ground
        //distToGround = GetComponent<Collider>().bounds.extents.y;
        
    }

    public bool IsGrounded()
    {
        //print("grounded");
        //bool rayCastHit =  Physics.Raycast(GetComponent<Collider>().bounds.center, Vector3.down, (int)(distToGround + 0.1), layerMask);

        //Color rayColor;
        //if (rayCastHit)
        //{
        //    rayColor = Color.red;
        //}
        //else
        //{
        //    rayColor = Color.green;
        //}

        //Debug.DrawRay(GetComponent<Collider>().bounds.center, Vector3.down * (int)(distToGround + 0.1));

        if (player.position.y<0.1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
