using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //only be in this state for a set amount of time
    //ONLY AFTER being in another state
    public class TimeCondition : ICondition
    {
        //constructor
        public TimeCondition(float t)
        {
            maxTime = t;
        }

        //variables
        public float maxTime;
        private float count = 0f;

        //time needs to be within a certain amount???
        public override bool Check()
        {
            if (count < maxTime)
            {
                //increment time
                count += Time.deltaTime;

                return true;
            }

            return false;
        }
    }
}
