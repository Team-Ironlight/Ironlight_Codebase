using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.StateCode
{

    public class dJumpState : dTraversalBaseState
    {
        private dStateManager Manager;

        private Vector3 jumpDirection = Vector3.zero;
        private Rigidbody m_Rigid;
        private Vector3 m_Velocity = Vector3.up;
        private bool m_Grounded = false;

        public dJumpState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if (Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            m_Rigid = Manager.rigidbody;
        }

        public override void OnEnter()
        {
            m_Velocity *= 5;
            m_Rigid.velocity = m_Velocity;
            
        }

        public override void OnExit()
        {
            jumpDirection = Vector3.zero;
            m_Velocity = Vector3.up;
        }

        public override Type Tick()
        {
            Debug.Log("Start Jump");

            if (m_Grounded)
            {

            }
            else
            {

            }

            if(m_Rigid.velocity.y > 0)
            {
                m_Rigid.velocity = new Vector3(m_Rigid.velocity.x, m_Rigid.velocity.y * 0.5f, m_Rigid.velocity.z);
            }
            else if(m_Rigid.velocity.y < 0)
            {
                m_Rigid.velocity = new Vector3(m_Rigid.velocity.x, m_Rigid.velocity.y - Time.deltaTime, m_Rigid.velocity.z);
                m_Grounded = OnGroundCheck();
            }


            Manager.rigidbody.velocity = (m_Velocity * 4);
            Debug.Log(Manager.rigidbody.velocity);

            return typeof(dRising);
        }



        bool OnGroundCheck()
        {
            Vector3 start = MainManager.objTransform.position;
            Vector3 end = start + (Vector3.down);

            RaycastHit hit;
            if (Physics.Linecast(start, end, out hit, ( 1 << 10)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}