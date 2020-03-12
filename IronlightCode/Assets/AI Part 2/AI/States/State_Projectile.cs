using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public class State_Projectile : IState
    {
        //constructor
        public State_Projectile(Transform launcherPosition,
                                Transform target,
                                GameObject projectile,
                                SO_Projectile soProjectile,
                                float timer,
                                float delay)
        {
            this.launcherPosition = launcherPosition;
            this.target = target;
            this.projectile = projectile;
            this.soProjectile = soProjectile;
            this.timer = timer;
            this.delay = delay;
        }

        //variables
        private Transform launcherPosition;
        private Transform target;
        private GameObject projectile;


        private SO_Projectile soProjectile;
        
        public void Enter()
        {
            Debug.Log("Projectile Enter");                      
        }

        public float delay, timer;
        private float count;
        private bool fired = false;

        public void Execute()
        {
            Debug.Log("Projectile");
            count += Time.deltaTime;
            //Debug.Log("Count: " + count);

            if(fired == false && count > delay)
            {
                //fire
                ProjectileManager.CreateProjectile(projectile,
                                                   launcherPosition.position,
                                                   launcherPosition.rotation,
                                                   soProjectile.SetupProjectile(target));


                fired = true;
            }
            else if(count > timer)
            {
                //reset count
                count = 0f;
                fired = false;
            }            
        }

        public void Examine()
        {

        }

        public void Exit()
        {
            Debug.Log("Projectile Exit");
        }
    }
}

