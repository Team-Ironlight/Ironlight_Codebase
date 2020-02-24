using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ROFO
{
    //static class that can be called by anything with string reference?
    //does all the input checking for everything else
    //does something need to be gathering input if nothing is requesting it???
    public static class InputManager
    {
        //public methods for outside access
        public static float GetAxis(string inputAxis)
        {


            return -1;
        }

        public static bool GetButton(KeyCode name)
        {
            return false;
        }
    }
}
