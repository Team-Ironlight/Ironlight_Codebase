using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;

namespace Danish.StateCode
{
    public class dAimState : dTraversalBaseState
    {
        private dStateManager Manager;
        private dMoveComponent moveComponent = null;
        private dRotationUpdater rotationUpdater = null;

        public dAimState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if(Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            moveComponent = Manager.dAimMove;
            moveComponent.Init(Manager.objTransform, Manager.CameraHolder, Manager.rigidbody, 0.2f);

            rotationUpdater = new dRotationUpdater();
            rotationUpdater.Init(Manager.objTransform, Manager.CameraHolder);
        }

        public override void FixedTick()
        {
            moveComponent.FixedTick(Manager.moveVector);
        }

        public override void OnEnter()
        {
            // Camera shift to aiming camera mode
        }

        public override void OnExit()
        {
            // Camera shift to platformer camera mode
        }

        public override Type Tick()
        {
            Debug.Log("Aiming");
            if (!Manager.ADS)
            {
                return typeof(dIdleState);
            }
            rotationUpdater.Tick();
            

            return null;
        }
    }
}