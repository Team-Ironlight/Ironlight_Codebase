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

        float count = 0;
        float maxTime = 1;

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
            count = 0;
            //orbComponent.ResetOrb();
        }

        public override Type Tick()
        {
            Debug.Log("Fired Orb");


            if (CoolDown())
            {
                
                return typeof(dReadyState);
            }


            return null;
        }

        private bool CoolDown()
        {
            count += Time.deltaTime;

            if (count >= maxTime)
            {
                return true;
            }

            return false;
        }
    }
}