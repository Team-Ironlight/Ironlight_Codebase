// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class RemoveGameObjectsAction : Phil_ActionBase
{
    public GameObject[] GameObjects;

    public override void Act() {
        foreach (var gameObject in GameObjects) {
            Destroy(gameObject);
        }
    }
}
