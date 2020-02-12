using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tDanish_Jump_Component : MonoBehaviour
{
    TestDanish_Controller_StateManager_v1 manager;

    private Vector2 tMoveDirection = Vector2.zero;
    private Vector3 tJumpDirection = Vector3.zero;
    private Vector3 tJumpOffset = Vector3.zero;
    private float tJumpHeight = 0;

    private bool onGround = false;
    private bool isRising = false;
    private bool isFalling = false;

    private Rigidbody rb;

    public void Init(TestDanish_Controller_StateManager_v1 stateManager)
    {
        manager = stateManager;
        tJumpHeight = stateManager.jumpHeight;
        rb = stateManager.rigid;
    }

    public void StartJump()
    {
        UpdateValues();
        DefineValues();
        
    }

    public void UpdateValues()
    {
        tJumpDirection = manager.moveVector;
        if (tJumpHeight != manager.jumpHeight)
        {
            tJumpHeight = manager.jumpHeight;
        }
    }

    void DefineValues()
    {
        tJumpDirection = new Vector3(tMoveDirection.x, 1, tMoveDirection.y);
        tJumpOffset = tJumpDirection * tJumpHeight * Time.deltaTime;
    }


    public void Jump()
    {
        Vector3 newPos = rb.position + tJumpOffset;
        rb.MovePosition(newPos);

        //onGround = false;
        //isRising = true;
    }

}
