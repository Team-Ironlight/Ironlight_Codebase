 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_HealthComponent : MonoBehaviour
{
    // Programmer: Ron
    // Additional Programmer: Danish
    // Description: Modular component to manage Health for all applicable entities.

    [Header("Variables")]
    public float currentHealth;
    float maxHealth;
    public float defValue;
    float maxMana;
    public float CurrMana;

    // Code to Initialize Health Component
    public void Init(int _maxHealth, float _defValue, float _MaxMana)
    {
        // Cache the _maxHealth value in a local variable
        maxHealth = _maxHealth;

        // Set currentHealth equal to the cached maxHealth value
        currentHealth = maxHealth;

        // Cache the _defValue in a local variable
        defValue = _defValue;

        maxMana = _MaxMana;

        CurrMana = maxMana;
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

    public void SubManaInst(float value)
    {
        if ((CurrMana - value) <= 0)
        {
            CurrMana = 0;
        }
        else
        {
            CurrMana -= value;
        }
    }
    public void SubManaTime(float value)
    {
        if ((CurrMana - value) <= 0)
        {
            CurrMana = 0;
        }
        else
        {
            CurrMana -= value * Time.deltaTime;
        }
    }
}
