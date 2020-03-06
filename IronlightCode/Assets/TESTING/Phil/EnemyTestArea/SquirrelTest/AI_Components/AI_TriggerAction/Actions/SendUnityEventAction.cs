// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using UnityEngine.Events;

public class SendUnityEventAction : Phil_ActionBase
{
    public UnityEvent OnAct;

    public override void Act() {
        OnAct.Invoke();
    }
}
