using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetPlayer;
    Camera playerCamera;
    public PlayerMove getActions; //used to get actions from PlayerMove
    public float cameraDistance = 5.0f;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public Vector2 mouseLook; //for Mouselook Axis
    public Vector2 rotateVert;

    //rotate camera only X axis
    public Quaternion camRotateX;
    //rotate Camera X andY axis
    public Quaternion camrotateXY;
    //look offset for 3rd person view
    public Vector3 lookOffset;


    public void Start()
    {
        getActions = FindObjectOfType<PlayerMove>();
        playerCamera = GetComponentInChildren<Camera>();
        lookOffset = playerCamera.transform.position - targetPlayer.transform.position;
    }

    public void Update()
    {
        cameraControl();
    }

    public void cameraControl()
    {
        var md = new Vector2(Input.GetAxisRaw("MouseX"), Input.GetAxisRaw("MouseY"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        rotateVert.x = Mathf.Lerp(rotateVert.x, md.x, 1f / smoothing);
        rotateVert.y = Mathf.Lerp(rotateVert.y, md.y, 1f / smoothing);
        mouseLook += rotateVert;

        //setting mouse angles for rotation
       //mouseLook.y = Mathf.Clamp(mouseLook.y, 40, -40);
        //this is optional if you want your X axis to work
       //mouseLook.x = Mathf.Clamp(mouseLook.y, -90, 90);

        //rotate cam on both axis
        camrotateXY = Quaternion.Euler(-mouseLook.y, mouseLook.x, 0);

        //rotate cam on x Asix only
        camRotateX = Quaternion.Euler(0, mouseLook.x, 0);

        //rotate camera around the player and rotate player with camera...check actions and conditions
        if (getActions.checkActions == true)
        {
            targetPlayer.eulerAngles = new Vector3(0, camRotateX.eulerAngles.y, 0);
        }
        else
        {
            Vector3 lookPoint = targetPlayer.transform.position;
            playerCamera.transform.LookAt(lookPoint + lookOffset);
        }

        //FPS look with only x rotation and on both axis rotation


        //third person look with x axis rotation
        Vector3 position = targetPlayer.position - (camRotateX * Vector3.forward * cameraDistance + new Vector3(0, -lookOffset.y, 0));

        //rotate cam only with X Axis
        //playerCamera.transform.rotation = camRotateX;

        //rotate cam ob Both Axis
        playerCamera.transform.rotation = camrotateXY;

        playerCamera.transform.position = position;
    }



}
