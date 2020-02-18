using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//shoot a projectile
//should there be a counter???
public class Projectile : IRState
{
    //constructor
    public Projectile(GameObject launcher, GameObject projectile, float launchTime)
    {
        this.projectile = projectile;

        //find launcher on this gameobject
        this.launcher = launcher.GetComponent<LauncherBehaviour>();

        this.launchTime = launchTime;
    }
    
    private GameObject projectile;
    private LauncherBehaviour launcher;
    float launchTime = 1f;


    private float count = 0.0f;
    public override void Enter()
    {
        count = 0.0f;
    }

    public override void Execute(Transform t)
    {        
        //Debug.Log("<color=green>LaunchTime: </color>" + launchTime);
        if(count < launchTime)
        {
            count += Time.deltaTime;
        }
        else
        {
            //launch projectile
            Debug.Log("<color=purple>Launch Projectile</color>");
            launcher.Launch(projectile);

            //reset count
            count = 0.0f;
        }
    }

    public override void Exit()
    {
        
    }

    public override string Name()
    {
        return "Projectile";
    }
}
