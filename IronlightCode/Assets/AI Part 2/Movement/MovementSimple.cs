using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSimple : MonoBehaviour
{
    public Transform parent;
    public float speedMove, speedRotation;

    
    private float movement, rotation;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Vertical"))
        {
            movement = Input.GetAxis("Vertical") * speedMove * Time.deltaTime;
        }
        else
        {
            movement = 0f;
        }
        
        if(Input.GetButton("Horizontal"))
        {
            rotation = Input.GetAxis("Horizontal") * speedRotation * Time.deltaTime;
        }
        else
        {
            rotation = 0f;
        }

        //move
        parent.position += parent.forward * movement;

        //rotate
        parent.eulerAngles = new Vector3(parent.eulerAngles.x,
                                         parent.eulerAngles.y + rotation,
                                         parent.eulerAngles.z);
    }
}
