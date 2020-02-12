using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tDanish_Move_Component
{
    TestDanish_Controller_StateManager_v1 manager;

    private Vector3 tMoveDirection = Vector3.zero;
    private Vector3 tMoveOffset = Vector3.zero;
    private float tMoveSpeed = 0;

    private Rigidbody rb;

    public void Init(TestDanish_Controller_StateManager_v1 stateManager)
    {
        manager = stateManager;
        tMoveSpeed = stateManager.moveSpeed;
        rb = stateManager.rigid;
    }

    public void Tick()
    {
        UpdateValues(manager.moveVector);

        ForwardMovement(tMoveDirection);
    }

    public void UpdateValues(Vector3 direction)
    {
        tMoveDirection = direction;
        if(tMoveSpeed != manager.moveSpeed)
        {
            tMoveSpeed = manager.moveSpeed;
        }
    }

    void ForwardMovement(Vector3 direction)
    {
        Vector3 vector = new Vector3(direction.x, 0, direction.y);
        Vector3 offset = vector * tMoveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + offset;

        rb.MovePosition(newPosition);
    }
}
