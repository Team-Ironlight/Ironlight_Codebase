using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    public class TimeMax : ICondition
    {
        ////constructor
        public TimeMax(float t)
        {
            maxTime = t;
        }

        //variables
        public float maxTime;
        private float count = 0f;

        //time needs to be within a certain amount???
        public bool Check()
        {
            if (count < maxTime)
            {
                //increment time
                count += Time.deltaTime;
                return true;
            }
            else
            {
                count = 0f;
                return false;
            }
        }
    }
}


