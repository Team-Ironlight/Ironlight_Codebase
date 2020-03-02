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
            beamComponent.Init(Manager.Muzzle, Manager.pooler); 
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override Type Tick()
        {
            Debug.Log("Beam State");

            if (!Manager.launchBeam)
            {
                return typeof(dReadyState);
            }

            return null;
        }
    }
}