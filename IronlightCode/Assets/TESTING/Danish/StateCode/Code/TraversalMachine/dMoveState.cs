using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.StateCode
{
    


    public class dMoveState : dTraversalBaseState
    {

        [Header("Speeds")]
        public float forwardSpeed = 1f;
        public float backwardSpeed = 1f;
        public float straffeSpeed = 1f;
        public float generalSpeed = 5f;


        private dStateManager Manager;
        private Rigidbody m_Rigid;
        private Animator m_Anim;
        private Vector3 m_ConvertedVector;
        private Vector3 m_NewPosition;

        public dMoveState(dStateManager _stateManager) : base (_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if(Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

            m_Rigid = Manager.rigidbody;
            m_Anim = Manager.animator;
        }

        

        public override void OnEnter()
        {
            Debug.Log("Entering Move State");
            Manager.isMoving = true;
            m_Anim.SetBool("Moving", true);
        }

        public override void OnExit()
        {
            Debug.Log("Exiting Move State");
            Manager.isMoving = false;
            m_Anim.SetBool("Moving", false);
        }

        public override Type Tick()
        {
            if(Manager.moveVector == Vector2.zero)
            {
                return typeof(dIdleState);
            }

            Debug.Log("In Move State");

            m_ConvertedVector = ConvertMoveVector(Manager.moveVector);

            m_NewPosition = CalculateNewPosition(m_ConvertedVector);

            UpdateAnimator(Manager.moveVector);
            MoveToNewPosition(m_NewPosition);


            return null;
        }



        Vector3 ConvertMoveVector(Vector2 inputVector)
        {
            Vector3 converted = Vector3.zero;

            inputVector.x *= straffeSpeed;
            inputVector.y *= forwardSpeed;

            inputVector = inputVector.normalized;

            converted = new Vector3(inputVector.x, 0f, inputVector.y);
            converted *= generalSpeed;

            return converted;
        }

        Vector3 CalculateNewPosition(Vector3 vector)
        {
            Vector3 offset = Vector3.zero;
            Vector3 newPos = Vector3.zero;

            offset = vector * Time.deltaTime;
            newPos = m_Rigid.position + offset;

            return newPos;
        }

        void MoveToNewPosition(Vector3 vector)
        {
            m_Rigid.MovePosition(vector);
        }

        void UpdateAnimator(Vector2 vector)
        {
            m_Anim.SetFloat("Forward", vector.y);
            m_Anim.SetFloat("Strafe", vector.x);
        }
    }
}