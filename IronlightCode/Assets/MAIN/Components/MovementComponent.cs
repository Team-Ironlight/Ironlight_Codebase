using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public float moveAmount = 0;
    public Vector3 moveDir = Vector3.zero;
    public float moveSpeed;

    public Vector3 FinalForce;

    public void CalculateMoveAmount(float v, float h)
    {
        float m = Mathf.Abs(h) + Mathf.Abs(v);
        moveAmount = Mathf.Clamp01(m);
    }

    public void CalculateMoveDir(float v, float h)
    {
        Vector3 ver = v * Camera.main.transform.forward;
        Vector3 hor = h * Camera.main.transform.right;

        moveDir = (ver + hor).normalized;
    }

    public void Move(float targetSpeed)
    {
        FinalForce = moveDir * (targetSpeed * moveAmount);
    }

    
    //moveDir * (targetSpeed * moveAmount);.
}
