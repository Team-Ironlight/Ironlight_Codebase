using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    float ver;
    float hor;
    bool jumpInput = false;


    public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        moveDir = new Vector3(hor, 0, ver);

        if (jumpInput)
        {
            Jump();
        }
    }
    
    void Jump()
    {
        Debug.Log("I'm Jumping bitches");
    }

    void GetInput()
    {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }
    }
}
