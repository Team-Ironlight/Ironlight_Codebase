using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialTest : MonoBehaviour
{
    public bool inputReceived = false;
    public float radius;

    public bool pulseMode = false;
    public bool holdMode = false;
    public bool chargeMode = false;


    private void Update()
    {
        GetInput();
        if (inputReceived)
            Shoot();
    }

    void GetInput()
    {
        // Change this depending on how you want the attack to work
        if (Input.GetKey(KeyCode.Space))
        {
            inputReceived = true;
        }
        else
        {
            inputReceived = false;
            radius = 0.01f;
        }
    }

    // Code to perform attack
    void Shoot()
    {
        radius += 0.5f * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
