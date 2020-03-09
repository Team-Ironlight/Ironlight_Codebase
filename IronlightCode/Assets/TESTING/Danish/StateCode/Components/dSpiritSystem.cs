using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;
using Viet.Components;

namespace Danish.Components
{


    public class dSpiritSystem : MonoBehaviour
    {
        [SerializeField] dHealthSystem HP_System = null;
        SpiritComponent SP = null;
        SpiritEffector SP_Effector = null;
        [SerializeField] private EnergyGain Gainer = null;
        [SerializeField] private EnergyDrain Drainer = null;

        [Header("Spirit Values")]
        public float maxSpirit = 100;
        public float currentSpirit = 0;
        public float currentHealth = 0;


        private void Awake()
        {
            
        }

        void Start()
        {
            SP = new SpiritComponent();
            SP.Init(maxSpirit, HP_System?.HEALTH);

            SP_Effector = new SpiritEffector();
            SP_Effector.Init(SP);

            Gainer = new EnergyGain();
            Gainer.Init(SP_Effector);

            Drainer = new EnergyDrain();
            Drainer.Init(SP_Effector);
        }


        public EnergyGain GAIN
        {
            get
            {
                if(Gainer != null)
                {
                    return Gainer;
                }

                return null;
            }
        }

        public EnergyDrain DRAIN
        {
            get
            {
                if(Drainer != null)
                {
                    return Drainer;
                }

                return null;
            }
        }

        void Update()
        {
            currentSpirit = SP.CurrentSpirit;
            currentHealth = HP_System.currentHealth;
        }
    }
}