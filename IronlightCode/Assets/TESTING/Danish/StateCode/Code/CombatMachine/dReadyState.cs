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
            Debug.Log("Ready To Attack");

            if (Manager.launchOrb)
            {
                Manager.launchOrb = false;
                return typeof(dOrbState);
            }
            else if (Manager.launchBeam)
            {
                return typeof(dBeamState);
            }
            else if (Manager.launchBlast)
            {
                return typeof(dBlastState);
            }

            return null;
        }
    }
}