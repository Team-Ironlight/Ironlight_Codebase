using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sharmout.attacks
{
    public class R_BeamLogic : MonoBehaviour
    {
        float beamSpeedGoing = 0;
        float beamSpeedClosing = 0;
        int beamRange = 10;
        float beamLengthGoing = 0;
        float beamLengthClosing = 0;

        public LineRenderer line;

        void Start()
        {
            
        }

        void Update()
        {
            StartBeam();
            
        }

        void BeamPositionUpdater()
        {

        }

        void StartBeam()
        {
            if (beamLengthGoing < beamRange)
            {
                beamLengthGoing += beamSpeedGoing * Time.deltaTime;
            }

        }

        void FinishBeam()
        {
            beamLengthClosing += beamSpeedClosing * Time.deltaTime;
        }
    }
}