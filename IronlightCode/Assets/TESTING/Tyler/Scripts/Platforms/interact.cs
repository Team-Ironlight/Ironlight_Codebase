using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class interact : MonoBehaviour
{
    [SerializeField] private Image customImage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            customImage.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            customImage.enabled = false;
        }
    }

}
