using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronLight;


namespace brian.Components
{



    [System.Serializable]
    public class SpiritComponent 
    {
        public float maxSpirit = 100;
        HealthComponent he;
        [SerializeField]
        public float currSpirit;
        // Start is called before the first frame update
        public void Init()
        {
            //    he = new HealthComponent();
            //    he.Init();
            currSpirit = maxSpirit;
        }
        public void subSpirit(float val)
        {
            currSpirit -= val;
            //this will subtract the remaing value from the health
            if (currSpirit <= 0)
            {
                he.subHealth(currSpirit*-1);
                currSpirit = 0;
            }
            Debug.Log(currSpirit + " and Current health " + he.currHealth);
        }

        public void addSpirit(float val)
        {
            float remainder = 0;

            if (he.currHealth < he.maxHealth)
            {
                remainder = he.addHealth(val);
                //if (he.currHealth >= he.maxHealth)
                //{

            }            //    he.currHealth = 100;
            //}
        
            else if(he.currHealth >= he.maxHealth)
            {
                currSpirit += val;

                if (currSpirit >= maxSpirit)
                {
                    currSpirit = maxSpirit;
                }
            }

            //else
            //{

            //    //if (currSpirit >=maxSpirit)
            //    //{
            //    //    currSpirit = maxSpirit;
            //    //}
            //}


            if (remainder > 0)
            {
                currSpirit += remainder;
            }

            Debug.Log(currSpirit + " and Current health " + he.currHealth);
        }
    }
}