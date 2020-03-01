using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sharmout.attacks
{
    public class R_BeamAttack
    {
        Transform muzzleRef = null;
        Vector3 firePosition = Vector3.zero;
        Quaternion fireRotation = Quaternion.Euler(Vector3.zero);

        public void Init(Transform _muzzle)
        {
            muzzleRef = _muzzle;
        }

        void Tick()
        {

        }

        void StartBeam()
        {

        }

        void FinishBeam()
        {

        }
    }
}