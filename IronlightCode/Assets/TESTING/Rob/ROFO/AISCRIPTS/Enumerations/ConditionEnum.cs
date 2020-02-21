using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //conditions
    public enum Condition { range, time, view }

    public class ConditionEnum
    {
        //gets the condition attached to the object
        public static ICondition SetCondition(GameObject g, Condition c)
        {
            switch (c)
            {
                case Condition.range:
                    return g.GetComponent<RangeCondition>();
                case Condition.time:
                    return g.GetComponent<TimeCondition>();
                case Condition.view:
                    return g.GetComponent<InViewCondition>();
            }

            return null;
        }

        //return ICondition based on Condition enum passed in
        //public static ICondition CreateCondition(ConditionCreator cc, int index)
        //{
        //    //return condition
        //    switch (cc.cons[index])
        //    {
        //        case Condition.range:
        //            RangeCondition range = new RangeCondition(cc.placeCount);
        //            cc.placeCount++;
        //            return range;
        //        case Condition.time:
        //            TimeCondition time = new TimeCondition(cc.placeCount);
        //            cc.placeCount++;
        //            return time;
        //        case Condition.view:
        //            InViewCondition view = new InViewCondition(cc.placeCount);
        //            cc.placeCount++;
        //            return view;
        //    }

        //    return null;
        //}
    }
}
