using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    PLY_HealthComponent PlayerHealth;
    float StartLightIntensity;
    float currLightIntensity;
    public float LightDepleted;

    public float RateOfDepletion = 3f;

    private Light spotLight;
    [SerializeField]
    bool absorbLight = false;
    // Start is called before the first frame update
    void Start()
    {
        spotLight = GetComponentInParent<Light>();
        StartLightIntensity = spotLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        currLightIntensity = spotLight.intensity;
        if (absorbLight)
        {

            LightAbsorb();
        }
        else
        {
            LightRegen();
        }
    }
    void LightAbsorb()
    {
        if (currLightIntensity > LightDepleted)
        {
            print("Absorb");
            spotLight.intensity -= Time.deltaTime*RateOfDepletion;
            //StartLightIntensity -= Time.deltaTime * 2;
            //PlayerHealth.currentHealth += Time.deltaTime;
        }
    }

    void LightRegen()
    {
        if (currLightIntensity < StartLightIntensity)
        {
            spotLight.intensity += Time.deltaTime;

        }
                 
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            print("Absorb");
            absorbLight = true;
            other.GetComponent<LightCharging>().isCharging = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<LightCharging>().isCharging = false;
            absorbLight = false;
        }
    }
}
