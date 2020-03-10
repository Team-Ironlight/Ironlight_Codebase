using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    [CreateAssetMenu(fileName = "ProjectileNew", menuName = "AI/Projectile")]
    public class SO_Projectile : ScriptableObject
    {
        public ProjectileEnum type;

        //seeker variables
        public float velocity = 1f;
        public float lifeTime = 1f;
        [Range(0.1f, 2f)] public float seekIntensity = 0.5f;
        [Range(0.1f, 1f)] public float seekControl = 0.1f;



        //create the correct monobehaviour and place info inside
        public IProjectile SetupProjectile(Transform target)
        {
            switch (type)
            {
                case ProjectileEnum.Seeker:
                    return new Projectile_Seeker(target, velocity, lifeTime, seekIntensity, seekControl);
                case ProjectileEnum.Physics:
                    return null;
                default:
                    return null;
            }
        }
    }
}



