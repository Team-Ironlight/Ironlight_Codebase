using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    //Could turn this into a scripable object eventually
    public class AnimatorStateMachine : MonoBehaviour
    {
        public Animator anim;
        public int numberOfAnimations;
        public SO_StateContainer[] states = new SO_StateContainer[1];
        public bool[] bools = new bool[1];
        public string[] animatorVariables = new string[1];

        //statemachine
        public GameObject stateMachine;
        private StateMachine_StateContainers sm;
        private string previousState = null;

        //custom struct to hold this info
        public struct Pair
        {
            public Pair(string s, bool b)
            {
                varName = s;
                value = b;
            }

            public string varName;
            public bool value;
        }


        //stores the name of the state as the key
        public Dictionary<string, Pair> animationState = new Dictionary<string, Pair>();

        private void Start()
        {
            //set all the information
            for (int i = 0; i < numberOfAnimations; i++)
            {
                Pair p = new Pair(animatorVariables[i], bools[i]);
                animationState.Add(states[i].name, p);
            }            
        }

        private void OnEnable()
        {
            StateMachine_StateContainers.notify += Setup;
        }

        private void Setup()
        {
            sm = stateMachine.GetComponent<StateMachine_StateContainers>();
            previousState = sm.GetState();
            StateMachine_StateContainers.notify -= Setup;
        }

        private void Update()
        {
            CheckState();
        }

        private void CheckState()
        {
            //check current state of statemachine, if not the same as last frame
            //update previousState and set correct bool from dictionary
            if (previousState != sm.GetState())
            {
                Debug.Log("<color=Red>Change Animation: </color>" + previousState);
                previousState = sm.GetState();
                Pair temp = animationState[previousState];
                                
                anim.SetBool(temp.varName, temp.value);
            }
        }



        //cycle through parameters (bools for now) and set param true, others false
        public void AnimSetBool(string paramName, bool b)
        {
            //Debug.Log("Param count: " + anim.parameterCount);
            for (int i = 0; i < anim.parameterCount; i++)
            {
                //check if param is bool            
                if (anim.parameters[i].type.ToString() == "Bool")
                {
                    if (anim.parameters[i].name == paramName)
                    {
                        //Debug.Log("Param name: " + anim.parameters[i].name + " " + true);
                        anim.SetBool(anim.parameters[i].name, true);
                    }
                    else
                    {
                        //Debug.Log("Param name: " + anim.parameters[i].name + " " + false);
                        anim.SetBool(anim.parameters[i].name, false);
                    }
                }
            }
        }

    }
}


