using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_PowerController : MonoBehaviour
{
    public MOD_orb orbTest;
    public MOD_beam beamTest;
    public PLY_ImanBlastTest blastTest;
    public GameObject firePoint;

    public PLY_HealthComponent Spirit;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("Muzzle");

    }

    // Update is called once per frame
    void Update()
    {
        OrbDrain();
        BeamDrain();
        BlastDrain();
    }
    void OrbDrain()
    {
        if (orbTest.shotFired == true)
        {
            if (Spirit.CurrMana > 0)
            {
                Spirit.SubManaInst(5);
                print("Spirit Remaining: " + Spirit.CurrMana);
            }
            else if (Spirit.CurrMana <= 0)
            {
                Spirit.SubHealth(5);
                print("Health Remaining: " + Spirit.currentHealth);
            }
        }
    }
    void BeamDrain()
    {
        if (beamTest.StartAttack == true)
        {
            if (Spirit.CurrMana > 0)
            {
                Spirit.SubManaTime(2);
                print("Spirit Remaining: " + Spirit.CurrMana);
            }
            else if (Spirit.CurrMana <= 0)
            {
                Spirit.SubHealth(5);
                print("Health Remaining: " + Spirit.currentHealth);
            }
        }
    }
    void BlastDrain()
    {
        if(blastTest.drainSpirit == true)
        {
            if (Spirit.CurrMana > 0)
            {
                Spirit.SubManaTime(5);
                print("Spirit Remaining: " + Spirit.CurrMana);
            }
            else if (Spirit.CurrMana <= 0)
            {
                Spirit.SubHealth(5);
                print("Health Remaining: " + Spirit.currentHealth);
            }
        }

    }
}
