using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.StateCode
{

    public class dReadyState : dCombatBaseState
    {
        private dStateManager Manager;

        public dReadyState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if (Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            
        }


        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override Type Tick()
        {
            Debug.Log("Ready State");

            if (Manager.isAttacking)
            {
                return typeof(dLaunchState);
            }

            return null;
        }
    }
}