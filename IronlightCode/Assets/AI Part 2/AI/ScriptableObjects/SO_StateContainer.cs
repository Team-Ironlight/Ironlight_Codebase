using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    [CreateAssetMenu(fileName = "StateContainer", menuName = "AI/StateContainer")]
    public class SO_StateContainer : ScriptableObject
    {
        new public string name = "name";

        //need to create these since they can have seperate info...
        public int numberOfStates = 1;
        public SO_State[] soStates;

        //match groups of conditions with groups of states
        public int numberOfTransitions = 1;
        public SO_ConditionContainer[] conditionContainers;
        public SO_StateContainer[] stateContainers;


        //create the state, get parent/target from state machine that instantiates all of these
        public StateContainer CreateStateContainer(Transform parent, Transform target)
        {
            //create the stats
            IState[] s = new IState[numberOfStates];
            for (int i = 0; i < numberOfStates; i++)
            {
                //create each state with its unique parameters
                s[i] = soStates[i].CreateState(parent, target);
                //Debug.Log("Name: " + name + " State: " + s[i].ToString());
            }

            //create the condition containers
            ConditionContainer[] c = new ConditionContainer[numberOfTransitions];
            for (int i = 0; i < c.Length; i++)
            {
                //create containers
                c[i] = conditionContainers[i].CreateConditionContainer(parent, target);
            }

            //create state container string
            string[] n = new string[numberOfTransitions];
            for (int i = 0; i < n.Length; i++)
            {
                //get the names of the statecontainers stored
                n[i] = stateContainers[i].name;
            }

            return new StateContainer(name, s, c, n);
        }
    }
}


