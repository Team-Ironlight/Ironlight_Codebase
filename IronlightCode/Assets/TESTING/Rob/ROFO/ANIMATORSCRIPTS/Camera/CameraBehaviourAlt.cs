using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class CameraBehaviourAlt : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject cameraPivot;
        [SerializeField] private GameObject camera;

        [Range(10f, 200f)] [SerializeField] private float horizontalRotSpeed;
        [Range(1f, 10f)] [SerializeField] private float verticalRotSpeed;
        [Range(0.01f, 5f)] [SerializeField] private float zoomSpeed;

        [Range(1f, 10f)] [SerializeField] private float followDistanceMax;
        [Range(1f, 10f)] [SerializeField] private float followDistanceMin;
        private float followDistanceTotal;
        [SerializeField] private float followDistance;
        [Range(1f, 10f)] [SerializeField] private float followHeightMax;
        [Range(0.1f, 5f)] [SerializeField] private float followHeightMin;
        private float followHeightTotal;
        [SerializeField] private float followHeight;

        [SerializeField] private float followRotation;
        [Range(1.0f, 60.0f)] [SerializeField] private float followRotationMax;
        [Range(-60.0f, 0f)] [SerializeField] private float followRotationMin;
        private float followRotationDefault;

        private float rotX;
        private float rotY;
        private float rotZ;

        //enum to set camera controller
        private enum CameraController { Keyboard, Mouse };
        [SerializeField] private CameraController currentCamera;

        private string vertical;
        private string horizontal;

        //make it so players can decide the arrow directions
        private enum ControllerSetup { normal, inverted, invertedX, invertedY };
        [SerializeField] private ControllerSetup currentSetup;

        //various setups for controllers stored as dictionary
        private Dictionary<ControllerSetup, Vector2> setup = new Dictionary<ControllerSetup, Vector2>()
    {
        { ControllerSetup.normal, new Vector2(1f, -1f) },
        { ControllerSetup.inverted, new Vector2(-1f, 1f) },
        { ControllerSetup.invertedX, new Vector2(-1f, -1f) },
        { ControllerSetup.invertedY, new Vector2(1f, 1f) },

    };

        //controller for moving camera updown/ inout
        private enum CameraMode { Height, Distance };
        [SerializeField] private CameraMode currentCameraMode = CameraMode.Height;

        private enum CameraPosition { Top, Bottom };
        [SerializeField] private CameraPosition currentCameraPosition = CameraPosition.Bottom;



        // Start is called before the first frame update
        void Start()
        {
            Setup();

            //makes cursor invisible
            Cursor.lockState = CursorLockMode.Locked;
        }


        // Update is called once per frame
        void Update()
        {
            //camera fucntion using the correct axis
            SetCamera(vertical, horizontal, setup[currentSetup]);
        }

        //set camera
        private void Setup()
        {
            //set camera default height/distance
            followHeight = followHeightMin;
            followDistance = followDistanceMax;
            followRotation = 0.0f;

            //set the totals for height and distance
            followHeightTotal = followHeightMax - followHeightMin;
            followDistanceTotal = followDistanceMax - followDistanceMin;

            //store main rotation
            followRotationDefault = followRotation;

            //set camera pivot identical to player character
            cameraPivot.transform.position = player.transform.position;
            cameraPivot.transform.rotation = player.transform.rotation;

            //get rotations for quick access later
            rotX = cameraPivot.transform.rotation.eulerAngles.x;
            rotY = cameraPivot.transform.rotation.eulerAngles.y;
            rotZ = cameraPivot.transform.rotation.eulerAngles.z;

            //set axis according to enum
            SetEnum();

            //set starting camera position
            SetCamera();
        }

        //get input axis
        private void SetEnum()
        {
            //use enum to control camera settings
            if (currentCamera == CameraController.Keyboard)
            {
                vertical = "CameraVertical";
                horizontal = "CameraHorizontal";
            }
            else if (currentCamera == CameraController.Mouse)
            {
                vertical = "Mouse Y";
                horizontal = "Mouse X";
            }
        }


        //pass in the input strings for whatever you want to use, keyboard or mouse
        //setup h/v will be for effecting invert or not
        private void SetCamera(string vertical, string horizontal, Vector2 setup)
        {
            //keep pivot on player position but with seperate rotation
            cameraPivot.transform.position = player.transform.position;

            //if camera Hor movement
            if (Input.GetAxisRaw(horizontal) != 0)
            {
                //get rotation change
                rotY += Input.GetAxisRaw(horizontal) * Time.deltaTime * horizontalRotSpeed * setup.x;

                //change pivot rotation
                cameraPivot.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
            }
            //if camera vert movement
            if (Input.GetAxisRaw(vertical) != 0)
            {
                //height mode
                if (currentCameraMode == CameraMode.Height)
                {
                    followHeight += Input.GetAxisRaw(vertical) * Time.deltaTime * verticalRotSpeed * setup.y;

                    //fix if out of range
                    if (followHeight < followHeightMin)
                    {
                        //clamp
                        followHeight = followHeightMin;

                        //change mode
                        currentCameraMode = CameraMode.Distance;
                        currentCameraPosition = CameraPosition.Bottom;
                    }
                    else if (followHeight > followHeightMax)
                    {
                        //clamp
                        followHeight = followHeightMax;

                        //change mode
                        currentCameraMode = CameraMode.Distance;
                        currentCameraPosition = CameraPosition.Top;
                    }
                }
                //distance movement
                else if (currentCameraMode == CameraMode.Distance)
                {
                    //different implementation depending on top of bottom
                    if (currentCameraPosition == CameraPosition.Bottom)
                    {
                        //alter distance by mouse input
                        followDistance += Input.GetAxisRaw(vertical) * Time.deltaTime * zoomSpeed * setup.y;
                    }
                    else if (currentCameraPosition == CameraPosition.Top)
                    {
                        //alter distance by mouse input
                        followDistance -= Input.GetAxisRaw(vertical) * Time.deltaTime * zoomSpeed * setup.y;
                    }

                    //fix if out of range and potential switch
                    if (followDistance > followDistanceMax)
                    {
                        //clamp
                        followDistance = followDistanceMax;

                        followRotation = followRotationDefault;

                        //change mode
                        currentCameraMode = CameraMode.Height;
                    }
                    else if (followDistance < followDistanceMin)
                    {
                        followDistance = followDistanceMin;
                    }

                    //clamp rotate depending on cameraPosition
                    if (currentCameraPosition == CameraPosition.Top)
                    {
                        followRotation = (1 - (followDistance - followDistanceMin / followDistanceTotal)) * followRotationMin;
                    }
                    else if (currentCameraPosition == CameraPosition.Bottom)
                    {
                        followRotation = (1 - (followDistance - followDistanceMin / followDistanceTotal)) * followRotationMax;
                    }
                }

                //adjust camera as new follow height
                SetCamera();
            }
        }


        //code sugar
        private void SetCamera()
        {
            camera.transform.position = cameraPivot.transform.position + (cameraPivot.transform.up * followHeight) +
                                        (cameraPivot.transform.forward * -1 * followDistance);
            camera.transform.LookAt(cameraPivot.transform.position);
            camera.transform.Rotate(-followRotation, 0, 0);
        }
    }
}
