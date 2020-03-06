using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharmout.SO;

namespace Sharmout.attacks
{
    public class R_BeamLogic : MonoBehaviour
    {
        float beamSpeedGoing = 15;
        float beamSpeedClosing = 20;
        int beamRange = 10;
        float beamLengthGoing = 0;
        float beamLengthClosing = 0;

        public LineRenderer line;
        public Transform muzzleRef = null;
        Coroutine currentCo = null;

        // LineCast Variables
        private Vector3 lineStart = Vector3.zero;
        private Vector3 lineEnd = Vector3.zero;
        private Vector3 lineDirection = Vector3.zero;
        private float lineLength = 0;
        private RaycastHit lineHit;
        private bool hittingObj = false;

        private Vector3 posBeforeRelease = Vector3.zero;
        private Vector3 dirBeforeRelease = Vector3.zero;

        public bool going = false;
        public bool ending = false;

        List<LayerMask> layersToCheck;

        public void Init(Vector3 _start, BeamSO stats)
        {
            lineStart = _start;
            lineEnd = _start;

            beamRange = stats._beamRange;
            beamSpeedGoing = stats.speedGoing;
            beamSpeedClosing = stats.speedEnding;
            layersToCheck = stats.layersToCheck;
        }

        public void ActiveTick(Vector3 _startPoint, Vector3 _rotation)
        {
            lineStart = _startPoint;
            lineDirection = _rotation;

            BeamPositionUpdater();

            //if(PerformLineCast(lineStart, lineEnd, layersToCheck))
            //{
            //    Debug.Log("Sharmouttttttt Pussy");
            //}
        }

        public void FinishTick()
        {
            currentCo = StartCoroutine(BeamEnder());
        }

        void BeamPositionUpdater()
        {
            if (going)
            {
                GoBeam();
                if (!hittingObj)
                {
                    lineEnd = lineStart + (lineDirection.normalized * beamLengthGoing);
                    line.SetPosition(1, lineEnd);
                }

                line.SetPosition(0, lineStart);

                posBeforeRelease = lineStart;
                dirBeforeRelease = lineDirection;
            }

            else if (ending)
            {
                //currentCo = StartCoroutine(BeamEnder());
            }
        }

        void GoBeam()
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

        float CalculateDistance()
        {
            return Vector3.Distance(lineStart, lineEnd);
        }

        IEnumerator BeamEnder()
        {
            while (CalculateDistance() > 1)
            {
                FinishBeam();
                lineStart = posBeforeRelease + (dirBeforeRelease * beamLengthClosing);
                line.SetPosition(0, lineStart);

                yield return null;
            }

            yield return new WaitForEndOfFrame();

            beamLengthGoing = 0;
            beamLengthClosing = 0;

            currentCo = null;
            gameObject.SetActive(false);
        }


        public bool PerformLineCast(Vector3 start, Vector3 end, List<LayerMask> layers)
        {
            bool LCD = false;
            RaycastHit hit;

            Debug.DrawLine(start, end, Color.blue);


            foreach(var layer in layers)
            {
                if (!LCD)
                {
                    LCD = Physics.Linecast(start, end, out hit, layer);
                }
                else
                {
                    break;
                }
            }


            return LCD;
        }

        
    }

    
   
}
