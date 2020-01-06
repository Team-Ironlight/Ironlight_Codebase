using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTest : MonoBehaviour
{
    public float timer = 3;
    public bool canDodge = true;
    public float dodgeForce = 10;
    public float speed;
    public float smoothing = 0.5f;

    public float vertical;
    public float horizontal;
    public Vector3 dodgeDirection;
    public Vector3 currentSpeed;

    public Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        dodgeDirection = new Vector3(horizontal, 0, vertical);

        TakeDodgeInput();


        if (!canDodge)
        {
            SlowDown();
        }
    }
    
    void TakeDodgeInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDodge)
        {
            Debug.Log("Input");
            PerformDodge();
        }
    }

    //It gets your direction and multiply it with the speed to dodge
    void PerformDodge()
    {
        currentSpeed = dodgeDirection * speed;
        _rb.velocity = currentSpeed;
        canDodge = false;
        Debug.Log("Dodge");
    }

    void SlowDown()
    {
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, smoothing);
        if(_rb.velocity == Vector3.zero)
        {
            canDodge = true;
        }
    }
}
