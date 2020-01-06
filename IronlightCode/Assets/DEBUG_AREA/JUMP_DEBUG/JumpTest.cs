using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{

    public Rigidbody rb;
    public float jumpForce = 10;
    [SerializeField]
    bool isGrounded;
    bool jumping;
    int jumpCount;
    float hor;
    float vert;
    [SerializeField]
    Vector3 moveDirection;
    void Start()
    {
        isGrounded = false;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        if (jumping)
        {
            jump();
        }
    }
    void jump()
    {
        rb.AddForce(transform.up * jumpForce +moveDirection, ForceMode.Impulse) ;
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumping = false;
            jumpCount = 0;
        }
      
    }
    void getInput()
    {
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        moveDirection = new Vector3(hor, 0, vert);
        if (Input.GetKeyDown(KeyCode.Space)&& jumpCount == 0)
        {
            jumping = true;
            jumpCount++;
        }
        else
        {
            jumping = false;
        }
    }
}
