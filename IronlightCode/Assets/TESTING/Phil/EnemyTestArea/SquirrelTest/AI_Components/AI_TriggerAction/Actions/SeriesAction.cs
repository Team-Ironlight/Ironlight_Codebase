// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class SeriesAction : Phil_ActionBase
{
    public Phil_ActionBase[] Actions;

    public override void Act() {
        foreach (var action in Actions) {
            action.Act();
        }
    }
}
