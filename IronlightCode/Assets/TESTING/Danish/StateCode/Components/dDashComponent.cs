using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Components
{
    public class dDashComponent
    {
        Vector3 dashDirection = Vector3.zero;
        Rigidbody rigidbody;
        Transform transform;

        public float FrictionMultiplier = 20;

        public void Init(Transform _transform, Rigidbody _rigid)
        {
            rigidbody = _rigid;
            transform = _transform;

        }

        public void StartDash(Vector2 moveDirection)
        {
            Vector3 convertedVector = Vector3.zero;
            
            
            if(moveDirection == Vector2.zero)
            {
                convertedVector = transform.forward;
            }

            else
            {
                convertedVector = transform.right * moveDirection.x;
                convertedVector += transform.forward * moveDirection.y;
            }


            convertedVector = convertedVector.normalized;

            dashDirection = convertedVector;


            PerformDash();

        }

        void PerformDash()
        {
            rigidbody.AddForce(dashDirection * FrictionMultiplier, ForceMode.VelocityChange);
        }

        public void Tick()
        {
            
        }

        public void ResetAllValues()
        {
            dashDirection = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }
    }

}