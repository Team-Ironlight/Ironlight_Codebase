using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.AttackCode;
using Danish.Tools;

namespace Danish.StateCode
{


    public class dLaunchState : dCombatBaseState
    {
        private dStateManager Manager;
        private dOrbAttack orbAttack;
        private dObjectPooler Pooler;

        public dLaunchState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if (Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            orbAttack = new dOrbAttack();

            Pooler = Manager.pooler;
        }


        public override void OnEnter()
        {
            orbAttack.Init(Manager);
        }

        public override void OnExit()
        {
        }

        public override Type Tick()
        {
            Debug.Log("Launch  State");

            if (!Manager.isAttacking)
            {
                return typeof(dReadyState);
            }

            orbAttack.Shoot(Pooler);

            return null;
        }
    }
}