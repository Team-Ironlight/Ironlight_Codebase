using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class Rotating : IState
    {
        string name = "Rotating";

        public override string Name()
        {
            return name;
        }

        //constructor
        public Rotating(Transform t)
        {
            target = t;
        }

        public Transform target;
        public float rotationSpeed = 1f;

        public override void Enter()
        {
            Debug.Log("Enter Rotating");
        }

        public override void Execute(Transform t)
        {
            Debug.Log("Execute Rotating");
            //rotate to face target
            t.rotation = Quaternion.Slerp(
                                 t.rotation,
                                 Quaternion.LookRotation(target.position - t.position),
                                 Time.deltaTime * rotationSpeed);
        }

        public override void Exit()
        {
            Debug.Log("Exit Rotating");
        }
    }
}