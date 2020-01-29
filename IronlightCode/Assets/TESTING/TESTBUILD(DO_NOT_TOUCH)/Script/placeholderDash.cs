using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeholderDash : MonoBehaviour
{
    float dashDistance = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetInput();
    }


    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashForward();
        }
    }


    void DashForward()
    {
        RaycastHit hit;
        Vector3 playerForward = transform.forward;
        Vector3 playerPosition = transform.position;

        Vector3 targetPosition = playerPosition + ((playerForward * dashDistance));

        if (Physics.Linecast(playerPosition, targetPosition, out hit))
        {
            targetPosition = playerPosition + (playerForward * (hit.distance - 0.05f));
        }

        if (Physics.Raycast(targetPosition, -Vector3.up, out hit))
        {
            targetPosition = hit.point;
            targetPosition.y = 1.2f;
            transform.position = targetPosition;
            
        }
    }
}
