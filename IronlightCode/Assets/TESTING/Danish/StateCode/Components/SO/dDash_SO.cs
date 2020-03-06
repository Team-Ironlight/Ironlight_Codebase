using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components.Abstract;

namespace Danish.Components.SO
{

    [CreateAssetMenu(fileName = "Dash Component.asset", menuName = "Components/Dash")]
    public class dDash_SO : dBaseComponent
    {
        //public float DistanceToTravel = 0;
        //public float DashTime = 0;


        public float FrictionMultiplier = 20;


        Vector3 dashDirection = Vector3.zero;
        Rigidbody rigidbody;


        public override void Init()
        {
            base.Init();
        }


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



        public void ResetAllValues()
        {
            dashDirection = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }
    }
}