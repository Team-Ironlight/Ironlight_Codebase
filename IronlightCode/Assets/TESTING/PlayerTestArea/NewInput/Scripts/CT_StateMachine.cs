using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CT_StateMachine : MonoBehaviour
{
    public Dictionary<Type, CT_BaseState> _states;

    public CT_BaseState CurrentState { get; private set; }
    // public event Action<BaseState> OnStateChanged;

    void Update()
    {
        // Initialize CurrentState to the first state in the dictionary 
        if (CurrentState == null)
        {
            CurrentState = _states.Values.First();
        }

        // Run the code in the Tick function of the active state and assign the returned state to nextState
        var nextState = CurrentState?.Tick();

        // If nextState is not NULL and is different from the CurrentState...
        if (nextState != null && nextState != CurrentState?.GetType())
        {
            // Use function to switch to new state
            SwitchToNewState(nextState);
        }


    }


    // Assign the Dictionary of States from StateManager
    public void SetStates(Dictionary<Type, CT_BaseState> states)
    {
        _states = states;
    }

    // When run, set CurrentState to the state in the Dictionary with the same returned Type
    private void SwitchToNewState(Type _nextState)
    {
        CurrentState?.OnExit();
        CurrentState = _states[_nextState];
        CurrentState?.OnEnter();
    }
}
