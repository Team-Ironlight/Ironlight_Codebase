using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public class Projectile_Seeker : IProjectile
    {
        //never gets called...
        public void Setup(Transform transform)
        {
            //should be created from instantiation
            forward = transform.forward;
            trajectory = forward;
        }

        //gets called by holder
        public void Fire(Transform transform)
        {
            count += Time.deltaTime;

            if (count < lifeTime)
            {
                Vector3 seek = Seek(transform.position, target);

                //if dot is negative 
                if (Vector3.Dot(transform.forward, seek) < seekControl)
                {
                    //past target move normally
                    transform.position += trajectory * velocity * Time.deltaTime;
                }
                else
                {
                    //recalculate trajectory
                    trajectory = (forward + (seek * seekIntensity)).normalized;
                    transform.position += trajectory * velocity * Time.deltaTime;
                }
            }
        }

        public bool Check()
        {
            if(count > lifeTime)
            {
                //reset count?
                count = 0f;
                return true;
            }

            return false;
        }

        private Vector3 Seek(Vector3 pos, Transform target)
        {
            return (target.position - pos).normalized;
        }



        //constructor
        public Projectile_Seeker(Transform target,
                                 float velocity,
                                 float lifeTime,
                                 float seekIntensity,
                                 float seekControl)
        {
            this.target = target;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.seekIntensity = seekIntensity;
            this.seekControl = seekControl;
        }

        
        //variables
        public Transform target;
        public float velocity = 1f;
        public float lifeTime = 1f;
        public float seekIntensity = 0.5f;
        public float seekControl = 0.1f;
        public Vector3 forward;
        public Vector3 trajectory;

        private float count = 0.0f;
    }
}
