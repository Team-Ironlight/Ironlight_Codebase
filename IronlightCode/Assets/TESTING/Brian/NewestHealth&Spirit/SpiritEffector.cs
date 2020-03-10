using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace brian.Components
{
    public class SpiritEffector 
    {
        SpiritComponent SpiritVal = null;

        public void Init(SpiritComponent SP)
        {
            SpiritVal = SP;
        }

        public void Affect(bool plusSpirit, float value)
        {
            if (plusSpirit)
            {
                SpiritVal.addSpirit(value);
            }
            else
            {
                SpiritVal.subSpirit(value);
            }
        }
    }
}