using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//takes in a gameobject projectile, and a projectile type
//and instantiates them into the game world...
namespace AITEST
{
    public class ProjectileManager : MonoBehaviour
    {
        public static void CreateProjectile(GameObject proj, Vector3 position, Quaternion rotation, IProjectile type)
        {
            GameObject temp = Instantiate(proj, position, rotation);

            //set up for object pooling, check if has handler already, otherwise add and set type
            //attach projectile holder monobehaviour that runs the projectile type
            temp.AddComponent<ProjectileHolder>();

            //set type
            temp.GetComponent<ProjectileHolder>().SetProjectile(type);
        }
    }
}


