using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    [CreateAssetMenu(fileName = "State", menuName = "AI/State")]
    public class SO_State : ScriptableObject
    {
        [SerializeField] public StateEnum state;

        //general variable holder
        [SerializeField] public float[] variables;

        //specific variables
        //projectile
        [SerializeField] public Transform launcher;
        [SerializeField] public GameObject projectile;
        [SerializeField] public SO_Projectile soProjectile;


        //creates the state based on these variables
        public IState CreateState(Transform parent, Transform target)
        {
            return EnumState.CreateState(parent, target, this);
        }
    }
}


