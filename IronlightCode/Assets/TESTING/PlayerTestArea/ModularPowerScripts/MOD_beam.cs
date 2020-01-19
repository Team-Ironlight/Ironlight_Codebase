using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOD_beam : MonoBehaviour
{
    public bool inputReceived = false;

    public LineRenderer _line;

    //beam
    public GameObject muzzle;
    private float Distance;
    public bool endAttack;
    public bool StartAttack;
    [SerializeField] private float _fBeamSpeedGoing;
    [SerializeField] private float _fBeamSpeedClosing;
    [SerializeField] private int _iBeamRange;
    bool hitLeaf;
    public LayerMask layerMask;
    float blah = 0;


    private void Start()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
    }

    private void Update()
    {
        GetInput();

        if (StartAttack)
        {
            beamgoing();

            if (_line.GetPosition(1).z <= _iBeamRange)
            {
                _line.SetPosition(1, new Vector3(_line.GetPosition(1).x, _line.GetPosition(1).y, _line.GetPosition(1).z + _fBeamSpeedGoing * Time.deltaTime));
            }
        }
        if (endAttack)
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                inputReceived = true;
                StartAttack = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            inputReceived = false;
            StartAttack = false;
            endAttack = true;
        }
    }

    private void beamgoing()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hit, 40f, layerMask))
        {
            hit.transform.SendMessage("RayHit");
            hitLeaf = true;
            print("Hit");
        }
        else
        {

            hitLeaf = false;
        }
    }

    private void beamEnding()
    {
        _line.SetPosition(0, new Vector3(_line.GetPosition(0).x, _line.GetPosition(0).y, _line.GetPosition(0).z + _fBeamSpeedClosing * Time.deltaTime));
    }
}
