using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components.Abstract;

namespace Danish.Components.SO
{
    [CreateAssetMenu(fileName = "Health Component.asset", menuName = "Components/Health")]
    public class dHealth_SO : dBaseComponent
    {
        public float maxHealth = 100;

        [SerializeField]
        private float currHealth;


        public override void Init()
        {
            base.Init();
        }


        public void Init(float num)
        {
            maxHealth = num;
            currHealth = maxHealth;
        }

        public float GetCurrentHealth()
        {
            return currHealth;
        }

        public float addHealth(float val)
        {
            float result = 0;
            if (currHealth + val >= maxHealth)
            {
                result = (currHealth + val) - maxHealth;
                currHealth = maxHealth;
            }
            else
            {
                currHealth += val;
            }

            return result;

            // print("current health is " + HealthVal.currHealth);
        }



        public void subHealth(float val)
        {
            if (currHealth - val <= 0)
            {
                currHealth = 0;
            }
            else
            {
                currHealth -= val;
            }
            // print("current health is " + HealthVal.currHealth);
        }
    }
}