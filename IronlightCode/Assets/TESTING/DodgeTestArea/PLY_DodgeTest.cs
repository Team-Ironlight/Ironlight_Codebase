using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_DodgeTest : MonoBehaviour
{
    public float timer = 3;
    public bool canDodge = true;
    public float dodgeForce = 10;
    public float speed;
    public float smoothing = 0.5f;
    public float dodgeDistance;

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
            //SlowDown();
        }

   
    }
    
    void TakeDodgeInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            Debug.Log("Input");
            PerformDodge();
        }
    }


    // When dodge input is received
        // Get player current position
        // Get direction of movement, if any
            // if not moving, dodge forward
        // normalize dodge direction
        // create a vector3 that adds the position and (the dodge direction * distance value)
                //Vector3 vector = new Vector3(transform.position.x + vector.x, transform.position.y + vector.y, transform.position.z + vector.z);
        // rb.MovePosition(vector)
        



    //It gets your direction and multiply it with the speed to dodge
    void PerformDodge()
    {
        canDodge = false;
        Debug.Log("Dodge");
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 dodgeDir = dodgeDirection.normalized;

        if(dodgeDir == Vector3.zero){
            dodgeDir = gameObject.transform.forward;
		}

        dodgeDir *= speed;

        Vector3 newPosition = new Vector3(currentPosition.x + dodgeDir.x, currentPosition.y, currentPosition.z + dodgeDir.z);

        _rb.MovePosition(newPosition);

        StartCoroutine(Wait());
       

       // canDodge = true;
        //currentSpeed = dodgeDirection * speed;
    }


       
    IEnumerator Wait() 
    {
        if(canDodge == false)
        {
            yield return new WaitForSeconds(2);
            canDodge = true;
        }
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
