using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestDanish_Controller_BaseState_v1
{
    protected GameObject obj;
    protected Transform transform;

    // Contstructor
    public TestDanish_Controller_BaseState_v1(GameObject gameObject)
    {
        this.obj = gameObject;
        this.transform = gameObject.transform;
    }

    // Code to run when Player switches into this state
    public abstract void OnEnter();
    // Code to run when Player switches out of this state
    public abstract void OnExit();
    // State pattern representation of an Update function
    public abstract void Tick();

    // Check to see if State needs to be changed
    public abstract Type CheckForNextState();

}
