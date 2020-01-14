using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_PowerController : MonoBehaviour
{
    public PLY_OrbTest orbTest;
    public PLY_BeamTest beamTest;
    public PLY_ImanBlastTest blastTest;
    public GameObject firePoint;


    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("Muzzle");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
