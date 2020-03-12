using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public class ProjectileHolder : MonoBehaviour
    {
        private IProjectile type;
        public string sType;
        public void SetProjectile(IProjectile p)
        {
            type = p;
            sType = p.ToString();
            //Debug.Log("Null?: " + type);
        }

        private bool terminate = false;

        private void Start()
        {
            //sets initial values
            type.Setup(transform);
        }

        private void Update()
        {
            type.Fire(transform);
            terminate = type.Check();

            if(terminate)
            {
                Destroy(gameObject);
            }
        }

        //collide with something destory for now
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}


