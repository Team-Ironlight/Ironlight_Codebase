using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    public PlayerCamera camScript;

    public float PlatFOV;
    public float ZoomFOV;
    float currFOV;
    public float Smooth;

    [SerializeField] private SOCamera _DefaultPreset;
    [SerializeField] private SOCamera _AimingPreset;

    public bool zoomed;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        PlatFOV = cam.fieldOfView;
    }

    public void ZoomIn()
    {
        Debug.Log("ZoomIn");
        if (zoomed == false)
        {
            camScript.ChangePlayerCamera(_AimingPreset, Smooth);
            zoomed = true;
        }
        //timer = Time.deltaTime * Smooth;
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, ZoomFOV, Time.deltaTime * Smooth);

    }
    public void ZoomOut()
    {
        Debug.Log("ZoomOut");
        if (zoomed == true)
        {
            camScript.ChangePlayerCamera(_DefaultPreset, Smooth);
            zoomed = false;
        }
        //timer = Time.deltaTime * Smooth;
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, PlatFOV, Time.deltaTime * Smooth);
    }
}
