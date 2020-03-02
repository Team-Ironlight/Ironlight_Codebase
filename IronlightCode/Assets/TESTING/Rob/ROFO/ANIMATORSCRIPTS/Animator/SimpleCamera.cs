using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class SimpleCamera : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        [Range(0.01f, 0.99f)]
        public float lerpPercent = 0.5f;


        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpPercent);
            Vector3 temp = target.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(temp), lerpPercent);
        }
    }
}
