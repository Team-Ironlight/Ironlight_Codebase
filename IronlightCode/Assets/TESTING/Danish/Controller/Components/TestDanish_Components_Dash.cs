using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_Components_Dash
{
    TestDanish_Controller_StateManager_v1 stateManager;

    public void Init(TestDanish_Controller_StateManager_v1 _manager)
    {
        stateManager = _manager;
    }

    public void PerformDash(Vector2 direction)
    {
        DashForward();
    }


    void DashForward()
    {
        RaycastHit hit;
        Vector3 playerForward = stateManager.playerObject.transform.forward;
        Vector3 playerPosition = stateManager.playerObject.transform.position;

        Vector3 targetPosition = playerPosition + ((playerForward * stateManager.dashDistance));

        if(Physics.Linecast(playerPosition, targetPosition, out hit))
        {
            targetPosition = playerPosition + (playerForward * (hit.distance - 0.05f));
        }

        if(Physics.Raycast(targetPosition, -Vector3.up, out hit))
        {
            targetPosition = hit.point;
            targetPosition.y = 0.15f;
            stateManager.playerObject.transform.position = targetPosition;
            stateManager.gameObject.GetComponent<TestDanish_Controller_InputHandler_v1>().isDashing = false;
        }
    }


}
