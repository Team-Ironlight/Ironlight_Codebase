using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public PLY_HealthComponent PlayerHealth;
    float StartLightIntensity;
    float currLightIntensity;
    public float LightDepleted;

    public float RateOfDepletion = 3f;

    private Light spotLight;
    [SerializeField]
    bool absorbLight = false;

    public AudioSource sound;
    public AudioClip soundToPlay;
    public float volume;
    bool clipPlayed = false;


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
            if (currLightIntensity <= 0)
            {
                sound.Stop();
            }
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
            sound.PlayOneShot(soundToPlay, volume);
            print("Absorb");
            spotLight.intensity -= Time.deltaTime*RateOfDepletion;
            //StartLightIntensity -= Time.deltaTime * 2;

        }
    }

    void LightRegen()
    {
        if (currLightIntensity < StartLightIntensity)
        {
            spotLight.intensity += Time.deltaTime;

        }
                 
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && spotLight.intensity!=0)
        {
            sound.PlayOneShot(soundToPlay, volume);
            print("Absorbing");
            PlayerHealth.currentHealth += 5 * Time.deltaTime;
            PlayerHealth.CurrSpirit += 5 * Time.deltaTime;
            absorbLight = true;
            other.GetComponent<LightCharging>().isCharging = true;
			if (PlayerHealth.currentHealth > PlayerHealth.maxHealth)
			{
				PlayerHealth.currentHealth = PlayerHealth.maxHealth;
			}
			if (PlayerHealth.CurrSpirit > PlayerHealth.maxSpirit)
			{
				PlayerHealth.CurrSpirit = PlayerHealth.maxSpirit;
			}
		}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sound.Stop();
            other.GetComponent<LightCharging>().isCharging = false;
            absorbLight = false;
        }
    }
}
