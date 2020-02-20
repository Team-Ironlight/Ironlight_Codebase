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

        public float jumpStartSpeed = 7;

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
        }

        public override void OnEnter()
        {
            JumpHandler.Init(MainManager.moveVector, m_Rigid);
            //FloatHandler.Init(MainManager.objTransform, Manager.CameraHolder, Manager.rigidbody, 0.8f);
            
        }

        public override void OnExit()
        {
            JumpHandler.ResetValues();
            m_Grounded = false;
        }

        public override Type Tick()
        {
            Debug.Log("Start Jump");

            m_Grounded = JumpHandler.GroundCheck();

            if (!m_Grounded)
            {
                JumpHandler.Tick();
                FloatHandler.Tick(Manager.moveVector);
            }
            else
            {
                return typeof(dIdleState);
            }

            return null;
        }

        public override void FixedTick()
        {
            throw new NotImplementedException();
        }



        //bool OnGroundCheck()
        //{
        //    Vector3 start = MainManager.objTransform.position;
        //    Vector3 end = start + (Vector3.down * 0.5f);

        //    Debug.DrawLine(start, end, Color.red);
        //    RaycastHit hit;
        //    if (Physics.Linecast(start, end, out hit, ( 1 << 10)))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
    }
}