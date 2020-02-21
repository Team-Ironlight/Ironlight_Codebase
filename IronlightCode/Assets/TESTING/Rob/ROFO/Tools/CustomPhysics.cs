using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class CustomPhysics
    {
        //RANDOM SHIT
        //for calculating velocity... doesn't truely work lol
        private float CalculateVelocity(Transform proj, Transform target)
        {
            Vector3 dx = new Vector3(target.position.x - proj.position.x, 0f, target.position.z - proj.position.z);
            float dxMag = dx.magnitude;
            Debug.Log("dx: " + dxMag);
            float dyMag = target.position.y - proj.position.y;
            Debug.Log("dy: " + dyMag);
            float angle = proj.transform.rotation.eulerAngles.x;
            Debug.Log("Angle: " + angle);
            float a = 0.5f * (-10f * (dxMag * dxMag));
            Debug.Log("a: " + a);
            float b = -(dyMag + (dxMag * Mathf.Sin(angle)) / Mathf.Cos(angle));
            Debug.Log("b: " + b);
            float velocity = Mathf.Sqrt(a / b);
            Debug.Log("<color=red>Velocity: </color>" + velocity);

            return 0f;
        }
    }
}
