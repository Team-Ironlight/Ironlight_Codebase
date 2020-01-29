using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundChecker : MonoBehaviour
{
    float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        // get the distance to ground
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    public bool IsGrounded()
    {
        print("grounded");
        return Physics.Linecast(transform.position, Vector3.down, (int)(distToGround + 0.1));
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
