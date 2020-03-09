using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronLight;


namespace brian.Components
{



    [System.Serializable]
    public class SpiritComponent 
    {
        HealthComponent HP;
        [SerializeField] private float currSpirit;
        [SerializeField] private float maxSpirit;

        public float CurrentSpirit
        {
            get
            {
                return currSpirit;
            }
        }

        public float MaxSpirit
        {
            get
            {
                return maxSpirit;
            }
        }


        public void Init(float _MaxSpirit, HealthComponent _HP)
        {
            maxSpirit = _MaxSpirit;
            currSpirit = maxSpirit;
            HP = _HP;
        }



        public void subSpirit(float val)
        {
            currSpirit -= val;

            //this will subtract the remaing value from the health
            if (currSpirit <= 0)
            {
                HP.subHealth(currSpirit *-1 );
                currSpirit = 0;
            }

            //Debug.Log(currSpirit + " and Current health " + he.CurrentHealth);
        }



        public void addSpirit(float val)
        {
            float remainder = 0;

            if (HP.CurrentHealth < HP.MaxHealth)
            {
                remainder = HP.addHealth(val);
            }            
        
            else if(HP.CurrentHealth >= HP.MaxHealth)
            {
                currSpirit += val;

                if (currSpirit >= maxSpirit)
                {
                    currSpirit = maxSpirit;
                }
            }

            if (remainder > 0)
            {
                currSpirit += remainder;
            }

        }
    }
}