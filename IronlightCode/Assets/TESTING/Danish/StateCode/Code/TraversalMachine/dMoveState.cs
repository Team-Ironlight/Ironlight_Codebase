using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;
using brian.Components;

namespace Danish.StateCode
{
    


    public class dMoveState : dTraversalBaseState
    {
        private dStateManager Manager;
        private Rigidbody m_Rigid;
        private Animator m_Anim;

        private dMoveComponent MoveHandler = null;
        private dRotationUpdater rotationUpdater = null;
        private dPhysicsComponent physics = null;

        private dAnimationUpdater animationUpdater = null;

        //[Header("Speeds")]
        //public float forwardSpeed = 1f;
        //public float backwardSpeed = 1f;
        //public float straffeSpeed = 1f;
        //public float generalSpeed = 5f;
        //public float rotationSpeed = 15f;


        //private dStateManager Manager;
        //private Rigidbody m_Rigid;

        //private Vector3 camForward;
        //private Vector3 m_ConvertedVector;
        //private Vector3 m_NewPosition;

        public dMoveState(dStateManager _stateManager) : base (_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if(Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            m_Rigid = Manager.rigidbody;
            m_Anim = Manager.animator;

            MoveHandler = Manager.dMove;
            MoveHandler.Init(Manager.objTransform, Manager.CameraHolder, Manager.rigidbody, 1);

            rotationUpdater = new dRotationUpdater();
            rotationUpdater.Init(Manager.objTransform, Manager.CameraHolder);

            physics = Manager.dPhysics;
            physics.Init(m_Rigid, 0.5f);

            animationUpdater = Manager.dAnimation;
        }


        public override void OnEnter()
        {
            //Debug.Log("Entering Move State");
            Manager.isMoving = true;
            m_Anim.SetBool("Moving", true);

        }

        public override void OnExit()
        {
            //Debug.Log("Exiting Move State");
            Manager.isMoving = false;
            m_Anim.SetBool("Moving", false);
        }

        public override void FixedTick()
        {
            MoveHandler.FixedTick(Manager.moveVector);
            physics.FixedTick();

            
        }

        public override Type Tick()
        {
            if (Manager.jump && physics.isGrounded)
            {
                Manager.jump = false;
                return typeof(dJumpState);
            }

            if (Manager.isDashing)
            {
                Manager.isDashing = false;
                return typeof(dDashState);
            }

            if (Manager.ADS)
            {
                return typeof(dAimState);
            }


            if (Manager.moveVector == Vector2.zero)
            {
                return typeof(dIdleState);
            }

            Debug.Log("In Move State");

            rotationUpdater.Tick();
            physics.Tick();
            //MoveHandler.Tick(Manager.moveVector);

            animationUpdater.Tick(Manager.moveVector);
            //UpdateAnimator(Manager.moveVector);

            return null;
        }

        //Vector2 move = Vector2.zero;

        //void UpdateAnimator(Vector2 vector)
        //{
        //    if(move != vector)
        //    {
        //        move = Vector2.Lerp(move, vector, Time.deltaTime);
        //    }


        //    m_Anim.SetFloat("Forward", move.y);
        //    m_Anim.SetFloat("Strafe", move.x);
        //}
    }
}