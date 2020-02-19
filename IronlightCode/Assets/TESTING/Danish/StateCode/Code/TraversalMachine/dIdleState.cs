using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Danish.StateCode
{

    public class dIdleState : dTraversalBaseState
    {

        private dStateManager Manager;

        public dIdleState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if (Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }
        }


        public override void OnEnter()
        {
            Debug.Log("Entering Idle State");
        }

        public override void OnExit()
        {
            Debug.Log("Exiting Idle State");
        }

        public override Type Tick()
        {
            Debug.Log("In Idle State");

            if(Manager.moveVector != Vector2.zero)
            {
                return typeof(dMoveState);
            }


            return null;
        }
    }
}