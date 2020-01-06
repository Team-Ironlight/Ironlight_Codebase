// Programmer : Phil James lapuz
// Description : Attached this into Door, this script will check the required number of Task/missions ( Orbs Activation thru Collisions)
//              this is our first version, im trying to do a dynamic system, a structure for Object Collision - so Feel free to change or Tweak this \
//              the Abstract class will be the template/guide for the triggerPad's
//
// Requirement :
//              #1. Attached this script into the GameObject "Door", on where do you want to do the Open Door Animation.
//              #2. Assign Number of Object to Collide thru the inspector , the "Size" must define
//              #3. from the inspector Drag-drop all the GameObject "Orbs" that has attached script(TriggerPad) on it !
//
// Scripts are :
//          1.ColliderManager
//          2.TriggerPad
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ironlight
{
    [System.Serializable]
    public class ColliderManager : MonoBehaviour
    {
        // Smoothly open a door
        public float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards
        public float openSpeed = 2.0f;     //Increasing this value will make the door open faster
        float defaultRotationAngle;
        float currentRotationAngle;
        float openTime = 0;

        [System.Serializable]
        public abstract class ColliderCheck : MonoBehaviour
        {
            // Structure for our Collider Object Class , All of this is for readyness future implementation
            public string Name;
            public abstract void OnEnter();
            public abstract void Run();
            public abstract string CheckConditions();
            public abstract void OnExit();
        }

        //Let's Collect all the required TriggerPads here
        public ColliderCheck[] ObjCollider;

        //This is the Final Flag to Open the Door - local used
        public bool isActive;

        //Container for all Activated Orbs, need this to ensure all the Collider has been activated
        bool[] TriggerArray;

        //Flag if all Orbs are activated - local used
        bool allMissionsComplete;


        // Start is called before the first frame update
        void Start()
        {
            //precaution
            if (ObjCollider == null)
             return;

            defaultRotationAngle = transform.localEulerAngles.y;
            currentRotationAngle = transform.localEulerAngles.y;


            //Initialized to False
            allMissionsComplete = false;

            //Lenght base from How many Collider you have
            TriggerArray = new bool[ObjCollider.Length];

            //For Future changes - Do this if you need something to execute function call  on each GameObject Orbs
            for(int i=0; i < ObjCollider.Length; i++)
            {
                if(ObjCollider != null)
                {
                    ObjCollider[i].OnEnter();  //This is similar to Tick() / Start() - first frame Update!

                }
            }//end for


        } //End Start()


        
        void Update()
        {
            // local flag - will only execute if false
            if(isActive !=true)
            {
              
                int countActive = 0;  // local used - total number of Activated Orbs
                for(int i =0; i< ObjCollider.Length; i++)
                {
                    //Status Check here - called one per frame
                    ObjCollider[i].Run();

                    string colliderID = ObjCollider[i].CheckConditions();  //Actions Trigger Here

                    
                    if(colliderID.Length > 0) //precaution check
                    {
                      
                        //Now let us Verify if the Object Collider is Registered!
                        foreach (ColliderCheck s in ObjCollider)
                        {
                           
                            //Ensure is known Object Collider!
                            if (s.Name == colliderID)
                            {
                                Debug.Log("The " + s.Name + " isActivated");

                                // Below are for future changes
                                ObjCollider[i].OnExit(); // Stop Particles - execute this Once per frame

                                ObjCollider[i].OnEnter();

                                TriggerArray[i] = true;

                                break;

                            } //end if

                        } // foreach (ColliderCheck)
                    } //endif (ColliderID)


                    //Validate number of "True" here
                    if(TriggerArray[i]) { countActive += 1; }


                    //if the total number "True" is equal to the number of ObjCollider ? then mission accomplished
                    if(countActive == ObjCollider.Length)
                    {
                        allMissionsComplete = true;
                        break;   //were good so get out from the loop
                    }


                } //End For Loop( ObjCollider)

                //Final Check here
                if(allMissionsComplete)
                {
                    Debug.Log("All Orbs are Activated!");
                    isActive = true;

                    
                    //Over here for Future changes, Call Function Here to Execute Open Door Animation


                }
                else
                {
                    isActive = false;
                }



            }// Endif (!isActive) - were done here


            if (openTime < 1)
            {
                openTime += Time.deltaTime * openSpeed;
            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (allMissionsComplete ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);


        } //End update()



    } //public class ColliderManager
    
} //namespace Ironlight.PuzzleDebug
