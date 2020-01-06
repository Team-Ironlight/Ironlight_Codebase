using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    float ver;
    float hor;
    bool jumpInput = false;
    public Rigidbody rb;
    bool isRising;
    bool isFalling;
    public Vector3 moveDir;
    public float jumpHeight = 80;
    Vector3 jumpVelocity;
    Vector3 fallVelocity;
    public float fallSpeed = 18;
    public float jumpForce = 200;
    Vector3 currPos;
    // Start is called before the first frame update
    void Start()
    {
        isRising = false;
        isFalling = false;
        jumpVelocity.y = jumpForce;
        fallVelocity.y = fallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currPos = rb.position;
        GetInput();

        moveDir = new Vector3(hor, 0, ver);

        if (jumpInput)
        {
            isRising = true;
            if (isRising && rb.position.y <= jumpHeight)
            {
                Jump();
                if (rb.position.y >= jumpHeight)
                {
                    isRising = false;

                    isFalling = true;
                }

            }
            if (isFalling)
            {
                Fall();
                if (rb.position.y <= 1)
                {
                    print("landed");
                    isFalling = false;
                    jumpInput = false;
                    currPos.y = 1;
                }
            }
        }

    }

    void Jump()
    {
        Debug.Log("I'm Jumping bitches");

        rb.position += jumpVelocity * Time.deltaTime;

    }
    void Fall()
    {
        rb.position -= fallVelocity * Time.deltaTime;
    }

    void GetInput()
    {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
        }
       
    }
}
