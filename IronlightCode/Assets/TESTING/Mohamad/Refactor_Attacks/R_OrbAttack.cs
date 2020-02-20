using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;
using Danish.StateCode;

namespace Sharmout.attacks
{
    public class R_OrbAttack
    {
        // get a reference to magazine holder or object pool
        // TODO - create a function to shoot a bullet
        // TODO - create a attack cooldown


        // TODO - Create a separate script to handle bullet logic
        // TODO - create a coroutine to disable bullet

        Transform muzzleRef = null;
        Vector3 fireDirection = Vector3.zero;
        //dObjectPooler OrbPool = null;
        string poolTag = " ";


        // Initialization function for this attack
        public void Start_bitch(dStateManager _manager)
        {
            // muzzleRef = _manager.muzzleObjTransform;

            //OrbPool = _manager.pooler;

            fireDirection = _manager.objTransform.forward;


        }

        public void Go_bitch(dObjectPooler pool, string tagName, Transform muzzle)
        {
            GameObject objToShoot = pool.SpawnFromPool(tagName, muzzle.position, Quaternion.Euler(muzzle.forward));
            
        }

        void Shoot()
        {
            //GameObject objToShoot = OrbPool.SpawnFromPool(poolTag, muzzleRef.position, Quaternion.Euler(fireDirection));
        }

    }
}