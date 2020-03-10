using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;
using Sharmout.SO;

namespace Sharmout.attacks
{
    public class R_BeamAttack
    {
        Transform muzzleRef = null;
        Vector3 firePosition = Vector3.zero;
        Vector3 fireDirection = Vector3.zero;
        Quaternion fireRotation = Quaternion.Euler(Vector3.zero);

        dObjectPooler beamPool = null;

        GameObject currentBeam = null;
        R_BeamLogic logic = null;

        public BeamSO beamStats = null;

        public void Init(Transform _muzzle, dObjectPooler _pool, BeamSO _stats)
        {
            muzzleRef = _muzzle;
            beamPool = _pool;
            beamStats = _stats;
        }

        public void StartBeam()
        {
            //firePosition = muzzleRef.position;

            if (currentBeam == null)
            {
                currentBeam = GetBeamToShoot();
                if (currentBeam.TryGetComponent(out R_BeamLogic _logic))
                {
                    logic = _logic;
                    logic.Init(muzzleRef.position, beamStats);
                }
            }


            logic.going = true;
            logic.ending = false;
        }

        public void EndBeam()
        {
            logic.going = false;
            logic.ending = true;

            logic.FinishTick();
            //logic.Tick(firePosition, muzzleRef.forward);
        }

        public void ResetBeam()
        {
            if(currentBeam != null)
            {
                currentBeam = null;
                logic = null;
            }
        }

        public void Tick()
        {
            firePosition = muzzleRef.position;

            logic.ActiveTick(firePosition, fireDirection);
        }

        public void SetFireDirection(Vector3 _direction)
        {
            fireDirection = _direction;
        }

        GameObject GetBeamToShoot()
        {
            return beamPool.SpawnFromPool("Beam", muzzleRef.position, muzzleRef.rotation);
        }
    }
}