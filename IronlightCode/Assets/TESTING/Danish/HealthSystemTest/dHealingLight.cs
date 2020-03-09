using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;

public class dHealingLight : MonoBehaviour
{
    [SerializeField] private Light lightSource = null;
    [SerializeField] private dSpiritSystem PlayerSpirit = null;

    public float spiritToAdd = 5;

    public float startLightIntensity = 0;
    public float currentLightIntensity = 0;

    public float depletedLight = 0;

    public bool PlayerInLight = false;

    void Start()
    {
        lightSource = gameObject.GetComponentInChildren<Light>();

        startLightIntensity = lightSource.intensity;
    }

    void Update()
    {
        currentLightIntensity = lightSource.intensity;

        if (PlayerInLight)
        {
            if(PlayerSpirit.currentSpirit < PlayerSpirit.maxSpirit || PlayerSpirit.currentHealth < PlayerSpirit.maxHealth)
            {
                DepleteLightSource();
            }

            else
            {
                Debug.Log("Health and Spirit maxed");
                RegenerateLightSource();
            }

        }
        else
        {
            RegenerateLightSource();
        }
    }


    void RegenerateLightSource()
    {
        if(currentLightIntensity == startLightIntensity)
        {
            return;
        }

        if(currentLightIntensity < startLightIntensity)
        {
            currentLightIntensity += (spiritToAdd * Time.deltaTime);
            lightSource.intensity = currentLightIntensity;
        }

        if(currentLightIntensity > startLightIntensity)
        {
            currentLightIntensity = startLightIntensity;
            lightSource.intensity = currentLightIntensity;
        }

    }

    void DepleteLightSource()
    {
        if(currentLightIntensity == 0)
        {
            return;
        }

        if(currentLightIntensity > 0)
        {
            currentLightIntensity -= (spiritToAdd * Time.deltaTime);
            lightSource.intensity = currentLightIntensity;
            PlayerSpirit?.GAIN.DoIt(spiritToAdd * Time.deltaTime, 1);
        }

        if(currentLightIntensity < 0)
        {
            currentLightIntensity = 0;
            lightSource.intensity = currentLightIntensity;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.gameObject.TryGetComponent(out dSpiritSystem dSpirit))
            {
                PlayerSpirit = dSpirit;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(currentLightIntensity > 0)
        {
            if(other.gameObject.tag == "Player")
            {
                PlayerInLight = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerInLight = false;
            PlayerSpirit = null;
        }
    }
}
