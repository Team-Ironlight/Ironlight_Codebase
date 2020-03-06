// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class EnterTriggerTrigger : MonoBehaviour {
    public Phil_ActionBase Action;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player"))
        {
             Debug.Log("Trigger Confirmed");
            Action.Act();
        }
    }
}
