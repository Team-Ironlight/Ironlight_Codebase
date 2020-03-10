using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    //so simple so clean
    public class StateMachine_StateContainers : MonoBehaviour
    {
        public Dictionary<string, StateContainer> allStateContainers = new Dictionary<string, StateContainer>();

        public string startState;
        public string thisState;
        public string GetState()
        {
            return thisState;
        }
        private bool isOn = false;
        public void SetIsOn(bool b)
        {
            isOn = b;

            if(isOn)
            {
                if(notify != null)
                {
                    notify();
                }                
            }
        }

        //create an event to notify observers
        public delegate void Notify();
        public static event Notify notify;

        private void Update()
        {
            if(isOn)
            {
                Run();
            }
        }

        //simple state machine
        private void Run()
        {
            string tempState = CheckState(thisState);

            if(tempState != thisState)
            {
                allStateContainers[thisState].CallExit();
                thisState = tempState;
                allStateContainers[thisState].CallEnter();
            }
            else
            {
                allStateContainers[thisState].CallExecute();
            }
        }

        //check conditionContainers attached to the current StateContainer
        //if returns true, new stateContainer is equal to the stateContainer parrallel to conditionContainer
        private string CheckState(string state)
        {
            ConditionContainer[] cc = allStateContainers[thisState].transitionConditions;
            string[] names = allStateContainers[thisState].transitionStateNames;
            for (int i = 0; i < cc.Length; i++)
            {
                if(cc[i].CheckConditions())
                {
                    return names[i];
                }
            }

            return state;
        }
    }
}

