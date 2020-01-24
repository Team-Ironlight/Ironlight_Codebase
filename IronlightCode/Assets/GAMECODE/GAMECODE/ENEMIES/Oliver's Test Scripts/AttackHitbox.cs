using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private int damage = 40;

    private void OnTriggerEnter(Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<IAttributes>();

        //If collided with the player model, player takes damage
        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(damage, true);
        }
    }
}
