using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_RadialTest : MonoBehaviour
{
    public bool inputReceived = false;
    public float radius;
    public float radiusMax = 1f;
    public float radiusSpeed = 1f;

   
    public bool isPressed = false;

    public float timer = 5.0f;
    private float countdown = 0.01f;

    public enum modes { off, pulse, hold, charge }
    public modes currentMode = modes.pulse; 

    private void Update()
    {
        //GetInput();
        //if (inputReceived)
        Shoot();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentMode = modes.pulse;
            Debug.Log("Pulse Mode Active");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentMode = modes.hold;
            Debug.Log("Hold Mode Active");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentMode = modes.charge;
            Debug.Log("Charge Mode Active");
        }
    }

    //void GetInput()
    //{
    //    // Change this depending on how you want the attack to work
        
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        inputReceived = true;
    //    }
    //    else
    //    {
    //        inputReceived = false;
    //        radius = 0.01f;
    //    }
    //}

    IEnumerator RadialAction(float x)
    {
        Debug.Log("PULSE");
        if(x < 0)
        {
            x = 1f;
        }

        float count = 0.0f;
        while(count < x)
        {
            radius = count;
            count += Time.deltaTime;

            yield return null;
        }

        radius = 0.01f;
        coroutineOn = false;
    }

    Coroutine currentCoroutine = null;
    bool coroutineOn = false;
    float chargeCount = 0f;

    private void Pulse()
    {
        if (Input.GetKeyUp(KeyCode.Space) && coroutineOn == false)
        {
            coroutineOn = true;
            currentCoroutine = StartCoroutine(RadialAction(radiusMax));
        }
    }

    public void TestPulse()
    {
        coroutineOn = true;
        currentCoroutine = StartCoroutine(RadialAction(radiusMax));
    }

    private void Hold()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (radius < radiusMax)
            {
                radius += radiusSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            radius = 0.01f;
        }
    }

    private void Charge()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (chargeCount < radiusMax)
            {
                chargeCount += radiusSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            currentCoroutine = StartCoroutine(RadialAction(chargeCount));
        }
    }

    // Code to perform attack
    public void Shoot()
    {
        Debug.Log("CurrentMode: " + currentMode.ToString());
        if(currentMode == modes.pulse)
        {
            Pulse();
        }
        else if(currentMode == modes.hold)
        {
            Hold();
        }
        else if(currentMode == modes.charge)
        {
            Charge();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
