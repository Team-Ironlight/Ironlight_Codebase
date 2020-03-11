using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;


namespace Viet.Components
{
    public class EnergyGain
    {
        public SpiritEffector spiritEffector = null;
        public float gainValue = 5;
        public float multiplier = 1;

        public void Init(SpiritEffector _spiritEffector)
        {
            spiritEffector = _spiritEffector;
        }

        public void DoIt(float amountToGain, float multi)
        {
            if(multi > 1)
            {
                multiplier = multi;
            }

            UpdateValues(amountToGain, multi);
            ProcessGain();
        }


        void UpdateValues(float value1, float value2)
        {
            if(gainValue == value1 && multiplier == value2)
            {
                return;
            }

            gainValue = value1;

            multiplier = value2;
        }

        void ProcessGain()
        {
            spiritEffector.Affect(true, gainValue);
        }


        void ResetValue()
        {
            gainValue = 5;
            multiplier = 1;
        }
    }
}