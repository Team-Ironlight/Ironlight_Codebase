using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    void HitWithLight(float pAmount);
    void EnterHitWithLight(float pAmount);
    void ExitHitWithLight();
}
