using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTest : MonoBehaviour
{
    public bool inputReceived = false;


    private void Update()
    {
        GetInput();
        if (inputReceived)
            Shoot();
    }

    void GetInput()
    {
        // Change this depending on how you want the attack to work
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputReceived = true;
        }
        else
        {
            inputReceived = false;
        }
    }

    // Code to perform attack
    void Shoot()
    {
        Debug.Log("Bitches");
    }
}
