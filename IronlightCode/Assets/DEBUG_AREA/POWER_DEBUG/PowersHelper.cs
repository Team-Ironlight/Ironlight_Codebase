using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersHelper : MonoBehaviour
{

    public bool OrbAttack = false;
    public bool BeamAttack = false;
    public bool RadialAttack = false;


    private void Update()
    {
        GetInput();asdf

    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OrbAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            BeamAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RadialAttack = true;
        }
    }
}
