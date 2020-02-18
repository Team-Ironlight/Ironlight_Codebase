using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.StateCode
{

    public abstract class dTraversalBaseState
    {
        protected dStateManager MainManager;
        protected GameObject _obj;
        protected Transform _objTransform;

        public dTraversalBaseState(GameObject gameObject)
        {
            _obj = gameObject;
            _objTransform = gameObject.transform;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract Type Tick();
    }
}