using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//simple camera that will follow the player from behind
//starting at an offset
public class CameraTest : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Vector3 offset;
    [Range(0.01f, 1f)]public float lerpPerc = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        cam.transform.position = target.position + offset;
        cam.transform.LookAt(target);
        //cam.transform.rotation = transform.rotation;
    }

    private void Update()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, target.transform.position + offset, lerpPerc);
    }
}
