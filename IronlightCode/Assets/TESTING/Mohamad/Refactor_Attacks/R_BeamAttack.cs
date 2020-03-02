using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;

namespace Sharmout.attacks
{
    public class R_BeamAttack
    {
        Transform muzzleRef = null;
        Vector3 firePosition = Vector3.zero;
        Quaternion fireRotation = Quaternion.Euler(Vector3.zero);

        dObjectPooler beamPool = null;

        

        public void Init(Transform _muzzle, dObjectPooler _pool)
        {
            muzzleRef = _muzzle;
            beamPool = _pool;
        }

        public void Shoot()
        {
            GetBeamToShoot();
        }

        void Tick()
        {
            
        }

        

        GameObject GetBeamToShoot()
        {
            firePosition = muzzleRef.position;
            fireRotation = muzzleRef.rotation;

            return beamPool.SpawnFromPool("Beam", firePosition, fireRotation);
        }
    }
}