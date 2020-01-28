using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_Components_Movement_v1
{
    TestDanish_Controller_StateManager_v1 stateManager;

    public void Init(TestDanish_Controller_StateManager_v1 manager)
    {
        stateManager = manager;
        Debug.Log("added movement component");
    }

    public void MoveObject(Vector2 _moveVector)
    {
        GetRotation();
        MoveForward(_moveVector.y);
        Strafe(_moveVector.x);

        //Vector3 moveAmount = stateManager.playerObject.transform.forward;

        //moveAmount.x += stateManager.moveVector.x;
        //moveAmount.z += stateManager.moveVector.y;

        //moveAmount *= stateManager.moveSpeed;



        //stateManager.playerObject.transform.position += moveAmount;
    }

    void MoveForward(float vectorY)
    {
        Vector3 playerForward = stateManager.playerObject.transform.forward;
        Vector3 playerPosition = stateManager.playerObject.transform.position;

        Vector3 targetPosition = playerPosition + ((playerForward * stateManager.moveSpeed) * vectorY);


        stateManager.playerObject.transform.position = targetPosition;
    }

    void Strafe(float vectorX)
    {
        Vector3 playerRight = stateManager.playerObject.transform.right;
        Vector3 playerPosition = stateManager.playerObject.transform.position;

        Vector3 targetPosition = playerPosition + ((playerRight * stateManager.moveSpeed) * vectorX);

        stateManager.playerObject.transform.position = targetPosition;
    }

    void GetRotation()
    {
        Vector3 cameraForward = stateManager.playerCamera.transform.forward;
        Vector3 cameraPosition = stateManager.playerCamera.transform.position;

        Vector3 targetPosition = cameraPosition + (cameraForward * 5);

        stateManager.playerObject.transform.LookAt(targetPosition);

        stateManager.playerObject.transform.localRotation = Quaternion.Euler(new Vector3(0, stateManager.playerObject.transform.eulerAngles.y, 0));
    }
}
