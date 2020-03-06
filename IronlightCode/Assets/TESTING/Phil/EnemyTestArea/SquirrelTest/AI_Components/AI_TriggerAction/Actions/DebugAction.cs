// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class DebugAction : Phil_ActionBase
{
    public override void Act() {
        Debug.Log("Action is firing on " + gameObject);
    }
}
