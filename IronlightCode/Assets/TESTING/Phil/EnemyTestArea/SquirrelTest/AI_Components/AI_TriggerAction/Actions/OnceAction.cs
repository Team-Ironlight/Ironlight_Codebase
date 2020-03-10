// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class OnceAction : Phil_ActionBase
{
    public Phil_ActionBase Action;
    private bool used = false;

    public override void Act() {
        if (!used) {
            used = true;
            Action.Act();
        }
    }
}
