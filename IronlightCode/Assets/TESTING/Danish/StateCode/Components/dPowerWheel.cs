using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;


namespace Danish.Components
{
	public class dPowerWheel
	{
		public PowerWheelScroll PWS;
		public bool OrbActive;
		public bool BeamActive;
		public bool BlastActive;

		float scrollCount;
		float activeAbility;

		// Start is called before the first frame update
		//public void Init()
		//{

		//}

		// Update is called once per frame
		public void Tick(bool scrollup, bool scrolldown)
		{
			if (!scrollup && !scrolldown)
			{
				return;
			}else if (scrolldown && !scrollup)
			{
				//Update the scroll -ve
				scrollCount -= 1;
			}
			else if (!scrolldown && scrollup)
			{
				//update the scroll +ve
				scrollCount += 1;
			}

			Debug.Log("poweerWHWEEEEEEEE");

			activeAbility = Mathf.Abs(scrollCount % 3);

			//Rotate UI Wheel
			PWS.RotateWheelFunc(activeAbility);

		}

		public void AbilityActivated(int activeAbility)
		{
			switch (activeAbility)
			{
				case 0:
					Debug.Log("Ability 1!");
					Debug.Log("Orb On!");
					OrbActive = true;
					break;
				case 1:
					Debug.Log("Ability 2!");
					Debug.Log("Beam On!");
					BeamActive = true;
					break;
				case 2:
					Debug.Log("Ability 3!");
					Debug.Log("Blast On!");
					BlastActive = true;
					break;
			}
		}


}
}
