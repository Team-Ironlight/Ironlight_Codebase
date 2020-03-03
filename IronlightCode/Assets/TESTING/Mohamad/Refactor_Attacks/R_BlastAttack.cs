using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;
using Sharmout.SO;

namespace Sharmout.attacks
{
    public class R_BlastAttack
    {
        Transform muzzleRef = null;
        dObjectPooler blastPool = null;

        GameObject currentBlast = null;
        R_BlastLogic logic = null;

        public BlastSO blastStats = null;

        public void Init(Transform _muzzle, dObjectPooler _pool)
        {
            muzzleRef = _muzzle;
            blastPool = _pool;
        }

        public void StartBlast()
        {
            if(currentBlast == null)
            {
                currentBlast = GetBlastToShoot();
                if(currentBlast.TryGetComponent(out R_BlastLogic _logic))
                {
                    logic = _logic;
                }
            }
        }

        public void ResetBlast()
        {
            if(currentBlast != null)
            {
                currentBlast = null;
                logic = null;

            }
        }


        public void Tick()
        {
            logic?.Tick(muzzleRef.position);
        }

        public void Launch()
        {
            logic?.OnRelease();
        }

        GameObject GetBlastToShoot()
        {
            return blastPool.SpawnFromPool("Blast", muzzleRef.position, muzzleRef.rotation);
        }
    }
}