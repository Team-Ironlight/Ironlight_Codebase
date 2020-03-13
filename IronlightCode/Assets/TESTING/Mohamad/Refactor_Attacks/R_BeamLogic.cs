﻿using System.Collections;
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

        //public LineRenderer line;
        //public Transform muzzleRef = null;
        Coroutine currentCo = null;

        public GameObject beamStart = null;
        public GameObject beamLoop = null;

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

            var vfx = beamStart.GetComponent<ParticleSystem>();
            vfx.time = 0;
            vfx.Play();

            var vfxLoop = beamLoop.GetComponentsInChildren<ParticleSystem>();
            foreach (var fx in vfxLoop)
            {
                fx.time = 0;
                fx.Play();
            }
        }

        public void ActiveTick(Vector3 _startPoint, Vector3 _fireDirection)
        {
            lineStart = _startPoint;
            lineDirection = _fireDirection;

            BeamPositionUpdater();

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
                    //line.SetPosition(1, lineEnd);
                }
                Vector3 direction = (lineEnd - lineStart);

                transform.position = lineStart;
                transform.forward = lineDirection;
                //line.SetPosition(0, lineStart);

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
                //line.SetPosition(0, lineStart);

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
