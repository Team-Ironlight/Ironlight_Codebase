using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharmout.attacks;
using System;

namespace Danish.StateCode
{
    public class dBlastState : dCombatBaseState
    {
        private dStateManager Manager;
        private R_BlastAttack blastComponent = null;


        public dBlastState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if(Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            blastComponent = Manager.rBlast;
            blastComponent.Init(Manager.Muzzle, Manager.pooler);
        }

        public override void OnEnter()
        {
            blastComponent.StartBlast();
        }

        public override void OnExit()
        {
            blastComponent.ResetBlast();
        }

        public override Type Tick()
        {
            Debug.Log("Blast State");

            blastComponent.Tick();

            if (!Manager.launchBlast)
            {
                blastComponent.Launch();
                return typeof(dReadyState);
            }


            return null;
        }
    }
}