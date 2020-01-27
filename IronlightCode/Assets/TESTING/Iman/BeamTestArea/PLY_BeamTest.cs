using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_BeamTest : MonoBehaviour
{
    public bool inputReceived = false;

    //linerenderer
    public LineRenderer _line;

    //LineCast
    private Vector3 LineStart;
    private Vector3 LineEnd;

    //beam
    public GameObject muzzle;
    private float Distance;
    public bool endAttack;
    public bool StartAttack;
    [SerializeField] private float _fBeamSpeedGoing;
    [SerializeField] private float _fBeamSpeedClosing;
    [SerializeField] private int _iBeamRange;

    //linecast
    private RaycastHit LineCastHit;
    private bool HittingObject;

    private void Start()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);

        LineStart = Vector3.zero;
        LineEnd = Vector3.zero;

        HittingObject = false;
    }

    private void Update()
    {
        GetInput();
        BeamLineCast();
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

                LineStart = Vector3.zero;
                LineEnd = Vector3.zero;
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
        if (_line.GetPosition(1).z <= _iBeamRange && !HittingObject)
        {
            _line.SetPosition(1, new Vector3(_line.GetPosition(1).x, _line.GetPosition(1).y, _line.GetPosition(1).z + _fBeamSpeedGoing * Time.deltaTime));
            LineEnd = new Vector3(LineEnd.x, LineEnd.y, LineEnd.z + _fBeamSpeedGoing * Time.deltaTime);
        }
    }

    private void beamEnding()
    {
        _line.SetPosition(0, new Vector3(_line.GetPosition(0).x, _line.GetPosition(0).y, _line.GetPosition(0).z + _fBeamSpeedClosing * Time.deltaTime));
        LineStart = new Vector3(LineStart.x, LineStart.y, LineStart.z + _fBeamSpeedClosing * Time.deltaTime);
    }

    private void BeamLineCast()
    {
        if(Physics.Linecast(LineStart, LineEnd,out LineCastHit))
        {
            //objects detects here add code

            HittingObject = true;
        }
        else
        {
            HittingObject = false;
        }

        if(HittingObject)
        {
            _line.SetPosition(1, LineCastHit.point);
            LineEnd = LineCastHit.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(LineStart, LineEnd);
    }
}
