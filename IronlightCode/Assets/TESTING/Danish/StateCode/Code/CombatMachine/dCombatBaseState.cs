using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.StateCode
{

    public abstract class dCombatBaseState
    {
        protected dStateManager MainManager;
        protected GameObject _obj;
        protected Transform _objTransform;

        public dCombatBaseState(GameObject gameObject)
        {
            _obj = gameObject;
            _objTransform = gameObject.transform;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract Type Tick();
    }
}