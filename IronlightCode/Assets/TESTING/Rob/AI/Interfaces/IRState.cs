using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IRState
{    
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Execute(Transform t);

    public abstract string Name();
}








