// Programmer :  Phil James Lapuz
// Description : This Class is for Collision Trigger, to define if the Orb has been Activated.
//              This is the first version of TriggerPad.
//
// Requirements :  
//      #1 -  Assign Name of the GameObject through the Inspector where you attach this Script
//      #2 -  make sure the GameObject it has attached "Sphere Collider" on it and activated the "Is Trigger"
//      #3 -  by default the "is Active" was not Activated , Do not tick the CheckBox from the inspector, this will be handle when theres a collision trigger
//      #4 -  ensure assign Player Tag name on it ,  "Player"

using UnityEngine;
using System.Collections;
using Ironlight;

[System.Serializable]
public class TriggerPad: ColliderManager.ColliderCheck
{
    [SerializeField]
    public bool isActive;
    public string MyCurrentStatus = "";

    //Only this two Function Call are used for this version the rest are for readyness future change 

    private void OnTriggerEnter(Collider other)
    {
      //For this Version we use Collision Trigger -  This can be change into a Projectile (Blast) that Collide into this Orbs GameObject , basically when the Blast Hit then set isActive into True! 
        if (other.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    public override string CheckConditions()
    {
       if(isActive)
        {
            MyCurrentStatus = Name;
            return MyCurrentStatus;
        }
        return "";
    }



    //Note: Below are Only for Future Changes 




    public override void OnEnter()  // similar to Start() - first frame Update!
    {
        // for future changes
      
    }

    public override void Run()     // similar to Update() - execute this Once per frame
    {
        // for future changes
    
    }

    public override void OnExit()
    {
        // for future changes
    }


}
