using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Danish.Components
{
    public class dJumpComponent
    {
        private Vector3 jumpDirection = Vector3.up;

        private Rigidbody rigidbody;

        private Vector3 velocity = Vector3.zero;
        private Vector3 targetVelocity = Vector3.zero;

        private float GravityModifier = 1;
        public float JumpForce = 4.5f;

        public void Init(Vector2 moveDirection, Rigidbody rigid)
        {
            Vector3 convertedVector = new Vector3(moveDirection.x, 0, moveDirection.y);
            convertedVector = convertedVector.normalized;

            //Debug.Log(convertedVector);

            jumpDirection += convertedVector;

            rigidbody = rigid;

            StartJump();
        }

        void StartJump()
        {
            velocity.y = JumpForce;

            rigidbody.velocity = velocity;
        }

        public void FixedTick()
        {
            velocity += GravityModifier * Physics.gravity * Time.deltaTime;

            velocity.x = targetVelocity.x;



            rigidbody.velocity = velocity;
        }

        public void ResetValues()
        {
            jumpDirection = Vector3.up;
            rigidbody.velocity = Vector3.zero;
        }


        //public bool GroundCheck()
        //{
        //    RaycastHit hit;
        //    if (rigidbody.SweepTest(Vector3.down, out hit, 0.1f))
        //    {
        //        //Debug.Log("Sweep confirmed");
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}