using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_StateManager : MonoBehaviour
{
    // Programmer: Danish
    // Additional Programmer: 
    // Description: Manages all player components

    [Header("Movement Variables")]
    public float vertical;
    public float horizontal;

    [Header("Action Variables")]
    public bool jump;
    public bool dodge;
    public bool attack;


    public float delta;
    public GameObject activeModel;
    public Animator anim;
    public Rigidbody rigid;

    [Header("Components")]
    Rigidbody _rb;
    PLY_StateMachine machine;
    PLY_Stats stats;
    public PLY_Attributes attributes;
    public PLY_MovementComponent movement;




    // Initialize State Manager by connecting to necessary functions
    public void Init()
    {
        // Get Components and assign to appropriate variables
        machine = GetComponent<PLY_StateMachine>();
        stats = GetComponent<PLY_Stats>();
        attributes = GetComponent<PLY_Attributes>();
        movement = GetComponent<PLY_MovementComponent>();
        

        SetupAnimator();
        SetupRigidbody();

        InitializeStateMachine();
        InitializeHealthComponent();
    }


    // Code run in the Update function of InputHandler
    public void Tick()
    {

    }


    // Code run in the FixedUpdate fucntion of InputHandler
    public void FixedTick(float d)
    {
        delta = d;

        //_rb.velocity = movement.moveDir * (stats.moveSpeed * movement.moveAmount);
    }




    #region Setup Functions


    // Assign the animator of the current model to the variable
    void SetupAnimator()
    {
        Debug.Log("Setting up Animator");
    }



    // Assign the rigidbody of the current model to the variable
    void SetupRigidbody()
    {
        Debug.Log("Setting up Rigidbody");
        _rb = GetComponentInChildren<Rigidbody>();
    }



    // Send a dictionary of usable states to the StateMachine
    void InitializeStateMachine()
    {
        // Create a dictionary using the TYPE of a state that inherits BaseState
        var states = new Dictionary<Type, PLY_BaseState>()
        {
            {typeof(PLY_IdleState), new PLY_IdleState(state:this) },
            {typeof(PLY_MoveState), new PLY_MoveState(state:this) },
            {typeof(PLY_AttackState), new PLY_AttackState(state:this) },
            {typeof(PLY_DashState), new PLY_DashState(state:this) }
        };

        // Cache the dictionary in a local variable in the PlayerStateMachine Component
        machine.SetStates(states);
    }

    void InitializeHealthComponent()
    {
        // Initialize health component using corresponding values from PlayerStats script
        attributes.Init(stats.maxHealthValue, stats.maxSpiritValue);
    }

    #endregion
}
