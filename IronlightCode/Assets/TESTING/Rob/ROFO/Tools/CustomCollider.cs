using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //put outside class so accessible to all classes
    public enum ColliderType { sphere, box, capsule };
    public enum CapsuleDirection { Xaxis, Yaxis, Zaxis }


    //Rob
    //Custom editor gives a handle to any object that has this script
    //Than on game start it will create a sphere collider the same size of the handler
    public class CustomCollider : MonoBehaviour
    {
        [Header("Handles")]
        [SerializeField] public bool handleOn = true;
        [SerializeField] public float handleSize = 1f;

        [SerializeField] public ColliderType currentCollider;

        [Header("Sphere")]
        [SerializeField] public float radiusSize = 1f;
        [Header("Box")]
        [SerializeField] public float length = 1f;
        [SerializeField] public float width = 1f;
        [SerializeField] public float depth = 1f;
        [Header("Capsule")]
        [SerializeField] public float radius = 1f;
        [SerializeField] public float height = 1f;

        [SerializeField] public CapsuleDirection direction = CapsuleDirection.Xaxis;
        [SerializeField] public Vector3 offset = Vector3.zero;

        [SerializeField] public bool isTrigger = true;

        private Collider newCollider;


        private void Start()
        {
            //create collider using settings
            if (currentCollider == ColliderType.sphere)
            {
                //create new sphere collider
                newCollider = new SphereCollider();
                newCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;

                //set sphere colliders radius to size of handleSize
                SphereCollider temp = newCollider.GetComponent<SphereCollider>();
                temp.radius = radiusSize;
                temp.center = offset;

                if (isTrigger)
                {
                    //set to trigger
                    newCollider.isTrigger = true;
                }
            }
            else if (currentCollider == ColliderType.box)
            {
                //create new sphere collider
                newCollider = new BoxCollider();
                newCollider = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;

                //set box collider dimensions...
                BoxCollider temp = newCollider.GetComponent<BoxCollider>();
                temp.size = new Vector3(width, depth, length);
                temp.center = offset;

                if (isTrigger)
                {
                    //set to trigger
                    newCollider.isTrigger = true;
                }
            }
            else if (currentCollider == ColliderType.capsule)
            {
                //create new sphere collider
                newCollider = new CapsuleCollider();
                newCollider = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;

                //set box collider dimensions...
                CapsuleCollider temp = GetComponent<CapsuleCollider>();
                temp.radius = radius;
                temp.height = height;
                temp.direction = (int)direction;
                temp.center = offset;

                if (isTrigger)
                {
                    //set to trigger
                    newCollider.isTrigger = true;
                }
            }

            //destroy script after???
            Destroy(this);
        }
    }
}
