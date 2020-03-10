using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    [CreateAssetMenu(fileName = "CC", menuName = "AI/ConditionContainer")]
    public class SO_ConditionContainer : ScriptableObject
    {
        [SerializeField] public int numberOfConditions;
        [SerializeField] public ConditionEnum[] conditionEnums;
        [SerializeField] public int numberOfVariables;
        [SerializeField] public float[] variables;


        //create the NEW condition container from this info
        //doesn't include and of the state stuff as it will no longer be
        //responsible for the transitions
        public ConditionContainer CreateConditionContainer(Transform parent, Transform target)
        {
            //name, state enum..., condition array
            ICondition[] c = new ICondition[conditionEnums.Length];
            int index = 0;
            for (int i = 0; i < numberOfConditions; i++)
            {
                //create condition
                c[i] = EnumCondition.CreateCondition(conditionEnums[i], variables, ref index, parent, target);
            }

            return new ConditionContainer(c);
        }
    }
}


