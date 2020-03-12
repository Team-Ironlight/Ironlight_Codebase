using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;

    public float PlatFOV;
    public float ZoomFOV;
    float currFOV;
    public float Smooth;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        PlatFOV = cam.fieldOfView;
    }

    public void ZoomIn()
    {

        //timer = Time.deltaTime * Smooth;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, ZoomFOV, Time.deltaTime * Smooth);

    }
    public void ZoomOut()
    {
        //timer = Time.deltaTime * Smooth;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, PlatFOV, Time.deltaTime * Smooth);
    }
}
