using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ROFO
{
    public class CameraBehaviour : MonoBehaviour
    {
        //References
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject cameraPivot;
        [SerializeField] private GameObject camera;

        //Variables   
        [Header("Distance")]
        [SerializeField] private float followDistance;
        [Range(1f, 10f)] [SerializeField] private float followDistanceMax;
        [Range(0.1f, 5f)] [SerializeField] private float followDistanceMin;
        [Header("Height")]
        [SerializeField] private float followHeight;
        [Range(1f, 10f)] [SerializeField] private float followHeightMax;
        [Range(0.1f, 5f)] [SerializeField] private float followHeightMin;
        [Header("Rotation")]
        [Range(10f, 200f)] [SerializeField] private float horizontalRotSpeed;
        [Range(1f, 10f)] [SerializeField] private float verticalRotSpeed;
        [Range(0.01f, 5f)] [SerializeField] private float zoomSpeed;
        [SerializeField] private float followRotation;
        [Range(0f, 90f)] [SerializeField] private float heightMultiplier;
        [Range(0f, 90f)] [SerializeField] private float distanceMultiplier;
        private float followRotationTotal;

        //hidden variables
        private float rotX;
        private float rotY;
        private float rotZ;



        //enum to set camera controller
        private enum CameraController { Keyboard, Mouse };
        [Header("Camera Options")]
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
            MoveCamera(vertical, horizontal, setup[currentSetup]);
        }

        //set camera
        private void Setup()
        {
            //set camera default height/distance
            followHeight = followHeightMin;
            followDistance = followDistanceMax;
            //set rotation to min
            followRotation = 0;

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
                vertical = "MouseVertical";
                horizontal = "MouseHorizontal";
            }
        }


        //pass in the input strings for whatever you want to use, keyboard or mouse
        //setup h/v will be for effecting invert or not
        private void MoveCamera(string vertical, string horizontal, Vector2 setup)
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
                    //adjust the height of camera
                    followHeight += Input.GetAxisRaw(vertical) * Time.deltaTime * verticalRotSpeed * setup.y;

                    //fix if out of range
                    if (followHeight < followHeightMin)
                    {
                        //clamp
                        followHeight = followHeightMin;
                        followDistance = followDistanceMax;

                        //change mode
                        currentCameraMode = CameraMode.Distance;
                    }
                    else if (followHeight > followHeightMax)
                    {
                        //clamp
                        followHeight = followHeightMax;
                        followDistance = followDistanceMin;
                    }
                    else
                    {
                        //adjust distance to be very close when at max height
                        followDistance = (1 - ((followHeight - followHeightMin) / (followHeightMax - followHeightMin))) * (followDistanceMax - followDistanceMin) + followDistanceMin;
                    }

                    //adjust rotation
                    //get a percentage from height, set rotation equal to that percentage
                    followRotation = (followHeight - followHeightMin) / (followHeightMax - followHeightMin);


                }
                //distance movement
                else if (currentCameraMode == CameraMode.Distance)
                {
                    //alter distance by mouse input
                    followDistance += Input.GetAxisRaw(vertical) * Time.deltaTime * zoomSpeed * setup.y;

                    //fix if out of range and potential switch
                    if (followDistance > followDistanceMax)
                    {
                        //clamp
                        followDistance = followDistanceMax;

                        //change mode
                        currentCameraMode = CameraMode.Height;
                    }
                    else if (followDistance < followDistanceMin)
                    {
                        followDistance = followDistanceMin;
                    }

                    //adjust angle
                    //get a percentage from height, set rotation equal to that percentage
                    followRotation = 1 - ((followDistance - followDistanceMin) / (followDistanceMax - followDistanceMin));
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
            //new
            camera.transform.rotation = cameraPivot.transform.rotation;
            if (currentCameraMode == CameraMode.Height)
            {
                camera.transform.Rotate(followRotation * heightMultiplier, 0, 0);
            }
            else if (currentCameraMode == CameraMode.Distance)
            {
                camera.transform.Rotate(-followRotation * distanceMultiplier, 0, 0);
            }
        }
    }
}
