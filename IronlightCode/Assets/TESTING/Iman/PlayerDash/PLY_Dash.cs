using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_Dash : MonoBehaviour
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float DashCooldown;
    private float CDTimer;
    [HideInInspector] public bool InputRecievced;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();     
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();  
    }

    private void GetInput()
    {
        //input check
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            //timer check
            if (CDTimer < Time.time)
            {
                StartCoroutine(Dash());               
                InputRecievced = true;
                CDTimer = Time.time + DashCooldown;
            }
        }
        else
        {
            InputRecievced = false;
        }
    }

    IEnumerator Dash()
    {
        //add force for certain amount of time
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);
        //set velocity to 0 after dash is over
        rb.velocity = Vector3.zero;
    }
}
