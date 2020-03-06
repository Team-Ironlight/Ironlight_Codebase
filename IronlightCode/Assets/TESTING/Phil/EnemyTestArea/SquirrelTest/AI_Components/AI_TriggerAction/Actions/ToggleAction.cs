// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class ToggleAction : Phil_ActionBase
{
    public bool Toggled = false;
    public Phil_ActionBase EnabledAction;
    public Phil_ActionBase DisabledAction;

    public override void Act() {
        Toggled = !Toggled;
        if (Toggled) {
            EnabledAction.Act();
        } else {
            DisabledAction.Act();
        }
    }
}
