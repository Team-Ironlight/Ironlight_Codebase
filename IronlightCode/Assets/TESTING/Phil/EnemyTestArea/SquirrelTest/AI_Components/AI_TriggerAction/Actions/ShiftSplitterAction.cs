// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class ShiftSplitterAction : Phil_ActionBase
{
    public Phil_ActionBase ShiftAction;
    public Phil_ActionBase StandardAction;

    public override void Act() {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            ShiftAction.Act();
        } else {
            StandardAction.Act();
        }
    }
}
