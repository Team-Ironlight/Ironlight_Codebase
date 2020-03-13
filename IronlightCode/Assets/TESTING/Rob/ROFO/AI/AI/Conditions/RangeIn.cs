using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public class RangeIn : ICondition
    {
        //constructor
        public RangeIn(Transform parent, Transform target, float r)
        {
            this.parent = parent;
            this.target = target;

            range = r;
        }

        //variables
        public Transform parent;
        private Transform target;
        private float range = 0;


        public bool Check()
        {
            //Debug.Log("Range In: " + (target.position - parent.position).magnitude);
            if ((target.position - parent.position).magnitude < range)
            {
                //return the state for that range
                return true;
            }

            return false;
        }
    }
}


