using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttributes
{
    bool IsDead();
    bool TakeDamage(int damage, bool react);
    void Respawn();
}
