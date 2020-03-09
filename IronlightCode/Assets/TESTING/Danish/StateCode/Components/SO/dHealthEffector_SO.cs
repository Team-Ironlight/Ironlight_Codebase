using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components.Abstract;

namespace Danish.Components.SO
{

    [CreateAssetMenu(fileName = "Health Effector.asset", menuName = "Components/HP Effector")]
    public class dHealthEffector_SO : dBaseComponent
    {
        [SerializeField]
        bool isHealing;
        [SerializeField]
        bool isDamaged;


        dHealth_SO HealthVal;

        public float defenseMultiplyer = 1;



        public void Init(dHealth_SO hp)
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
            Debug.Log(HealthVal.GetCurrentHealth());
        }
    }
}