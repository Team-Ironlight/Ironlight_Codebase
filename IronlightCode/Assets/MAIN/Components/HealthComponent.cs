using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    // Programmer: Ron
    // Additional Programmer:
    // Description: Modular component to manage Health for all applicable entities.
 
    // Variables
    public float currentHealth { get; private set; }
    float maxHealth = 100;
    public float defValue = 0.5f;

    // Code to Initialize Health Component
    void Init()
    {
        // Set currentHealth to maxHealth
        currentHealth = maxHealth;
    }

    // Code to Add Health 
    public void AddHealth(float value)
    {
       // If currentHealth plus value is greater than maxHealth, set it to maxHealth
       if((currentHealth + value) > maxHealth)
        {
            currentHealth = maxHealth;
        }
       // else, add the value to currentHealth
        else
        {
            currentHealth += value;
        }
    }

    // Code to Subtract Health
    public void SubHealth(float value)
    {
        // Calculate damage amount by multiplying value by defValue
        float damage = value * defValue;

        // If currentHealth minus value is less than 0, set it to 0
        if ((currentHealth - damage) < 0)
        {
            currentHealth = 0;
        }
        // else, subtract the damage from the currentHealth
        else
        {
            currentHealth -= damage;
        }
    }
}
