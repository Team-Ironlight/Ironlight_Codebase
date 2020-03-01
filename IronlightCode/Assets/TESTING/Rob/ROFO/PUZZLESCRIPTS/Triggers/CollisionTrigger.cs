using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class CollisionTrigger : MonoBehaviour, ITrigger
    {
        public string tagCollision;
        private AChange[] changes;

        public void Trigger()
        {
            for (int i = 0; i < changes.Length; i++)
            {
                changes[i].Change();
            }
        }

        //get all changes attached to this object
        private AChange[] GatherChanges()
        {
            return GetComponents<AChange>();
        }

        private void Start()
        {
            changes = GatherChanges();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == tagCollision)
            {
                //call trigger
                Trigger();
            }
        }
    }
}
