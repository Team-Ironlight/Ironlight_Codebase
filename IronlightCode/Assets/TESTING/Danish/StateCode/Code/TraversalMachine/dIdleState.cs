using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;
using ROFO;

namespace Danish.StateCode
{

    public class dIdleState : dTraversalBaseState
    {

        private dStateManager Manager;
        private dRotationUpdater rotationUpdater = null;
        private dCrystalTrigger crystalTrigger = null;
        private dPhysicsComponent physicsComponent = null;

        public dIdleState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if (Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            rotationUpdater = new dRotationUpdater();
            rotationUpdater.Init(Manager.objTransform, Manager.CameraHolder);

            crystalTrigger = new dCrystalTrigger();
            //crystalTrigger.Init();
            physicsComponent = Manager.dPhysics;
            physicsComponent.Init(Manager.rigidbody, 0.5f);
        }

        public override void FixedTick()
        {
            physicsComponent.FixedTick();
        }

        public override void OnEnter()
        {
            //Debug.Log("Entering Idle State");
        }

        public override void OnExit()
        {
            //Debug.Log("Exiting Idle State");
        }

        public override Type Tick()
        {
            //Debug.Log("In Idle State");

            crystalTrigger.Tick(Manager.isCrystal);
            Manager.isCrystal = false;

            if (Manager.jump)
            {
                Manager.jump = false;
                return typeof(dJumpState);
            }

            if (Manager.isDashing)
            {
                Manager.isDashing = false;
                return typeof(dDashState);
            }


            if (Manager.moveVector != Vector2.zero)
            {
                return typeof(dMoveState);
            }

            //rotationUpdater.Tick();
            physicsComponent.Tick();


            return null;
        }
    }
}