using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_PowerController : MonoBehaviour
{
    public PLY_2ndOrbAttack orbTest;
    public PLY_BeamTest beamTest;
    public PLY_ImanBlastTest blastTest;
    public GameObject firePoint;

    public PowerWheelScroll pwrscrl;

    public PLY_HealthComponent Spirit; //Place holder for spiritBar
    // Start is called before the first frame update
    void Start()
    {
        orbTest = gameObject.GetComponent<PLY_2ndOrbAttack>();
        beamTest = gameObject.GetComponent<PLY_BeamTest>();
        blastTest = gameObject.GetComponent<PLY_ImanBlastTest>();
        orbTest.enabled = false;
        beamTest.enabled = false;
        blastTest.enabled = false;
        firePoint = GameObject.FindGameObjectWithTag("Muzzle");

    }

    // Update is called once per frame
    void Update()
    {

        if (pwrscrl.activeAbility == 0)
        {
            print("Orb On!");
            orbTest.enabled = true;
            beamTest.enabled = false;
            blastTest.enabled = false;
        }
        else if (pwrscrl.activeAbility == 1)
        {
            print("Beam On!");
            orbTest.enabled = false;
            beamTest.enabled = true;
            blastTest.enabled = false;
        }
        else if (pwrscrl.activeAbility == 2)
        {
            print("Blast On!");
            orbTest.enabled = false;
            beamTest.enabled = false;
            blastTest.enabled = true;
        }


        OrbDrain();
        BeamDrain();
        BlastDrain();

    }
    void OrbDrain()
    {
        if (orbTest.Orbshoot == true)
        {
            if (Spirit.CurrSpirit> 0)
            {
                Spirit.SubSpiritOrb(5);
                print("Spirit Remaining: " + Spirit.CurrSpirit);
            }
            else if (Spirit.CurrSpirit <= 0)
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
            if (Spirit.CurrSpirit > 0)
            {
                Spirit.SubSpiritTime(2);
                print("Spirit Remaining: " + Spirit.CurrSpirit);
            }
            else if (Spirit.CurrSpirit <= 0)
            {
                Spirit.SubHealthTime(2);
                print("Health Remaining: " + Spirit.currentHealth);
            }
        }
    }
    void BlastDrain()
    {
		if (blastTest.inputReceived == true)
		{
			if (Spirit.CurrSpirit > 0)
			{
				Spirit.SubSpiritTime(5);
				print("Spirit Remaining: " + Spirit.CurrSpirit);
			}
			else if (Spirit.CurrSpirit <= 0)
			{
				Spirit.SubHealthTime(5);
				print("Health Remaining: " + Spirit.currentHealth);
			}
		}

	}
}
