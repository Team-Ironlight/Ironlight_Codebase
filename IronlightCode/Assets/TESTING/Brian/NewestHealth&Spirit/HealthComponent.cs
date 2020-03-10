using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace brian.Components
{

    [System.Serializable]
    public class HealthComponent
    {
        private float maxHealth = 100;

        [SerializeField]
        private float currHealth = 0;

        public float CurrentHealth
        {
            get
            {
                return currHealth;
            }
        }

        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
        }


        public void Init(float _MaxHealth)
        {
            maxHealth = _MaxHealth;
            currHealth = maxHealth;
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
        }
    }
}