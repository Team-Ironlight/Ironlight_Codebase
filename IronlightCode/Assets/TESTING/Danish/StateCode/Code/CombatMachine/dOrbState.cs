using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharmout.attacks;

namespace Danish.StateCode
{


    public class dOrbState : dCombatBaseState
    {
        private dStateManager Manager;
        private R_OrbAttack orbComponent;

        public dOrbState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if(Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            orbComponent = Manager.rOrb;
            orbComponent.Init(Manager.Muzzle, Manager.pooler, Manager.orbStats);

        }


        public override void OnEnter()
        {
            orbComponent.Shoot();
        }

        public override void OnExit()
        {
            orbComponent.ResetOrb();
        }

        public override Type Tick()
        {
            Debug.Log("Fired Orb");
            return typeof(dReadyState);
        }
    }
}