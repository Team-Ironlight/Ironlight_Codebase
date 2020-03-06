using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger Confirmed");
         
        }
    }
}
