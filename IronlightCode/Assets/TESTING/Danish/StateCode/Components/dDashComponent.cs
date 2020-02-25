using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Components
{
    public class dDashComponent
    {
        Vector3 dashDirection = Vector3.zero;
        Rigidbody rigidbody;

        private Vector3 velocity = Vector3.zero;

        private float FrictionMultiplier = 20;

        public void Init(Vector3 playerForward, Rigidbody rigid)
        {
            Vector3 convertedVector = playerForward;

            convertedVector = convertedVector.normalized;

            dashDirection = convertedVector;

            rigidbody = rigid;

            StartDash();
        }

        void StartDash()
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