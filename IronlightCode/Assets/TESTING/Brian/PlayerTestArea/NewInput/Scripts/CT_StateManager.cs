using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_StateManager : MonoBehaviour
{
    CT_StateMachine machine;
    Rigidbody _rb;

    public void Init()
    {
        // Get Components and assign to appropriate variables
        machine = GetComponent<CT_StateMachine>();

        //muzzle = GameObject.FindGameObjectWithTag("Muzzle");
        //muzzle.AddComponent<PLY_OrbTest>();
        //muzzle.AddComponent<PLY_BeamTest>();
        //muzzle.AddComponent<PLY_RadialTest>();


        SetupAnimator();
        SetupRigidbody();

        InitializeStateMachine();
        
    }


    // Code run in the Update function of InputHandler
    public void Tick()
    {

    }


    // Code run in the FixedUpdate fucntion of InputHandler
    public void FixedTick(float d)
    {

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
        var states = new Dictionary<Type, CT_BaseState>()
        {
            {typeof(CT_IDLE), new CT_IDLE(_state:this) }
        };

        // Cache the dictionary in a local variable in the PlayerStateMachine Component
        machine.SetStates(states);
    }

    void InitializeHealthComponent()
    {
        // Initialize health component using corresponding values from PlayerStats script
        //attributes.Init(stats.maxHealthValue, stats.maxSpiritValue);
    }

    #endregion
}
