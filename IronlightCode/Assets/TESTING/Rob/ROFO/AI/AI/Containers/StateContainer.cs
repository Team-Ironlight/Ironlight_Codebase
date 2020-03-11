using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    //hold a list of states
    //Loop through all states and run each of their relevant methods
    public class StateContainer
    {
        //constructor
        public StateContainer(string name)
        {
            this.name = name;
        }

        //might be more reliable
        public StateContainer(string name, 
                              IState[] states,
                              ConditionContainer[] transitionConditions,
                              string[] transitionStateNames)
        {
            this.name = name;
            this.states = states;
            this.transitionConditions = transitionConditions;
            this.transitionStateNames = transitionStateNames;
        }

        //name of container needed for dictionary key
        public string name;

        //states contained in this object
        public IState[] states;

        //each of these can/will be unique, so need
        public ConditionContainer[] transitionConditions;

        //names of stateContainers returned by condition containers for dictionary key
        public string[] transitionStateNames;        



        //methods
        //iterate through list and call enter on each state
        public void CallEnter()
        {
            for (int i = 0; i < states.Length; i++)
            {
                states[i].Enter();
            }
        }

        public void CallExit()
        {
            for (int i = 0; i < states.Length; i++)
            {
                states[i].Exit();
            }
        }

        public void CallExecute()
        {
            for (int i = 0; i < states.Length; i++)
            {
                states[i].Execute();
            }
        }
    }
}

