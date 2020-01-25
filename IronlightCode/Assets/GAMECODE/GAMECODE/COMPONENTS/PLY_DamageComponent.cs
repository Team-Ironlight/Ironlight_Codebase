using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_DamageComponent : MonoBehaviour
{
    /*Script With Damage value*/

    
    public PLY_HealthComponent _defender;

    float _adjustedDamage;

    //void setDefender()
    //{

    //}


    void calculateDamageFunction(float _entityHealth, float _damageAmount, float _defenseVal)
    {
        _defenseVal = _defender.defValue;
        _adjustedDamage -= _damageAmount / _defenseVal;
        //_damageAmount = _attacker./*DamageValue*/;
        //_entityHealth = _defender.currentHealth;



        
            _defender.SubHealth(_adjustedDamage);


        //_adjustedHealth = _entityHealth;
        //_defender.subHealth(adjustedHealth);
    }



   
}
