using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;
using Viet.Components;

namespace Danish.Components
{
    

    public class dHealthSystem : MonoBehaviour
    {
        HealthComponent HP = null;
        HealthEffector HP_Effector = null;
        [SerializeField] private DamageComp Damager = null;
        [SerializeField] private HealComp Healer = null;

        [Header("Health Values")]
        public float maxHealth = 100;
        public float currentHealth = 0;

        private void Awake()
        {
            HP = new HealthComponent();
            HP.Init(maxHealth);
        }

        private void Start()
        {
            //HP = new HealthComponent();
            //HP.Init(maxHealth);

            HP_Effector = new HealthEffector();
            HP_Effector.Init(HP);

            Damager = new DamageComp();
            Damager.Init(HP_Effector);

            Healer = new HealComp();
            Healer.Init(HP_Effector);
        }

        public HealthComponent HEALTH
        {
            get
            {
                return HP;
            }
        }

        public DamageComp DMG
        {
            get
            {
                return Damager;
            }
        }

        public HealComp ABS
        {
            get
            {
                return Healer;
            }
        }

        private void Update()
        {
            currentHealth = HP.CurrentHealth;
        }
    }
}