using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public abstract class IState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Execute(Transform t);

        public abstract string Name();
    }
}








