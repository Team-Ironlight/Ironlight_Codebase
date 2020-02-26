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
            blastComponent.Init();
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override Type Tick()
        {
            Debug.Log("Blast State");

            if (!Manager.launchBlast)
            {
                return typeof(dReadyState);
            }


            return null;
        }
    }
}