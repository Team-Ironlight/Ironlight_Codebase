using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject target;

    private Transform _ParentTransform;
    private Vector3 _LocalRotation;
    private Vector3 _TargetLocalPosition;

    [SerializeField] private bool Inverted = false;

    [Header("Idle")]
    [SerializeField] private float idleSpinSpeed = 1;
    [SerializeField] private float idleSpinY = 40;

    [Header("Camera Collision")]
    [SerializeField] private LayerMask cameraCollisionLayers;
    [SerializeField] private float cameraCollisionDampening = 20;
    [SerializeField] [Range(0, 1)] private float cameraCollisionMinDisPercent = 0.1f;
    [SerializeField] private float cameraCollisionOffset = 0.1f;

    [Space]
    public float mouseSensitivity = 4f;


    [SerializeField] private SOCamera defaultCamera;
    [SerializeField] private SOCamera aimCamera;
    private float mouseSensitivityMult = 1f;
    [SerializeField] private float turnDampening = 10f;
    private float offSetUp = 0.6f;
    private float offSetLeft = 0f;
    private float cameraDistance = 16f;
    private float cameraMinHeight = -20f;
    private float cameraMaxHeight = 90f;

    [Space]
    public bool CameraDisabled = false;
    public bool Idle = false;
    
    private Coroutine transRoutine;

    void Start()
    {
        //Set Camera to default values
        //ResetCameraVars();
        ResetCameraVars(defaultCamera);

        //Getting Transforms
        _ParentTransform = transform.parent;

        //Maintaining Starting Rotation
        _LocalRotation.x = _ParentTransform.eulerAngles.y;
        _LocalRotation.y = _ParentTransform.eulerAngles.x;

        //Locking cursor
        //Cursor.lockState = CursorLockMode.Locked;

        //Setting camera distance
        _TargetLocalPosition = new Vector3(-offSetLeft, 0f, cameraDistance * -1f);
        transform.localPosition = _TargetLocalPosition;
    }

    public void ResetCameraVars()
    {
        mouseSensitivityMult = defaultCamera.SensitivityMult;
        offSetUp = defaultCamera.UpOffset;
        offSetLeft = defaultCamera.LeftOffset;
        cameraDistance = defaultCamera.Distance;
        cameraMinHeight = defaultCamera.MinY;
        cameraMaxHeight = defaultCamera.MaxY;
    }

    public void ResetCameraVars(SOCamera preset)
    {
        mouseSensitivityMult = preset.SensitivityMult;
        offSetUp = preset.UpOffset;
        offSetLeft = preset.LeftOffset;
        cameraDistance = preset.Distance;
        cameraMinHeight = preset.MinY;
        cameraMaxHeight = preset.MaxY;


        _TargetLocalPosition = new Vector3(-offSetLeft, 0f, cameraDistance * -1f);
        transform.localPosition = _TargetLocalPosition;

    }

    //public void Respawn(float pRotationY) 
    //{
    //    transform.parent.rotation = Quaternion.Euler(0, pRotationY, 0);
    //    _LocalRotation.x = pRotationY;
    //    _LocalRotation.y = 0;
    //}

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ResetCameraVars(aimCamera);
        }
        else
        {
            ResetCameraVars(defaultCamera);
        }



        //Getting Mouse Movement
        if (!CameraDisabled)
        {
            if (Idle == true)
            {
                IdleCameraMovement();
            }
            else
            {
                DefaultCameraMovement();
            }
        }

        //Actual Camera Transformations
        Quaternion TargetQ = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        _ParentTransform.rotation = Quaternion.Slerp(_ParentTransform.rotation, TargetQ, Time.deltaTime * turnDampening);

        //Position the camera pivot on the player
        _ParentTransform.position = target.transform.position + (Vector3.up * offSetUp);

        //Camera Collision
        CameraCollision();
    }

    void DefaultCameraMovement(float pInputModifier = 1f)
    {
        //Rotation of the camera based on mouse movement
        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY") != 0)
        {
            _LocalRotation.x += Input.GetAxis("MouseX") * mouseSensitivity * mouseSensitivityMult * pInputModifier;
            _LocalRotation.y -= Input.GetAxis("MouseY") * mouseSensitivity * mouseSensitivityMult * pInputModifier * (Inverted ? -1 : 1);

            //Clamping the y rotation to horizon and not flipping over at the top
            if (_LocalRotation.y < cameraMinHeight)
            {
                _LocalRotation.y = cameraMinHeight;
            }
            else if (_LocalRotation.y > cameraMaxHeight)
            {
                _LocalRotation.y = cameraMaxHeight;
            }
        }
    }

    void IdleCameraMovement()
    {
        //Slowly Rotate
        _LocalRotation.x += idleSpinSpeed * Time.deltaTime;
        _LocalRotation.y = Mathf.Lerp(_LocalRotation.y, idleSpinY, Time.deltaTime);
    }





    // Camera Collision //////////////
    void CameraCollision()
    {
        RaycastHit hit;
        Vector3 rayDirection = (transform.position - _ParentTransform.position).normalized;
        Physics.Raycast(_ParentTransform.position, rayDirection, out hit, cameraDistance + cameraCollisionOffset, cameraCollisionLayers);

        if (hit.point != Vector3.zero)
        {
            hit.point -= _ParentTransform.position + rayDirection * cameraCollisionOffset;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _TargetLocalPosition * Mathf.Clamp((hit.point.magnitude / _TargetLocalPosition.magnitude), cameraCollisionMinDisPercent, 0.5f), Time.deltaTime * cameraCollisionDampening);
            //Debug.Log(hit.point.magnitude / _TargetLocalPosition.magnitude * 2 * 100 + "%");
            //Debug.DrawLine(_ParentTransform.position, hit.point + _ParentTransform.position, Color.red, 0.1f);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _TargetLocalPosition * 0.5f, Time.deltaTime * cameraCollisionDampening);
        }
    }




    // Camera Transition ////////////// Lerp camera variables
    public void ChangePlayerCamera(float pOffSetUp, float pOffSetLeft, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pSensitivityMult, float pTransitionSpeed)
    {
        if (transRoutine != null)
            StopCoroutine(transRoutine);
        transRoutine = StartCoroutine(CameraVarsTransition(pOffSetUp, pOffSetLeft, pTurnDampening, pCameraDistance, pCameraMinHeight, pCameraMaxHeight, pSensitivityMult, pTransitionSpeed));
    }

    public void ChangePlayerCamera(SOCamera pPreset, float pTransitionSpeed)
    {
        if (transRoutine != null)
            StopCoroutine(transRoutine);
        transRoutine = StartCoroutine(CameraVarsTransition(pPreset.UpOffset, pPreset.LeftOffset, pPreset.TurnDampening, pPreset.Distance, pPreset.MinY, pPreset.MaxY, pPreset.SensitivityMult, pTransitionSpeed));
    }

    public IEnumerator CameraVarsTransition(float pOffSetUp, float pOffSetLeft, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pSensitivityMult, float pTransitionSpeed)
    {
        mouseSensitivityMult = pSensitivityMult;

        short done;

        do {
            done = 0;

            //Lerping all of the values
            turnDampening = Mathf.Lerp(turnDampening, pTurnDampening, pTransitionSpeed * Time.deltaTime * 10);
            if (Mathf.Abs(turnDampening - pTurnDampening) <= 0.01f)
            {
                turnDampening = pTurnDampening;
                done++;
            }

            cameraDistance = Mathf.Lerp(cameraDistance, pCameraDistance, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(cameraDistance - pCameraDistance) <= 0.01f)
            {
                cameraDistance = pCameraDistance;
                done++;
            }

            cameraMinHeight = Mathf.Lerp(cameraMinHeight, pCameraMinHeight, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(cameraMinHeight - pCameraMinHeight) <= 0.01f)
            {
                cameraMinHeight = pCameraMinHeight;
                done++;
            }

            cameraMaxHeight = Mathf.Lerp(cameraMaxHeight, pCameraMaxHeight, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(cameraMaxHeight - pCameraMaxHeight) <= 0.01f)
            {
                cameraMaxHeight = pCameraMaxHeight;
                done++;
            }

            offSetLeft = Mathf.Lerp(offSetLeft, pOffSetLeft, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(offSetLeft - pOffSetLeft) <= 0.01f)
            {
                offSetLeft = pOffSetLeft;
                done++;
            }

            offSetUp = Mathf.Lerp(offSetUp, pOffSetUp, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(offSetUp - pOffSetUp) <= 0.01f)
            {
                offSetUp = pOffSetUp;
                done++;
            }

            //Setting camera distance
            _TargetLocalPosition = new Vector3(-offSetLeft, 0f, cameraDistance * -1f);

            yield return null;
        }
        while (done != 6);
    }
}
