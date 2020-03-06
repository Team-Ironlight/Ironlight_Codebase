// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class DelayedAction : Phil_ActionBase
{
    public float Delay = 1f;
    public Phil_ActionBase Action;

    public override void Act() {
        StartCoroutine(DelayAction());
    }

    IEnumerator DelayAction() {
        yield return new WaitForSeconds(Delay);
        Action.Act();
    }
}
