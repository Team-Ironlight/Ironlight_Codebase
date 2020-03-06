// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System;
using System.Collections;

public class CallbackAction : Phil_ActionBase
{
    private Action callback;

    public void Initialize(Action callback) {
        this.callback = callback;
    }

    public override void Act() {
        callback();
    }
}
