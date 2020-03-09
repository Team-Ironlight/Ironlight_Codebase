using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class SnakeRattle : IState
    {
        public string name = "SnakeRattle";

        public override string Name()
        {
            return name;
        }



        public override void Enter()
        {

        }

        public override void Execute(Transform t)
        {
            Debug.Log("Rattle");
        }

        public override void Exit()
        {

        }
    }
}
