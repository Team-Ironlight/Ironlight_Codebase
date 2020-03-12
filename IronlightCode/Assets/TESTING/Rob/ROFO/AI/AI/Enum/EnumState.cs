using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AITEST
{
    public enum StateEnum { None, Alert, FocusOn, Projectile, MoveForward }
    public static class EnumState
    {
        //return the correct state given the state enum
        public static IState CreateState(Transform parent, Transform target, SO_State so)
        {
            switch (so.state)
            {
                case StateEnum.None:
                    return new State_None();
                case StateEnum.Alert:
                    return new State_Alert(parent, so.variables[0],
                                           so.variables[1],
                                           so.variables[2],
                                           so.variables[3]);
                case StateEnum.FocusOn:
                    return new State_FocusOn(parent, target, so.variables[0]);
                case StateEnum.Projectile:
                    return new State_Projectile(so.launcher, target, so.projectile, so.soProjectile,
                                                so.variables[0], so.variables[1]);
                case StateEnum.MoveForward:
                    return new State_MoveForward(parent, target, so.variables[0]);
                default:
                    return null;
            }
        }
    }
}


