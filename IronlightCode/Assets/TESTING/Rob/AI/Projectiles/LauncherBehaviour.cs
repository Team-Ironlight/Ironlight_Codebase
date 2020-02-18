using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//just instanties projeciles in the right direction and rotation
public class LauncherBehaviour : MonoBehaviour
{   
    public void Launch(GameObject projectile)
    {
        GameObject p = Instantiate(projectile, transform.position, transform.rotation);
    }
}
