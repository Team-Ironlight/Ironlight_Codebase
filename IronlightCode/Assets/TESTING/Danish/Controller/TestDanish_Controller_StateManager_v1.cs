using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TestDanish_Controller_StateManager_v1 : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed = 0.005f;
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
