using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components.Abstract;

namespace Danish.Components.SO
{
    [CreateAssetMenu(fileName = "Jump Component.asset", menuName = "Components/Jump")]
    public class dJump_SO : dBaseComponent
    {
        public float GravityModifier = 1;
        public float JumpForce = 4.5f;

        private Rigidbody rigidbody;
        private Vector3 velocity = Vector3.zero;
        private Vector3 jumpDirection = Vector3.up;


        public override void Init()
        {
            base.Init();
        }

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
            //rigidbody.useGravity = false;

            //rigidbody.AddForce(Physics.gravity * (rigidbody.mass * rigidbody.mass));
        }

        public void ResetValues()
        {
            jumpDirection = Vector3.up;
            rigidbody.velocity = Vector3.zero;
        }

    }
}