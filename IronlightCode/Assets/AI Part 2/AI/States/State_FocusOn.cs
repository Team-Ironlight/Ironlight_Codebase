using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public class State_FocusOn : IState
    {
        //constructor
        public State_FocusOn(Transform parent, Transform target, float rotationSpeed)
        {
            this.parent = parent;
            this.target = target;            
            this.rotationSpeed = rotationSpeed;
        }

        //variables
        private Transform parent;
        private Transform target;
        private float rotationSpeed;

        public void Enter()
        {
            Debug.Log("Enter FocusOn");
        }

        public void Examine()
        {
            
        }

        public void Execute()
        {
            Debug.Log("Execute FocusOn");
            //rotate to face target
            parent.rotation = Quaternion.Slerp(
                                 parent.rotation,
                                 Quaternion.LookRotation(target.position - parent.position),
                                 rotationSpeed * Time.deltaTime);
        }

        public void Exit()
        {
            Debug.Log("Exit FocusOn");
        }
    }
}


