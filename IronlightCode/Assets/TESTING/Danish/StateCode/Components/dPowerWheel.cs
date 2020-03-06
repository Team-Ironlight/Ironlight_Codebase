using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;


namespace Danish.Components
{
	public class dPowerWheel
	{
		public GameObject PWS;
		//public float scrollSpeed = 5;
		//public GameObject rotatingObject;
		public bool OrbActive;
		public bool BeamActive;
		public bool BlastActive;


        public enum ActivePower
        {
            None,
            Orb,
            Beam,
            Blast
        }

        public ActivePower activePower = ActivePower.None;

		float scrollCount = 0;
		float activeAbility;

		// Start is called before the first frame update
		public void Init()
		{
			PWS = GameObject.Find("Canvas/PowerWheelLatest/Icon Holder");
		}

		// Update is called once per frame
		public void Tick(bool scrollup, bool scrolldown)
		{
			if (!scrollup && !scrolldown)
			{
				//Debug.Log("no Scroll Input");
			}
            else if (scrolldown && !scrollup)//Scroll values are not changing!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			{
				Debug.Log("Scroll -ve");
				//Update the scroll -ve
				scrollCount -= 1;
			}
			else if (!scrolldown && scrollup)
			{
				Debug.Log("Scroll +ve");
				//update the scroll +ve
				scrollCount += 1;
			}

			//Debug.Log("poweerWHWEEEEEEEE");

			activeAbility = Mathf.Abs(scrollCount % 3);

			//Activate Ability here
			AbilityActivated(activeAbility);

            Debug.Log(activePower.ToString());

			//Rotate UI Wheel
			//This is not working right now......................................................................
			if (PWS!=null)
			{
				PWS.GetComponent<PowerWheelScroll>().RotateWheelFunc(activeAbility);
			}
			else
			{
				//Debug.Log("PWS NOT FOUND!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			}


		}

		public void AbilityActivated(float activeAbility)
		{
			switch (activeAbility)
			{
				case 0:
					//Debug.Log("Ability 1!");
					//Debug.Log("Orb On!");
                    activePower = ActivePower.Orb;
					break;
				case 1:
					//Debug.Log("Ability 2!");
					//Debug.Log("Beam On!");
                    activePower = ActivePower.Beam;
                    break;
				case 2:
					//Debug.Log("Ability 3!");
					//Debug.Log("Blast On!");
                    activePower = ActivePower.Blast;
                    break;
			}
		}

        //public void AbilityActivated(float activeAbility)
        //{
        //    switch (activeAbility)
        //    {
        //        case 0:
        //            Debug.Log("Ability 1!");
        //            Debug.Log("Orb On!");
        //            OrbActive = true;
        //            BeamActive = false;
        //            BlastActive = false;
        //            break;
        //        case 1:
        //            Debug.Log("Ability 2!");
        //            Debug.Log("Beam On!");
        //            BeamActive = true;
        //            OrbActive = false;
        //            BlastActive = false;
        //            break;
        //        case 2:
        //            Debug.Log("Ability 3!");
        //            Debug.Log("Blast On!");
        //            BlastActive = true;
        //            OrbActive = false;
        //            BeamActive = false;
        //            break;
        //    }
        //}
    }
}
