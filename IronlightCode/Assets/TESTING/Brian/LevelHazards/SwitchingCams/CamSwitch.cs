using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public Camera Cam;
    public GameObject DefaultCamPos;
    public GameObject aimCamPos;
    public GameObject player;
    public float smooth;
    // Start is called before the first frame update
    float value;

    // Update is called once per frame
    void Update()
    {
        Cam.transform.position = DefaultCamPos.transform.position;
        Cam.transform.LookAt(player.transform);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cam.transform.Translate(aimCamPos.transform.position * Time.deltaTime * smooth);
            Cam.transform.LookAt(player.transform);
        }
        //if (Input.GetKeyUp(KeyCode.Mouse1))
        //{
        //    Cam.transform.Translate(DefaultCamPos.transform.position);
        //}
    }
}
