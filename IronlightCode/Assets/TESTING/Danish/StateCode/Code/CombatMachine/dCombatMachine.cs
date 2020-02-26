using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Danish.StateCode
{


    public class dCombatMachine
    {
        public Dictionary<Type, dCombatBaseState> _AvailableCombatStates;


        public dCombatBaseState _currentState { get; private set; }

        public void Tick()
        {
            //Debug.Log("ticking Combat Machine");

            if (_currentState == null)
            {
                _currentState = _AvailableCombatStates.Values.First();
            }

            var nextState = _currentState.Tick();

            if (nextState != null && nextState != _currentState?.GetType())
            {
                ChangeState(nextState);
            }
        }

        void ChangeState(Type _nextState)
        {
            _currentState?.OnExit();
            _currentState = _AvailableCombatStates[_nextState];
            _currentState?.OnEnter();
        }

        public void SetStates(Dictionary<Type, dCombatBaseState> states)
        {
            _AvailableCombatStates = states;
            //Debug.Log(_AvailableCombatStates.Count);
        }
    }
}