using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TestDanish_Controller_InputHandler_v1 : MonoBehaviour
{
    TestDanish_Controller_Input controls;


    public bool isMoving = false;


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


    private void OnEnable()
    {
        controls.Enable();

        controls.Traversal.Movement.performed += Movement_performed;

        controls.Traversal.Jump.started += Jump_started;
        controls.Traversal.Jump.performed += Jump_performed;
        controls.Traversal.Jump.canceled += Jump_canceled;

        controls.Traversal.Dash.started += Dash_started;
        controls.Traversal.Dash.performed += Dash_performed;
    }

    private void Dash_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Dash Started");
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Dash Performed");
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
        if(ctx.interaction is PressInteraction)
        {
            isMoving = !isMoving;
        }

        if (isMoving)
        {
            Debug.Log("Movement input received");
        }
        else
        {
            Debug.Log("Movement input canceled");
        }

    }
    

    private void OnDisable()
    {
        controls.Traversal.Movement.performed -= Movement_performed;

        controls.Traversal.Jump.started -= Jump_started;
        controls.Traversal.Jump.performed -= Jump_performed;
        controls.Traversal.Jump.canceled -= Jump_canceled;

        controls.Traversal.Dash.started -= Dash_started;
        controls.Traversal.Dash.performed -= Dash_performed;


        controls.Disable();
    }
}
