using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CT_BaseState
{
    protected GameObject obj;
    protected Transform objTrans;

    // State Constructor
    public CT_BaseState(GameObject gameObject)
    {
        this.obj = gameObject;
        this.objTrans = gameObject.transform;
    }

    // Update function
    public abstract Type Tick();
    // Setup Code run for when player switches into the state
    public abstract void OnEnter();
    // Code to run when Player exits the state
    public abstract void OnExit();
}
