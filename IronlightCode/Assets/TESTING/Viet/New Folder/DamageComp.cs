using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComp : MonoBehaviour
{
    public float damageValue; 

    private GameObject attacker = null; //attacker currently empty
   

    void Update ()
    {

    }

    void processDmg() // Access the health value of the defender and deal dmg or recover hp by the attacker dmg or heal by amount of heal source
    {

    }

    void resetValue() // aftter take damage from dmg component, reset the dmg value receive and attacker value
    {
        damageValue = 0;
        attacker = null;
    }
}
