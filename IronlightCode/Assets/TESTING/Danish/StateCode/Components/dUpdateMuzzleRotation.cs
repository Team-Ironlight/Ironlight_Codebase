using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Components
{
    public class dUpdateMuzzleRotation
    {
        Transform _player;
        Transform _muzzle;

        float rotationSpeed = 15f;

        public void Init(Transform playerHolder, Transform muzzleHolder)
        {
            _player = playerHolder;
            _muzzle = muzzleHolder;
        }

        public void Tick()
        {
            RotatePlayerToCameraForward(_muzzle, _player);
        }



        void RotatePlayerToCameraForward(Transform toRotate, Transform rotationRef)
        {
            Quaternion currentObjRot = toRotate.rotation;
            Quaternion target = rotationRef.rotation;


            target.x = 0;
            target.z = 0;

            toRotate.rotation = Quaternion.Lerp(currentObjRot, target, rotationSpeed * Time.deltaTime);

        }
    }
}