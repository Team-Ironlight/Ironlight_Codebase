using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_AttackComponent : MonoBehaviour
{
    PLY_OrbTest orbAttack;
    PLY_BeamTest beamAttack;
    PLY_RadialTest radialAttack;
    GameObject muzzle;

    // Start is called before the first frame update
    void Start()
    {
        muzzle = GameObject.FindGameObjectWithTag("Muzzle");
        orbAttack = muzzle.GetComponent<PLY_OrbTest>();
        beamAttack = muzzle.GetComponent<PLY_BeamTest>();
        radialAttack = muzzle.GetComponent<PLY_RadialTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchOrb()
    {
        //orbAttack.Shoot();
    }

    public void StartBeam()
    {
        beamAttack.StartAttack = true;
    }

    public void EndBeam()
    {
        beamAttack.endAttack = true;
        beamAttack.StartAttack = false;
    }

    public void LaunchRadial()
    {
        radialAttack.TestPulse();
    }
}
