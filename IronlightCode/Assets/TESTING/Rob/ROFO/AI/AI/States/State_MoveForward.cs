using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    //simply move the object in its forward direction by a speed

    public class State_MoveForward : IState
    {
        //constructor
        public State_MoveForward(Transform parent, Transform target, float speed)
        {
            this.parent = parent;
            this.target = parent;
            this.speed = speed;
        }

        //variables
        private Transform parent;
        private Transform target;
        private float speed = 1f;



        public void Enter()
        {
            Debug.Log("Move Forward Enter");
        }

        public void Examine()
        {
            
        }

        public void Execute()
        {
            Debug.Log("Move Forward");
            parent.position += parent.forward * speed * Time.deltaTime;
        }

        public void Exit()
        {
            Debug.Log("Move Forward Enter");
        }
    }
}

