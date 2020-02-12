using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Iman_StateMachine : MonoBehaviour
{
    public Dictionary<Type, ImanBaseState> _states;

    public ImanBaseState currentState { get; private set; }

    private void Update()
    {
        if (currentState == null)
        {
            currentState = _states.Values.First();
        }

        var nextState = currentState.Tick();

        if (nextState != null && nextState != currentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }


    public void SwitchToNewState(Type _nextState)
    {
        currentState?.OnExit();
        currentState = _states[_nextState];
        currentState?.OnEnter();
    }


    public void SetStates(Dictionary<Type, ImanBaseState> states)
    {
        _states = states;
    }
}
