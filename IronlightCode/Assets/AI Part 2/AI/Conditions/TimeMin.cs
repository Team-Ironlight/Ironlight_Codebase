using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public class TimeMin : ICondition
    {
        //constructor
        public TimeMin(float t)
        {
            minTime = t;
        }

        //variables
        public float minTime;
        private float count = 0f;

        //time needs to be within a certain amount???
        public bool Check()
        {
            if (count < minTime)
            {
                //increment time
                count += Time.deltaTime;
                return false;
            }
            else
            {
                count = 0f;
                return true;
            }            
        }
    }
}


