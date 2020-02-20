using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.StateCode;

namespace Danish.Components
{
    public class dMoveComponent
    {


        [Header("Speeds")]
        public float forwardSpeed = 1f;
        public float backwardSpeed = 1f;
        public float straffeSpeed = 1f;
        public float generalSpeed = 5f;
        public float rotationSpeed = 15f;


        private dStateManager Manager;
        private Transform _playerTransform;
        private Transform _cameraHolder;
        private Rigidbody m_Rigid;

        private Vector3 camForward;
        private Vector3 m_ConvertedVector;
        private Vector3 m_NewPosition;


        public void Init(Transform playerTransform, Transform camHolder, Rigidbody rigid)
        {
            _playerTransform = playerTransform;
            _cameraHolder = camHolder;
            m_Rigid = rigid;
        }

        public void Tick(Vector2 _moveVector)
        {
            RotatePlayerToCameraForward(_playerTransform, _cameraHolder);

            m_ConvertedVector = ConvertMoveVector(_moveVector);

            m_NewPosition = CalculateNewPosition(m_ConvertedVector);
            MoveToNewPosition(m_NewPosition);



        }


        void RotatePlayerToCameraForward(Transform toRotate, Transform camera)
        {
            Quaternion currentObjRot = toRotate.rotation;
            Quaternion cameraRot = camera.rotation;


            cameraRot.x = 0;
            cameraRot.z = 0;

            toRotate.rotation = Quaternion.Lerp(currentObjRot, cameraRot, rotationSpeed * Time.deltaTime);

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
            Vector3 forward = Vector3.zero;
            Vector3 right = Vector3.zero;
            Vector3 newPos = Vector3.zero;

            forward = _playerTransform.forward * vector.z;
            right = _playerTransform.right * vector.x;

            offset = (forward + right) * Time.deltaTime;

            newPos = m_Rigid.position + offset;

            return newPos;
        }

        void MoveToNewPosition(Vector3 vector)
        {
            m_Rigid.MovePosition(vector);
        }
    }
}