using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalEmission : MonoBehaviour
{
    public Material mat;
    public string collisionTag;
    public string emissionString;
    public float emissionMax = 1f;
    public float speed = 1f;
    private float emissionCurrent = 0f;

    private void Start()
    {        
        mat.SetFloat(emissionString, emissionCurrent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == collisionTag)
        {
            TurnOn();
        }        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == collisionTag)
        {
            TurnOn();
        }
    }


    private bool isOn = false;
    private Coroutine c = null;
    //method to call for emission to go from 0 to set value
    public void TurnOn()
    {
        Debug.Log("Turn ON");
        if(isOn == false)
        {
            Debug.Log("Worked");
            isOn = true;
            c = StartCoroutine(On());
        }
    }

    IEnumerator On()
    {
        while(emissionCurrent < emissionMax)
        {
            emissionCurrent += speed * Time.deltaTime;
            mat.SetFloat(emissionString, emissionCurrent);
            //Debug.Log(mat.GetFloat("_EmissionIntensity"));
            yield return null;
        }

        emissionCurrent = emissionMax;
        mat.SetFloat(emissionString, emissionCurrent);       
    }
}
