// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class isOnSafeZone : Phil_ActionBase
{
    //public GameObject GameObject;
    // SetLightActive setLight;
    public bool sendValue;

     public override void Act()
     {
        SetLightActive setLight = GetComponent<SetLightActive>();
        setLight.isPlayerCharging = sendValue;

    }
}
