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
        Vector3 fireDirection = Vector3.zero;
        Quaternion bulletRotation = Quaternion.Euler(Vector3.zero);

        // reference to the object pool of the orb bullet prefabs
        dObjectPooler OrbPool = null;

        GameObject currentOrb = null;
        R_OrbLogic logic = null;

        // tag name of pool containing orb bullets
        string poolTag = " ";

        public OrbSO orbStats = null;


        // Initialization function for this attack
        public void Init(Transform _muzzle, dObjectPooler _pooler, OrbSO _stats)
        {
            // set references
            muzzleRef = _muzzle;
            OrbPool = dObjectPooler.Instance;
            orbStats = _stats;
        }

        public void Tick(Transform muzzle)
        {
            

        }

        public void SetFireDirection(Vector3 direction)
        {
            fireDirection = direction;
        }

        public void SetFirePosition(Vector3 position)
        {
            firePosition = position;
        }


        // function to shoot orb
        public void Shoot()
        {
            ResetOrb();


            if (currentOrb == null)
            {
                currentOrb = GetOrbToShoot();
                if (currentOrb.TryGetComponent(out R_OrbLogic _logic))
                {
                    logic = _logic;
                    logic.Init(firePosition, fireDirection, orbStats);
                }
            }
        }

        public void ResetOrb()
        {
            if(currentOrb != null)
            {
                currentOrb = null;
                logic = null;
            }
        }

        // gets a orb bullet from the pool and gives it the firePosition and rotation
        GameObject GetOrbToShoot()
        {
            //firePosition = muzzleRef.position;
            bulletRotation = muzzleRef.rotation;

            return OrbPool.SpawnFromPool("Orb", firePosition, bulletRotation);
        }


    }
}