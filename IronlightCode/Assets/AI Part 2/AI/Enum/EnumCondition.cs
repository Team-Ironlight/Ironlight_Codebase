using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    //all possible conditions, so far...
    public enum ConditionEnum { RangeIn, RangeOut, ViewIn, ViewOut,
                                TimeMin, TimeMax }

    public static class EnumCondition
    {
        //specifically for creating conditions from SO_ConditionContainers
        public static ICondition CreateCondition(ConditionEnum ce, float[] variables, ref int index,
                                                 Transform parent, Transform target)
        {
            switch (ce)
            {
                case ConditionEnum.RangeIn:
                    index++;
                    return new RangeIn(parent, target, variables[index - 1]);
                case ConditionEnum.RangeOut:
                    index++;
                    return new RangeOut(parent, target, variables[index - 1]);
                case ConditionEnum.ViewIn:
                    index++;
                    return new ViewIn(parent, target, variables[index - 1]);
                case ConditionEnum.ViewOut:
                    index++;
                    return new ViewOut(parent, target, variables[index - 1]);
                case ConditionEnum.TimeMin:
                    index++;
                    return new TimeMin(variables[index - 1]);
                case ConditionEnum.TimeMax:
                    index++;
                    return new TimeMax(variables[index - 1]);
                default:
                    return null;
            }
        }
    }
}


