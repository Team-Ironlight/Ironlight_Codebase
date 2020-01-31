using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestDanish_TraversalBaseState
{
    protected GameObject obj;
    protected Transform objTransform;

    public TestDanish_TraversalBaseState(GameObject gameObject)
    {
        this.obj = gameObject;
        this.objTransform = gameObject.transform;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract Type Tick();
}
