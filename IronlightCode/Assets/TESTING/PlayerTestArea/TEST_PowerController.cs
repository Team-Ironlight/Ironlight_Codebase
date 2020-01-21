using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_PowerController : MonoBehaviour
{
    public MOD_orb orbTest;
    public MOD_beam beamTest;
    public PLY_ImanBlastTest blastTest;
    public GameObject firePoint;

    float Spirit; //Place holder for spiritBar
    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("Muzzle");
        Spirit = 100;
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
            Spirit -= 5;
            print("Spirit Remaining: " + Spirit);
        }
    }
    void BeamDrain()
    {
        if (beamTest.StartAttack == true)
        {
            Spirit -= 2 * Time.deltaTime;
            print("Spirit Remaining: " + Spirit);
        }
    }
    void BlastDrain()
    {
        if(blastTest.drainSpirit == true)
        {
            Spirit -= 5 * Time.deltaTime;
            print("Spirit Remaining: " + Spirit);
        }

    }
}
