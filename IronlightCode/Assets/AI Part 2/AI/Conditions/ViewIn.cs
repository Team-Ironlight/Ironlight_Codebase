using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    public class ViewIn : ICondition
    {
        //constructor
        public ViewIn(Transform parent, Transform target, float v)
        {
            this.parent = parent;
            this.target = target;

            dotAngle = v;
        }

        //variables
        private Transform parent;
        private Transform target;
        [Range(0f, 360f)] public float viewAngle = 30f;
        [Range(-1f, 1f)] public float dotAngle = 0f;


        public bool Check()
        {
            Vector3 vectorToTarget = target.position - parent.position;

            if (Vector3.Dot(parent.forward, vectorToTarget) > dotAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


