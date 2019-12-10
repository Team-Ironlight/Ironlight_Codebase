using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
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
    PlayerStateMachine machine;
    PlayerStats stats;
    HealthComponent health;



    // Initialize State Manager by connecting to necessary functions
    public void Init()
    {
        // Get Components and assign to appropriate variables
        machine = GetComponent<PlayerStateMachine>();
        stats = GetComponent<PlayerStats>();

        // Add other necessary components
        gameObject.AddComponent<HealthComponent>();

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
    }



    // Send a dictionary of usable states to the StateMachine
    void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(IdleState), new IdleState(state:this) },
            {typeof(MoveState), new MoveState(state:this) },
            {typeof(AttackState), new AttackState(state:this) },
            {typeof(DodgeState), new DodgeState(state:this) }
        };

        machine.SetStates(states);
    }

    void InitializeHealthComponent()
    {
        health.Init(stats.maxHealthValue, stats.defenseValue);
    }

    #endregion
}
