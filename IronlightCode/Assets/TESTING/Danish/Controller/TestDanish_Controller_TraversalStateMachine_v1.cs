using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestDanish_Controller_TraversalStateMachine_v1 : MonoBehaviour
{
    public Dictionary<Type, TestDanish_TraversalBaseState> _states;

    public TestDanish_TraversalBaseState currentState { get; private set; }


    private void Update()
    {
        if (currentState == null)
        {
            currentState = _states.Values.First();
        }

        var nextState = currentState.Tick();

        //currentState.Tick();

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


    public void SetStates(Dictionary<Type, TestDanish_TraversalBaseState> states)
    {
        _states = states;
    }
}
