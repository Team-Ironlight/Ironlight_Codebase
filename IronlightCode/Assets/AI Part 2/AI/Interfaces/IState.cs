using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Execute();
        void Examine();
    }
}


