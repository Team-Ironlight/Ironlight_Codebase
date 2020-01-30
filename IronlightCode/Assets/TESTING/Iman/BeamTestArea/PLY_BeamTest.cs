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
    [HideInInspector] public bool endAttack;
    [HideInInspector] public bool StartAttack;
    [SerializeField] private float _fBeamSpeedGoing;
    [SerializeField] private float _fBeamSpeedClosing;
    [SerializeField] private int _iBeamRange;
    private float BeamLengthGoing;
    private float BeamLengthClosing;

    //linecast
    private RaycastHit LineCastHit;
    private bool HittingObject;

    private void Start()
    {
        BeamReset();

        HittingObject = false;

        _line.transform.position = muzzle.transform.position;
    }

    private void Update()
    {
        GetInput();
        BeamLineCast();
        //function of the attack
        if(StartAttack)
        {
            beamgoing();
            BeamPosUpdate();
        }
        if(endAttack)
        {
            beamEnding();
            BeamPosUpdate();
            if (Vector3.Distance(LineEnd, muzzle.transform.position) < Vector3.Distance(LineStart, muzzle.transform.position))
            {
                endAttack = false;
                BeamReset();
            }
        }
        
    }

    void GetInput()
    {
        // Change this depending on how you want the attack to work
        if (!endAttack)
        {
            if (Input.GetKeyDown(KeyCode.T) || Input.GetMouseButtonDown(0))
            {
                inputReceived = true;
                StartAttack = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.T) || Input.GetMouseButtonUp(0))
        {
            inputReceived = false;
            StartAttack = false;
            endAttack = true;
        }
    }
    //position the points in the world every frame
    private void BeamPosUpdate()
    {
        //endpoint
        _line.SetPosition(1,muzzle.transform.forward * BeamLengthGoing);
        LineEnd = muzzle.transform.position + (muzzle.transform.forward * BeamLengthGoing);

        //startpoint
        if (BeamLengthClosing == 0)
        {
            _line.SetPosition(0, muzzle.transform.position);
            LineStart = muzzle.transform.position + (muzzle.transform.forward * BeamLengthClosing);
        }
        else
        {
            _line.SetPosition(0, muzzle.transform.forward * BeamLengthClosing);
            LineStart = muzzle.transform.position + (muzzle.transform.forward * BeamLengthClosing);
        }
    }
    //add to end point distance
    private void beamgoing()
    {
        if (_line.GetPosition(1).z <= _iBeamRange && !HittingObject)
        {
            BeamLengthGoing += _fBeamSpeedGoing * Time.deltaTime;
        }
    }
    //add to start point distance
    private void beamEnding()
    {
        BeamLengthClosing += _fBeamSpeedClosing * Time.deltaTime;
    }
    //Function for linecast
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
    //function to reset the beam
    private void BeamReset()
    {
        //linerenderer reset
        _line.SetPosition(0, muzzle.transform.position);
        _line.SetPosition(1, muzzle.transform.position);
        //linecast reset

        LineStart = muzzle.transform.position;
        LineEnd = muzzle.transform.position;
        //length reset
        BeamLengthGoing = 0;
        BeamLengthClosing = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(LineStart, LineEnd);
    }
}
