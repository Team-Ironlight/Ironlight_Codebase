using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    public class StateMachineTemplate : MonoBehaviour
    {
        [Header("Default State")]
        public SO_StateContainer startState;

        public int numberOfContainers;
        public SO_StateContainer[] stateContainers;

        //transforms
        private Transform parent;
        private Transform target;


        private void Start()
        {
            Setup();
        }        

        private void Setup()
        {
            parent = TransformFinder.GetParent(transform);
            target = GameObject.Find("Player").transform;

            //instantiate StateMachine Script
            StateMachine_StateContainers stateMachine = gameObject.AddComponent<StateMachine_StateContainers>();

            stateMachine.startState = startState.name;
            stateMachine.thisState = stateMachine.startState;

            //create StateContainers
            for (int i = 0; i < numberOfContainers; i++)
            {
                StateContainer temp = stateContainers[i].CreateStateContainer(parent, target);
                stateMachine.allStateContainers.Add(temp.name, temp);
                //Debug.Log("Name: " + temp.name);
            }

            //get rid of template
            stateMachine.SetIsOn(true);
            Destroy(this);
        }
    }
}


