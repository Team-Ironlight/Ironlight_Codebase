using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;

namespace Viet.Components
{
	public class HealComp
	{
		public HealthEffector healthEffector = null;
		public float absorbValue = 5;
		public float multiplier = 1;

		// Initializes damage component, similar to Awake/Start function
		public void Init(HealthEffector _healthEffector)
		{
			healthEffector = _healthEffector;
		}
		public void DoIt(float amountToHeal, float multi)
		{
			if(multi > 1)
			{
				multiplier = multi;
			}


			UpdateValues(amountToHeal, multi);
			processDmg();
			//resetValue();
		}

		void UpdateValues(float value1, float value2)
		{
			if(absorbValue == value1 && multiplier == value2)
			{
				return;
			}


			absorbValue = value1;
			multiplier = value2;
		}


		void processDmg() // Access the health value of the defender and deal dmg or recover hp by the attacker dmg or heal by amount of heal source
		{
			healthEffector.affect(true, absorbValue, multiplier);
		}

		void resetValue() // after take damage from dmg component, reset the dmg value receive and attacker value
		{
			absorbValue = 5;
			multiplier = 1;
			//attacker = null;
		}
	}
}

