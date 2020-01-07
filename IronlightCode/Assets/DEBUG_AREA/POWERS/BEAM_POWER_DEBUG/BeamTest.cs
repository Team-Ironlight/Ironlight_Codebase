using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTest : MonoBehaviour
{
    public bool inputReceived = false;

    public LineRenderer _line;

    //beam
    public GameObject muzzle;
    private float Distance;
    private bool endAttack;
    private bool StartAttack;


    private void Start()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
    }

    private void Update()
    {
        GetInput();

        if(StartAttack)
        {
            beamgoing();
        }
        if(endAttack)
        {
            beamEnding();
            if (_line.GetPosition(0).z >= _line.GetPosition(1).z)
            {
                endAttack = false;
                _line.SetPosition(0, Vector3.zero);
                _line.SetPosition(1, Vector3.zero);
            }
        }

    }

    void GetInput()
    {
        // Change this depending on how you want the attack to work
        if (!endAttack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputReceived = true;
                StartAttack = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            inputReceived = false;
            StartAttack = false;
            endAttack = true;
        }


    }

    private void beamgoing()
    {
        _line.SetPosition(1, new Vector3(_line.GetPosition(1).x, _line.GetPosition(1).y, _line.GetPosition(1).z + 5 * Time.deltaTime));
    }

    private void beamEnding()
    {
        _line.SetPosition(0, new Vector3(_line.GetPosition(0).x, _line.GetPosition(0).y, _line.GetPosition(0).z + 5 * Time.deltaTime));
    }

}
