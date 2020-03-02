using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Danish.Custom;

namespace Danish.Components
{


    public class dCrystalTrigger
    {
        

        public void Init()
        {
            
        }

        public void Tick(bool shouldInteract)
        {


            if (!shouldInteract)
            {
                return;
            }

            
            PuzzleInteractionManager.CallTriggers();

            //shouldInteract = false;
        }
    }
}