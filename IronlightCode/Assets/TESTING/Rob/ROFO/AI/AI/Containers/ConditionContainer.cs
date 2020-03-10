using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    //holds an array of IConditions
    //has functionality to search through its conditions and determine if true or false
    public class ConditionContainer
    {
        //constructor
        public ConditionContainer(ICondition[] conditions)
        {
            this.conditions = conditions;
        }

        //public IState state;
        private ICondition[] conditions;


        //method to search through conditions and return true or false
        public bool CheckConditions()
        {            
            for (int i = 0; i < conditions.Length; i++)
            {
                //Debug.Log("Condition " + (i + 1));
                if(conditions[i].Check() == false)
                {
                    //Debug.Log("Condition False");
                    return false;
                }
            }

            //Debug.Log("Condition True");
            return true;
        }
    }
}


