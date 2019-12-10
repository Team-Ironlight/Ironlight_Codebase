using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
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
    PlayerStateMachine machine;
    PlayerStats stats;
    public HealthComponent health;
    public MovementComponent movement;




    // Initialize State Manager by connecting to necessary functions
    public void Init()
    {
        // Get Components and assign to appropriate variables
        machine = GetComponent<PlayerStateMachine>();
        stats = GetComponent<PlayerStats>();
        health = GetComponent<HealthComponent>();
        movement = GetComponent<MovementComponent>();
        

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

        _rb.velocity = movement.moveDir * (stats.moveSpeed * movement.moveAmount);
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

        if (_rb)
            Debug.Log("You connected nigga");
    }



    // Send a dictionary of usable states to the StateMachine
    void InitializeStateMachine()
    {
        // Create a dictionary using the TYPE of a state that inherits BaseState
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(IdleState), new IdleState(state:this) },
            {typeof(MoveState), new MoveState(state:this) },
            {typeof(AttackState), new AttackState(state:this) },
            {typeof(DodgeState), new DodgeState(state:this) }
        };

        // Cache the dictionary in a local variable in the PlayerStateMachine Component
        machine.SetStates(states);
    }

    void InitializeHealthComponent()
    {
        // Initialize health component using corresponding values from PlayerStats script
        health.Init(stats.maxHealthValue, stats.defenseValue);
    }

    #endregion
}
