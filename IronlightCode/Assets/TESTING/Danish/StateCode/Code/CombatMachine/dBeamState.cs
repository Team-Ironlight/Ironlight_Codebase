using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharmout.attacks;

namespace Danish.StateCode
{
    public class dBeamState : dCombatBaseState
    {
        private dStateManager Manager;
        private R_BeamAttack beamComponent = null;


        public dBeamState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if(Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            beamComponent = Manager.rBeam;
            beamComponent.Init(Manager.Muzzle, Manager.pooler, Manager.beamStats); 
        }

        public override void OnEnter()
        {
            beamComponent.ResetBeam();
            beamComponent.StartBeam();
        }

        public override void OnExit()
        {
        }

        public override Type Tick()
        {
            Debug.Log("Beam State");

            beamComponent.Tick();

            if (!Manager.isAttacking)
            {
                beamComponent.EndBeam();
                return typeof(dReadyState);
            }

            return null;
        }
    }
}