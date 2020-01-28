using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TestDanish_Controller_InputHandler_v1 : MonoBehaviour
{
    TestDanish_Controller_Input controls;



    [Header("Movement Variables")]
    public Vector2 moveVector = Vector2.zero;
    public bool isMoving = false;

    [Header("Dash Variables")]
    public Vector2 dashVector = Vector2.zero;
    public bool isDashing = false;

    [Header("Jump Variables")]
    public bool jumpStart = false;



    TestDanish_Controller_StateManager_v1 stateManager;



    private void Awake()
    {
        stateManager = GetComponent<TestDanish_Controller_StateManager_v1>();
        stateManager.Init();


        controls = new TestDanish_Controller_Input();
    }

    private void Update()
    {
        stateManager.Tick();
    }

    private void FixedUpdate()
    {
        UpdateStateManager();
    }


    private void UpdateStateManager()
    {
        stateManager.moveVector = moveVector;
        stateManager.isMoving = isMoving;
        stateManager.isDashing = isDashing;
        stateManager.dodgeVector = dashVector;
        stateManager.jump = jumpStart;
    }





    #region Input Initialization








    private void OnEnable()
    {
        controls.Enable();

        controls.Traversal.Movement.performed += Movement_performed;
        controls.Traversal.Movement.canceled += Movement_canceled;

        controls.Traversal.Jump.started += Jump_started;
        controls.Traversal.Jump.performed += Jump_performed;
        controls.Traversal.Jump.canceled += Jump_canceled;

        controls.Traversal.Dash.started += Dash_started;
        controls.Traversal.Dash.performed += Dash_performed;
    }



    private void OnDisable()
    {
        controls.Traversal.Movement.performed -= Movement_performed;
        controls.Traversal.Movement.canceled -= Movement_canceled;

        controls.Traversal.Jump.started -= Jump_started;
        controls.Traversal.Jump.performed -= Jump_performed;
        controls.Traversal.Jump.canceled -= Jump_canceled;

        controls.Traversal.Dash.started -= Dash_started;
        controls.Traversal.Dash.performed -= Dash_performed;


        controls.Disable();
    }







    #endregion









    #region Input Functions







    private void Dash_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Dash Started");

        dashVector = moveVector;
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Dash Performed");
        isDashing = true;

    }

    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump Canceled Early");
    }

    private void Jump_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump Started");
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump Performed");
    }

    private void Movement_performed(InputAction.CallbackContext ctx)
    {
        // when WASD has been pressed for more than 0.01 seconds, 
        // player is moving and move vector is updated with a key pair representing 
        // vertical and horizontal input.
        if (ctx.interaction is HoldInteraction)
        {
            isMoving = true;
            moveVector = ctx.ReadValue<Vector2>();
        }
        // when WASD is released, player is no longer giving movement input and 
        // move vector is reset to wait for the next update
        else if (ctx.interaction is PressInteraction)
        {
            isMoving = false;
            moveVector = Vector2.zero;
        }

    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {

    }






    #endregion

}
