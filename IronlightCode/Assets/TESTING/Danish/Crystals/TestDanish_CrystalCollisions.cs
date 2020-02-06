using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_CrystalCollisions : MonoBehaviour, IHit
{
    public TestDanish_RotatingCrystal crystal;

    

    public void HitWithLight(float pAmount)
    {
        crystal.lineActive = true;
    }
    public void EnterHitWithLight(float pAmount)
    {

    }
    public void ExitHitWithLight()
    {
        crystal.lineActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            crystal.playerCanActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            crystal.playerCanActivate = false;
        }
    }
}
