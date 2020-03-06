// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class UnlockCursorAction : Phil_ActionBase
{
    public override void Act() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
