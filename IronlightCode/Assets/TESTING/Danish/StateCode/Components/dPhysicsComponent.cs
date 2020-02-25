using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Components
{
    public class dPhysicsComponent
    {
        private Rigidbody m_Rigid;
        Vector3 velocity = Vector3.zero;
        float GravityModifier = 1;

        public bool isGrounded = false;

        public void Init(Rigidbody rigidbody, float gravityLevel) 
        {
            m_Rigid = rigidbody;
            GravityModifier = gravityLevel;
        }

        public void Tick() 
        {
            isGrounded = GroundCheck();
        }

        public void FixedTick() 
        {
            if (!isGrounded)
            {
                ApplyGravity();
            }
        }


        void ApplyGravity()
        {
            velocity += GravityModifier * Physics.gravity * Time.deltaTime;


            m_Rigid.velocity = velocity;
        }

        public bool GroundCheck()
        {
            RaycastHit hit;
            if (m_Rigid.SweepTest(Vector3.down, out hit, 0.1f))
            {
                if (hit.collider.gameObject.layer == 10)
                {
                    Debug.Log(hit.distance + " Ground check = " + isGrounded);
                    if (hit.distance > 0.04f)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }
    }
}