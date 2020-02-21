using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{ 
[RequireComponent(typeof(CameraSetup))]
public class CameraNew : MonoBehaviour
{
    public GameObject player;
    public LayerMask enemyLayerMask;
    private CameraSetup camSetup;
    private Camera cam;
    private float fOVstart;
    private float fOVcurrent;

    [Header("Camera")]
    public Transform pivotCamera;
    public Transform pivotMovement;
    

    [Header("Camera Position")]
    public Vector3 offset;

    //variables
    [Header("Camera Rotation")]
    public Vector2 startRotation = Vector2.zero;
    public int rotationSpeedX = 10;
    public int rotationSpeedY = 10;

    [Range(-180f,0f)] public float xMin;
    [Range(0f,180f)] public float xMax;

    [Header("Aimer Camera")]
    [Range(1, 180)] public float aimerFOV = 30f;
    [Range(1f, 180)] public float zoomSpeed = 10f;

    [Header("LockOn Camera")]
    public GameObject target = null;


    private string verticalAxis;
    private string horizontalAxis;


    //MIGHT NOT NEED
    //controller for moving camera updown/ inout
    public enum CameraMode { Normal, Aimer, LockOn };
    [SerializeField] private CameraMode camMode = CameraMode.Normal;

    //private enum CameraPosition { Top, Bottom };
    //[SerializeField] private CameraPosition currentCameraPosition = CameraPosition.Bottom;

    private void Awake()
    {
        camSetup = GetComponent<CameraSetup>();
        cam = GetComponent<Camera>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Tiny_Full");
        //Debug.Log(player.transform.position);

        //enemyLayerMask = LayerMask.GetMask("Enemy");
        //Debug.Log(enemyLayerMask.value);

        fOVstart = cam.fieldOfView;
        fOVcurrent = fOVstart;
        //get correct axis
        SetInputAxis();

        //set camera to start pos
        transform.position = transform.parent.position + offset;
        transform.rotation = Quaternion.Euler(startRotation.x, startRotation.y, 0f);

        //center cursor
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        //logic
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            camMode = CameraMode.Aimer;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            camMode = CameraMode.Normal;
            cam.fieldOfView = fOVstart;
            cam.nearClipPlane = 0.3f;
        }
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            camMode = CameraMode.LockOn;
            //send raycasts into the scene across an view range looking for enemies
            target = CustomSearch.GetNearestObj(player, 20, 120f, 100f, enemyLayerMask);

            if(target == null)
            {
                Debug.Log("No target Found");
                camMode = CameraMode.Normal;
            }
            else
            {
                Debug.Log("Target Name: " + target.name);
            }            
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            camMode = CameraMode.Normal;
        }


        //run
        if(camMode == CameraMode.Normal)
        {
            //run states here
            NormalCamera();
        }
        else if(camMode == CameraMode.Aimer)
        {
            AimerCamera();
        }
        else if(camMode == CameraMode.LockOn)
        {
            LockOnCamera();
        }
        
    }

    private void LockOnCamera()
    {
        //look at the found target
        transform.LookAt(target.transform);       
    }

    private void AimerCamera()
    {
        //don't do normal camera shit??
        if(cam.fieldOfView > aimerFOV)
        {
            cam.fieldOfView -= zoomSpeed * Time.deltaTime;
        }
        if(cam.nearClipPlane < 1f)
        {
            cam.nearClipPlane += zoomSpeed * Time.deltaTime;
        }

        NormalCamera();
    }



    private void NormalCamera()
    {
        //get camera input
        float x = Input.GetAxis(verticalAxis) * rotationSpeedX * Time.deltaTime;
        float y = Input.GetAxis(horizontalAxis) * rotationSpeedY * Time.deltaTime;

        //make temp rotation
        Vector3 temp = pivotCamera.rotation.eulerAngles;

        //add values to temp
        temp += new Vector3(x, y, 0f);

        //check it
        temp = ClampCamera(temp);

        //set it
        pivotCamera.rotation = Quaternion.Euler(temp);
        Vector3 t = new Vector3(0f, pivotCamera.rotation.eulerAngles.y, 0f);
        pivotMovement.rotation = Quaternion.Euler(t);
    }


    //take in the input and ensure that it stays within bounds
    private Vector3 ClampCamera(Vector3 v)
    {
        //add 360 to x value
        float x = v.x;
        //Debug.Log("X: " + x);

        if(x > xMax && x < 180)
        {
            x = xMax;
        }
        else if(x < 360 + xMin && x > 180)
        {
            x = 360 + xMin;
        }
     
        //clamp z rotation        
        return new Vector3(x, v.y, 0f);
    }


        //get input axis
        private void SetInputAxis()
        {
            //use enum to control camera settings
            if (camSetup.currentCamera == CameraSetup.CameraController.Keyboard)
            {
                verticalAxis = "Vertical";
                horizontalAxis = "Horizontal";
            }
            else if (camSetup.currentCamera == CameraSetup.CameraController.Mouse)
            {
                verticalAxis = "Mouse Y";
                horizontalAxis = "Mouse X";
            }
        }
    }
}
