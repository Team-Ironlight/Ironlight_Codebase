using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PLY_BaseState
{ 
    protected GameObject obj;
    protected Transform transform;

    // State Constructor
    public PLY_BaseState(GameObject gameObject)
    {
        this.obj = gameObject;
        this.transform = gameObject.transform;
    }

    // Update function
    public abstract Type Tick();
    // Setup Code run for when player switches into the state
    public abstract void OnEnter();
    // Code to run when Player exits the state
    public abstract void OnExit();
}
