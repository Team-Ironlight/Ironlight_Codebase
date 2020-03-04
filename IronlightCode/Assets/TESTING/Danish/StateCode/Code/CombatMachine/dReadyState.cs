using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;

namespace Danish.StateCode
{

    public class dReadyState : dCombatBaseState
    {
        private dStateManager Manager;

		private dPowerWheel dPowerComponent = null;

        public dReadyState(dStateManager _stateManager) : base(_stateManager.obj)
        {
            base.MainManager = _stateManager;

            if (Manager != base.MainManager)
            {
                Manager = base.MainManager;
            }

			dPowerComponent = Manager.dPower;
			dPowerComponent.Init();

		}


        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override Type Tick()
        {
            Debug.Log("Ready To Attack");

			dPowerComponent.Tick(Manager.scrollUp, Manager.scrollDown);

			//WORKS BUT NEED TWEAKING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

			//if (dPowerComponent.OrbActive && Manager.launchOrb)
			//{
			//	Manager.launchOrb = false;
			//	return typeof(dOrbState);
			//}
			//else if (dPowerComponent.BeamActive && Manager.launchBeam)
			//{
			//	return typeof(dBeamState);
			//}
			//else if (dPowerComponent.BlastActive && Manager.launchBlast)
			//{
			//	return typeof(dBeamState);
			//}

			//To be changed
			if (Manager.launchOrb)
			{
				Manager.launchOrb = false;
				return typeof(dOrbState);
			}
			else if (Manager.launchBeam)
			{
				return typeof(dBeamState);
			}
			else if (Manager.launchBlast)
			{
				return typeof(dBlastState);
			}

			return null;
        }
    }
}