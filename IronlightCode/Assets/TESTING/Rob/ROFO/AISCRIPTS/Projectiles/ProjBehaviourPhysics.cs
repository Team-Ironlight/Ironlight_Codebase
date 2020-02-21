using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class ProjBehaviourPhysics : MonoBehaviour
    {
        private float velocity = 1f;
        private float gravity = -1f;
        private float lifeSpan = 1f;
        public float count;

        private Vector3 startForward;
        private float startY;
        private Vector3 XZ;
        float Y;

        private bool run = false;

        //stats must be set to run
        public void Setup(float v, float g, float l)
        {
            velocity = v;
            gravity = g;
            lifeSpan = l;

            run = true;
        }

        //basic projectile
        private void Start()
        {
            //get forward when instantiated
            startForward = transform.forward;
            startY = transform.position.y;
            XZ = new Vector3(startForward.x, 0f, startForward.z) * velocity;
            Y = transform.forward.y * velocity;
        }

        private void Update()
        {
            if (run)
            {
                count += Time.deltaTime;

                transform.position = MoveProjectile();

                if (count > lifeSpan)
                {
                    DestroyProjectile();
                }
            }
        }

        private Vector3 MoveProjectile()
        {
            //move along XZ of forward by velocityH
            transform.position += XZ * Time.deltaTime;

            //displacment formula for Y
            float yDisplacement = (startForward.y * velocity * count) + (0.5f * gravity * (count * count));

            return new Vector3(transform.position.x, startY + yDisplacement, transform.position.z);
        }


        private void DestroyProjectile()
        {
            //call some particle effects or something
            Debug.Log("<color=red>Destroy Projectile</color>");
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
            DestroyProjectile();
        }
    }    
}
