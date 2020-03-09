// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class CooldownAction : Phil_ActionBase
{
    public float CooldownDelay;
    public Phil_ActionBase Action;
    public bool InCooldown = false;

    public override void Act() {
        if (!InCooldown) {
            Action.Act();
            InCooldown = true;
            StartCoroutine(ClearCooldown());
        }
    }

    IEnumerator ClearCooldown() {
        yield return new WaitForSeconds(CooldownDelay);
        InCooldown = false;
    }
}
