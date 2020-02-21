using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ROFO
{

    //no gravity
    //lives for set amount of time
    //steering behaviour to target onto player 
    //normal forward direction from instantiation + steering direction
    public class ProjBehaviourSeeker : MonoBehaviour
    {
        //SO that holds all info for this
        public SO_Projectile_Seeker so;

        private Transform target;
        private float velocity = 1f;
        private float lifeTime = 1f;
        private float seekIntensity = 0.5f;
        private float seekControl = 0.1f;
        private Vector3 forward;
        private Vector3 trajectory;

        private float count = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            Setup();
            forward = transform.forward;
            //for now
            target = GameObject.Find("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            count += Time.deltaTime;

            if (count < lifeTime)
            {
                Vector3 seek = Seek(transform.position, target);

                //if dot is negative 
                if (Vector3.Dot(forward, seek) < seekControl)
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
            else
            {
                Destroy(gameObject);
            }
        }

        private Vector3 Seek(Vector3 pos, Transform target)
        {
            return (target.position - pos).normalized;
        }


        private void Setup()
        {
            velocity = so.velocity;
            lifeTime = so.lifeTime;
            seekIntensity = so.seekIntensity;
            seekControl = so.seekControl;
        }

        public void Set(SO_Projectile_Seeker so, Transform t)
        {
            target = t;
            velocity = so.velocity;
            lifeTime = so.lifeTime;
            seekIntensity = so.seekIntensity;
            seekControl = so.seekControl;
        }
    }
}
