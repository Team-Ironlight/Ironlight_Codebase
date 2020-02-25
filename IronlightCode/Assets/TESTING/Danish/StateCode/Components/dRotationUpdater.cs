using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Danish.Components
{
    public class dRotationUpdater
    {
        Transform _player;
        Transform _camera;

        float rotationSpeed = 15f;

        public void Init(Transform playerHolder, Transform cameraHolder)
        {
            _player = playerHolder;
            _camera = cameraHolder;
        }

        public void Tick()
        {
            RotatePlayerToCameraForward(_player, _camera);
        }



        void RotatePlayerToCameraForward(Transform toRotate, Transform camera)
        {
            Quaternion currentObjRot = toRotate.rotation;
            Quaternion cameraRot = camera.rotation;


            cameraRot.x = 0;
            cameraRot.z = 0;

            toRotate.rotation = Quaternion.Lerp(currentObjRot, cameraRot, rotationSpeed * Time.deltaTime);

        }
    }
}