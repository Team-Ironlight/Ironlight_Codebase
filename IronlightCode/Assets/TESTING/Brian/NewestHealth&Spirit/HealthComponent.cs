using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HealthComponent 
{
    public float maxHealth = 100;

    [SerializeField]
    public float currHealth;



    public void Init()
    {
        currHealth = maxHealth;
    }
    public float addHealth(float val)
    {
        float result = 0;
        if (currHealth + val >= maxHealth)
        {
            result = (currHealth + val) - maxHealth; 
            currHealth = maxHealth;
        }
        else
        {
            currHealth += val;
        }

        return result;
       
        // print("current health is " + HealthVal.currHealth);
    }
    public void subHealth(float val)
    {
        if (currHealth - val <= 0)
        {
            currHealth = 0;
        }
        else
        {
            currHealth -= val;
        }
        // print("current health is " + HealthVal.currHealth);
    }
}
