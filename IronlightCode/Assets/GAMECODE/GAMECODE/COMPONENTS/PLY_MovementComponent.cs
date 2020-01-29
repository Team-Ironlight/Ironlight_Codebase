﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_MovementComponent : MonoBehaviour
{
    public Vector2 vMoveInput = Vector3.zero;
    private Vector3 _vMoveDir = Vector3.zero;
    [SerializeField] private float _fMoveSpeed;
    [SerializeField] private float _fJumpForce;
    float distToGround;
    [Range(0.1f,0.8f)]
    public float groundThreshHold = 0.8f;
    bool IsGrounded;

    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        CalculateMoveDir();
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, distToGround + groundThreshHold);
        if (IsGrounded)
        {
            print("grounded...");
        }
    }

    public void CalculateMoveDir()
    {
        Vector3 ver = vMoveInput.x * Camera.main.transform.right;
        Vector3 hor = vMoveInput.y * Camera.main.transform.forward;

        _vMoveDir = ver + hor;
        _vMoveDir.y = 0;
        _vMoveDir = _vMoveDir.normalized * _fMoveSpeed;

        //GetComponent<PHY_Physics>().AddHorizontalAcceleration(new Vector2(_vMoveDir.x, _vMoveDir.z));

        if (Input.GetButtonDown("Jump")&& IsGrounded)
            GetComponent<PHY_Physics>().SetVerticalForce(_fJumpForce);
    }

    private void OnDisable()
    {
        // Switch to idle
        GetComponent<PHY_Physics>().AddHorizontalAcceleration(Vector2.zero);
    }
}
