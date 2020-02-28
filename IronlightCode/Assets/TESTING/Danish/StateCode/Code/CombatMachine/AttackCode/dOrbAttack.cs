using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.StateCode;
using Danish.Tools;

namespace Danish.AttackCode
{

    public class dOrbAttack
    {
        GameObject player;
        Transform transform;

        Vector3 fireDirection = Vector3.zero;

        public void Init(dStateManager manager)
        {
            player = manager.obj;
            transform = manager.objTransform;

            fireDirection = transform.forward;
        }


        public void Shoot(dObjectPooler _pooler)
        {
            GameObject bullet = _pooler.SpawnFromPool("Orb", (transform.position + (transform.forward * 2)), transform.localRotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.velocity = fireDirection.normalized * 5;
        }
    }
}