using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;

namespace Viet.Components
{


    public class DamageComp
    {
        public HealthEffector healthEffector = null;
        public float damageValue = 5;
        public float CritDmg = 1;

        //private GameObject attacker = null; //attacker currently empty

        // Initializes damage component, similar to Awake/Start function
        public void Init(HealthEffector _healthEffector)
        {
            healthEffector = _healthEffector;
        }
        public void DoIt(float dmg, float crit)
        {
            UpdateValues(dmg, crit);
            processDmg();
            resetValue();
        }

        void UpdateValues(float value1, float value2)
        {
            damageValue = value1;
            CritDmg = value2;
        }


        void processDmg() // Access the health value of the defender and deal dmg or recover hp by the attacker dmg or heal by amount of heal source
        {
            healthEffector.affect(false, damageValue, CritDmg);           
        }

        void resetValue() // aftter take damage from dmg component, reset the dmg value receive and attacker value
        {
            damageValue = 0;
            CritDmg = 1;
            //attacker = null;
        }
    }


    public class AbsorbComp
    {
        public HealthEffector healthEffector = null;
        public float absorbValue = 5;
        public float bonusHealth = 1;

        //private GameObject attacker = null; //attacker currently empty

        // Initializes damage component, similar to Awake/Start function
        public void Init(HealthEffector _healthEffector)
        {
            healthEffector = _healthEffector;
        }
        public void DoIt(float dmg, float crit)
        {
            UpdateValues(dmg, crit);
            processDmg();
            resetValue();
        }

        void UpdateValues(float value1, float value2)
        {
            absorbValue = value1;
            bonusHealth = value2;
        }


        void processDmg() // Access the health value of the defender and deal dmg or recover hp by the attacker dmg or heal by amount of heal source
        {
            healthEffector.affect(true, absorbValue, bonusHealth);
        }

        void resetValue() // aftter take damage from dmg component, reset the dmg value receive and attacker value
        {
            absorbValue = 0;
            bonusHealth = 1;
            //attacker = null;
        }
    }
}
