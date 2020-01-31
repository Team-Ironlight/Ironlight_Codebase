using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_CrystalCollisions : MonoBehaviour
{
    public TestDanish_RotateCrystal crystal;

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
