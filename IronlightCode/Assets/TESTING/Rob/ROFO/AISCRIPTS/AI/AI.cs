using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class AI : MonoBehaviour
    {
        //public State defaultState = State.None;
        private IState defaultState;
        public IState lastState;

        private ConditionContainer[] container;
        public ConditionContainer[] GetContainer()
        {
            return container;
        }

        private bool isReady = false;
        public bool GetisReady()
        {
            return isReady;
        }


        private void Start()
        {
            defaultState = new None();
            container = CreateContainer(gameObject);
        }


        //state machine will call this to get a next state
        public IState CheckContainer(ConditionContainer[] cc)
        {
            if (cc == null)
            {
                lastState = defaultState;
                return lastState;
            }

            //Debug.Log("Container Length: " + cc.Length);
            for (int i = 0; i < cc.Length; i++)
            {
                bool all = true;
                for (int j = 0; j < cc[i].conditions.Length; j++)
                {
                    //if a single condition in the conditoin containe is false, break
                    if (cc[i].conditions[j].Check() == false)
                    {
                        all = false;
                        break;
                    }
                }

                //if all conditions pass than return state
                if (all)
                {
                    //Debug.Log("Returning: " + cc[i].name);
                    lastState = cc[i].GetState();
                    return lastState;
                }
            }

            //if no conditions check out return default state
            Debug.Log("Nothing True, returning none");
            lastState = defaultState;
            return lastState;
        }


        //get all instances of conditionContainer on gameobject
        private ConditionContainer[] CreateContainer(GameObject g)
        {
            ConditionContainer[] temp = g.GetComponents<ConditionContainer>();
            temp = SortContainer(temp);

            return temp;
        }


        //sort the container passed in based on rank
        private ConditionContainer[] SortContainer(ConditionContainer[] cc)
        {
            //new sort
            ConditionContainer[] temp = new ConditionContainer[cc.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[cc[i].rank] = cc[i];
            }

            //debug the order with rank
            for (int i = 0; i < temp.Length; i++)
            {
                Debug.Log("<color=blue>Return: </color>" + temp[i].returnState + " Rank: " + temp[i].rank);
            }

            return temp;
        }
    }
}
