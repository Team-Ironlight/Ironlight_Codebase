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

        public float Multiplyer;

        // Start is called before the first frame update
        public void Init()
        {
            HealthVal = new HealthComponent();
            HealthVal.Init();
        }


        //void addHealth(float val)
        //{
        //    HealthVal.currHealth += val;
        //   // print("current health is " + HealthVal.currHealth);
        //}
        //void subHealth(float val)
        //{
        //    HealthVal.currHealth -= val;
        //   // print("current health is " + HealthVal.currHealth);
        //}
        public void affect(bool plusHealth, float val, float multi)
        {
            Multiplyer = multi;
            float value = val * Multiplyer;

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