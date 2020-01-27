using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TestDanish_Controller_StateManager_v1 : MonoBehaviour
{
    [Header("Movement Variables")]
    public Vector2 moveVector;
    public bool isMoving = false;

    [Header("Dash Variables")]
    public Vector2 dodgeVector;
    public bool isDashing = false;

    [Header("Jump Variables")]
    public bool jump = false;

    public string currentState = " ";

    public GameObject playerObject;
    public GameObject modelHolder;
    public Rigidbody rigid;
    public TestDanish_Controller_StateMachine_v1 machine;
    TestDanish_Controller_Input controls;


    public void Init()
    {
        machine = GetComponent<TestDanish_Controller_StateMachine_v1>();

        controls = new TestDanish_Controller_Input();

        InitializeStateMachine();
    }

    public void Tick()
    {
        
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

        dodgeVector = moveVector;
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
        if (ctx.interaction is HoldInteraction)
        {
            isMoving = true;
            moveVector = ctx.ReadValue<Vector2>();
        }
        else if(ctx.interaction is PressInteraction)
        {
            isMoving = false;
            moveVector = Vector2.zero;
        }

    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {

    }






    #endregion



    #region Setup Functions

    void InitializeStateMachine()
    {
        var states = new Dictionary<Type, TestDanish_Controller_BaseState_v1>()
        {
            {typeof(TestDanish_Controller_IdleState_v1), new TestDanish_Controller_IdleState_v1(state:this) },
            {typeof(TestDanish_Controller_MoveState_v1), new TestDanish_Controller_MoveState_v1(state:this) },
            {typeof(TestDanish_Controller_JumpState_v1), new TestDanish_Controller_JumpState_v1(state:this) },
            {typeof(TestDanish_Controller_DashState_v1), new TestDanish_Controller_DashState_v1(state:this) }
        };

        machine.SetStates(states);
    
    }



    #endregion
}
