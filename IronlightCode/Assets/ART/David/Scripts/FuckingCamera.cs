using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckingCamera : MonoBehaviour
{
    public float speed = 1f;
    public float speedRot = 30f;

   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            transform.position +=  Vector3.forward * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
            transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, speedRot * Time.deltaTime, 0f);
            //transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -speedRot * Time.deltaTime, 0f);
        }
    }
}
