using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Danish.StateCode
{


    public class dRising : dTraversalBaseState
    {
        private dStateManager Manager;
        private Rigidbody m_Rigid;

        private float m_Yvelocity = 0;

        public dRising(dStateManager _stateManager) : base(_stateManager.obj)
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
            m_Yvelocity = 0;
        }

        public override Type Tick()
        {
            Debug.Log("Currently Rising");

            if(m_Yvelocity > 0)
            {
                m_Yvelocity = m_Yvelocity * 0.5f;
                Vector3 vel = m_Rigid.velocity;

                vel.y = m_Yvelocity;

                m_Rigid.velocity = vel;
            }

            if(m_Yvelocity < 0)
            {
                return typeof(dFalling);
            }

            return null;
        }
    }
}