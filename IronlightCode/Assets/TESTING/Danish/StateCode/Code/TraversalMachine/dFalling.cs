using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.StateCode
{


    public class dFalling : dTraversalBaseState
    {
        private dStateManager Manager;
        private Rigidbody m_Rigid;

        private float m_Yvelocity = 0;


        public dFalling(dStateManager _stateManager) : base(_stateManager.obj)
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
            m_Yvelocity = m_Rigid.velocity.y;
        }

        public override void OnExit()
        {

        }

        public override Type Tick()
        {
            Debug.Log("Currently Falling");

            if (!OnGroundCheck())
            {
                m_Yvelocity = m_Yvelocity * 0.5f;
                Vector3 vel = m_Rigid.velocity;

                vel.y -= 0.98f * Time.deltaTime;

                m_Rigid.velocity = vel;
            }
            else
            {
                return typeof(dIdleState);
            }

            return null;
        }



        bool OnGroundCheck()
        {
            Vector3 start = MainManager.objTransform.position;
            Vector3 end = start + (Vector3.down);

            RaycastHit hit;
            if(Physics.Linecast(start, end, out hit, ~ (1 << 10)))
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