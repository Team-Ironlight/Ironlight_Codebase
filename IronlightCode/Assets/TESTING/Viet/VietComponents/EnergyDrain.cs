using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;


namespace Viet.Components
{
    public class EnergyDrain
    {
        public SpiritEffector spiritEffector = null;
        public float drainValue = 5;
        public float multiplier = 1;

        public void Init(SpiritEffector _spiritEffector)
        {
            spiritEffector = _spiritEffector;
        }

        public void DoIt(float amountToDrain, float multi)
        {
            if (multi > 1)
            {
                multiplier = multi;
            }

            UpdateValues(amountToDrain, multi);

            ProcessDrain();
        }


        void UpdateValues(float value1, float value2)
        {
            if (drainValue == value1 && multiplier == value2)
            {
                return;
            }

            drainValue = value1;

            multiplier = value2;
        }

        void ProcessDrain()
        {
            spiritEffector.Affect(false, drainValue);
        }


        void ResetValue()
        {
            drainValue = 5;
            multiplier = 1;
        }
    }
}