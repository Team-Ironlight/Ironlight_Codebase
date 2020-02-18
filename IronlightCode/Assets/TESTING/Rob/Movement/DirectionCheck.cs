using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//make a generic script to replace other checks
//shoots a ray/rays in desired direction/directions
//keep directions limited to 1 axis???
//keeping refernce to those collisions and calculating the closest point
public class DirectionCheck : MonoBehaviour
{
    public Transform cameraMovement;

    private RaycastHit collision;
    [Header("Variables")]
    //public Vector3 rayDirection = Vector3.zero;
    public float rayDistance = 1f;
    public LayerMask ignoreLayer;
    [Header("Ground Value")]
    //public float minY;
    [Tooltip("Desired distance to be from ground")]
    //public float distanceFromGround;

    public bool canMoveXZ = false;
    public int numbOfRays = 2;
    public float viewDegrees;
    public Vector3[] rays;
    public float rayAverage;
    public Vector3 raycastDirection = Vector3.zero;

    private void Start()
    {
        //create array for rays
        rays = new Vector3[numbOfRays];
        rayAverage = viewDegrees / numbOfRays;
    }


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        raycastDirection = cameraMovement.rotation * (new Vector3(h, 0f, v)).normalized;

        for (int i = 0; i < rays.Length; i++)
        {
            rays[i] = LinearRotation.AroundY(raycastDirection, rayAverage * i + 1);

            //get collisions
            if (Physics.Raycast(transform.position, rays[i] , out collision, rayDistance, ~ignoreLayer, QueryTriggerInteraction.Ignore))
            {
                //if collision don't allow XZ movement to happen??
                canMoveXZ = false;
                break;
            }
            else
            {
                canMoveXZ = true;
            }
        }


        //DEBUG
        if (canMoveXZ)
        {
            for (int i = 0; i < rays.Length; i++)
            {
                Debug.DrawRay(transform.position, rays[i] * 5f, Color.blue, 2f);
            }
            
        }
        else
        {
            for (int i = 0; i < rays.Length; i++)
            {
                Debug.DrawRay(transform.position, rays[i] * 5f, Color.cyan, 2f);
            }
        }
    }

    

    private void OnDrawGizmos()
    {
        if(canMoveXZ)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one/2f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one/2f);
        }
    }
}
