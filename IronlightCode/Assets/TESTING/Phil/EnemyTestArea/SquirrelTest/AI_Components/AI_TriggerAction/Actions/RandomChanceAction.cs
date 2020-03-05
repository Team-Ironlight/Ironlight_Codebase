// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class RandomChanceAction : Phil_ActionBase
{
    public int PercentageChance;
    public Phil_ActionBase Action;

    public override void Act() {
        if (Random.Range(0, 100) <= PercentageChance) {
            Action.Act();
        }
    }
}
