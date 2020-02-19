using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Danish.StateCode
{

    public class dTraversalMachine
    {
        public Dictionary<Type, dTraversalBaseState> _AvailableTraversalStates;


        public dTraversalBaseState _currentState { get; private set; }

        public void Tick()
        {
            Debug.Log("ticking State Machine");

            if (_currentState == null)
            {
                _currentState = _AvailableTraversalStates.Values.First();
            }

            var nextState = _currentState.Tick();

            if(nextState != null && nextState != _currentState?.GetType())
            {
                ChangeState(nextState);
            }
        }

        void ChangeState(Type _nextState)
        {
            _currentState?.OnExit();
            _currentState = _AvailableTraversalStates[_nextState];
            _currentState?.OnEnter();
        }

        public void SetStates(Dictionary<Type, dTraversalBaseState> states)
        {
            _AvailableTraversalStates = states;
            Debug.Log(_AvailableTraversalStates.Count);
        }
    }
}