using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_CameraManager : MonoBehaviour
{
    [Header("Speed")]
    public float followSpeed = 9;
    public float mouseSpeed = 2;
    public float controllerSpeed = 7;

    [Header("Target Transforms")]
    public Transform target;
    //public Transform lockOnTransform

    [Header("Camera Components")]
    public Transform pivot;
    public Transform CameraTransform;

    PLY_PlayerStateManager playerState;

    public float turnSmoothing = 0.1f;
    public float minAngle = -15;
    public float maxAngle = 35;

    float smoothX;
    float smoothY; 
    float smoothXVelocity;
    float smoothYVelocity;

    public float lookAngle;
    public float tiltAngle;

    // Initialize a singleton to ensure no overlap
    public static SYS_CameraManager singleton;

    private void Awake()
    {
        if(singleton != null)
        {
            Destroy(this);
        }

        singleton = this;
    }

    public void Init(Transform t)
    {
        //playerState = state;
        target = t;

        CameraTransform = Camera.main.transform;
        pivot = CameraTransform.parent;
    }

    public void Tick(float delta)
    {

        // Mouse Input
        // TODO move this to InputHandler and StateManager
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        // Controller Input
        // TODO also add to InputHandler and StateManager
        //float c_H = 
        //float c_V =

        float targetSpeed = mouseSpeed;

        FollowTarget(delta);
        HandleRotations(delta, v, h, targetSpeed);

    }


    void FollowTarget(float d)
    {
        Debug.Log("Entered Follow Target");
        float speed = d * followSpeed;
        Debug.Log(speed);
        Vector3 targetPos = Vector3.Lerp(transform.position, target.position, speed);
        Debug.Log(targetPos);
        transform.position = targetPos;
    }

    void HandleRotations(float delta, float vertical, float horizontal, float targetSpeed)
    {
        if(turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, horizontal, ref smoothXVelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, vertical, ref smoothYVelocity, turnSmoothing);

        }
        else
        {
            smoothX = horizontal;
            smoothY = vertical;
        }



        tiltAngle -= smoothY * targetSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);


        // TODO lockon code

        Vector3 targetDir = transform.forward;
        Quaternion targetRot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, delta * 9);


        lookAngle += smoothX * targetSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);
    }
}
