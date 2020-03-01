using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;

namespace Danish.StateCode
{

    public class dJumpState : dTraversalBaseState
    {
        private dStateManager Manager;

        private Rigidbody m_Rigid;
        private Vector3 m_Velocity = Vector3.up;

        private bool m_Grounded = false;
        private dJumpComponent JumpHandler = null;
        private dMoveComponent FloatHandler = null;
        private dRotationUpdater rotationUpdater = null;
        private dPhysicsComponent physics = null;

        public float jumpStartSpeed = 7;

        bool jumpStarted = false;

        public dJumpState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if (Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            m_Rigid = Manager.rigidbody;

            JumpHandler = Manager.dJump;

            FloatHandler = Manager.dFloat;
            FloatHandler.Init(MainManager.objTransform, Manager.CameraHolder, Manager.rigidbody, 0.8f);

            rotationUpdater = new dRotationUpdater();
            rotationUpdater.Init(Manager.objTransform, Manager.CameraHolder);

            physics = Manager.dPhysics;
            physics.Init(m_Rigid, 0.5f); 
        }

        public override void OnEnter()
        {
            JumpHandler.Init(MainManager.moveVector, m_Rigid);
            Manager.currentlyJumping = true;
            
        }

        public override void OnExit()
        {
            JumpHandler.ResetValues();
            jumpStarted = false;
            m_Grounded = false;

            Manager.currentlyJumping = false;
        }

        public override Type Tick()
        {
            Debug.Log("Start Jump");

            if (Manager.isDashing)
            {
                Manager.isDashing = false;
                return typeof(dDashState);
            }

            rotationUpdater.Tick();

            if (jumpStarted)
            {
                m_Grounded = physics.GroundCheck();
            }

            if (m_Grounded)
            {
                return typeof(dIdleState);
            }

            return null;
        }

        public override void FixedTick()
        {
            JumpHandler.FixedTick();
            //physics.FixedTick();

            if (!jumpStarted)
                jumpStarted = true;

            FloatHandler.FixedTick(Manager.moveVector);
        }


    }
}