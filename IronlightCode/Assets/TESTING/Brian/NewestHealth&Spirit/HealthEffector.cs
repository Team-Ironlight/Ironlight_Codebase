using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace brian.Components
{



    public class HealthEffector
    {
        [SerializeField]
        bool isHealing;
        [SerializeField]
        bool isDamaged;


        HealthComponent HealthVal;

        public float defenseMultiplyer = 1;



        public void Init(HealthComponent hp)
        {
            HealthVal = hp;
        }



        public void affect(bool plusHealth, float val, float multi)
        {
            defenseMultiplyer = multi;
            float value = val * defenseMultiplyer;

            if (plusHealth)
            {
                HealthVal.addHealth(value);
            }
            else
            {
                HealthVal.subHealth(value);
            }
            Debug.Log(HealthVal.currHealth);
        }
    }
}