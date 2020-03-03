using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;
using Danish.StateCode;
using Sharmout.SO;

namespace Sharmout.attacks
{
    public class R_OrbAttack
    {
        // reference to the muzzle transform to get firePosition and rotation
        Transform muzzleRef = null;
        Vector3 firePosition = Vector3.zero;
        Quaternion bulletRotation = Quaternion.Euler(Vector3.zero);

        // reference to the object pool of the orb bullet prefabs
        dObjectPooler OrbPool = null;

        // tag name of pool containing orb bullets
        string poolTag = " ";

        public OrbSO orbStats = null;


        // Initialization function for this attack
        public void Init(Transform _muzzle, dObjectPooler _pooler)
        {
            // set references
            muzzleRef = _muzzle;
            OrbPool = _pooler;
        }

        //public void Tick(dObjectPooler pool, string tagName, Transform muzzle)
        //{

            
        //}

        // function to shoot orb
        public void Shoot()
        {
            GetOrbToShoot();
        }

        // gets a orb bullet from the pool and gives it the firePosition and rotation
        GameObject GetOrbToShoot()
        {
            firePosition = muzzleRef.position;
            bulletRotation = muzzleRef.rotation;

            return OrbPool.SpawnFromPool("Orb", firePosition, bulletRotation);
        }


    }
}