using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //sets player controls for camera
    public class CameraSetup : MonoBehaviour
    {
        //enum to set camera controller
        public enum CameraController { Keyboard, Mouse };
        public CameraController currentCamera;


        //make it so players can decide the arrow directions
        public enum ControllerSetup { normal, inverted, invertedX, invertedY };
        public ControllerSetup currentSetup;

        //various setups for controllers stored as dictionary
        private Dictionary<ControllerSetup, Vector2> setup = new Dictionary<ControllerSetup, Vector2>()
        {
            { ControllerSetup.normal, new Vector2(1f, -1f) },
            { ControllerSetup.inverted, new Vector2(-1f, 1f) },
            { ControllerSetup.invertedX, new Vector2(-1f, -1f) },
            { ControllerSetup.invertedY, new Vector2(1f, 1f) },

        };
    }    
}
