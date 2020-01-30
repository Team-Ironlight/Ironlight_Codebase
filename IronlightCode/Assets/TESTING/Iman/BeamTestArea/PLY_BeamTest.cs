using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_BeamTest : MonoBehaviour
{
    public bool inputReceived = false;

    //linerenderer
    [SerializeField] private LineRenderer LineRenderer;

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
    private Vector3 PosBeforeRelease;
    private Vector3 DirBeforeRelease;

    //linecast
    private RaycastHit LineCastHit;
    private bool HittingObject;


    private void Start()
    {
        BeamReset();

        HittingObject = false;

        LineRenderer.gameObject.transform.position = Vector3.zero;
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

        //while key is pressed
        if (StartAttack)
        {
            //update the end position of the beam
            LineEnd = muzzle.transform.position + (muzzle.transform.forward * BeamLengthGoing);
            LineRenderer.SetPosition(1, LineEnd);

            //set start position to muzzle
            LineStart = muzzle.transform.position ;
            LineRenderer.SetPosition(0, LineStart);
            //save the last position and direction before reelease
            PosBeforeRelease = muzzle.transform.position;
            DirBeforeRelease = muzzle.transform.forward;
        }
        //when key is released
        if (endAttack)
        {
            //update start position after release
            LineStart = PosBeforeRelease + (DirBeforeRelease * BeamLengthClosing);
            LineRenderer.SetPosition(0, LineStart);
        }
        //set the position of the game object always to center so it doesnt create offset problems
        LineRenderer.gameObject.transform.position = Vector3.zero;
    }
    //add to end point distance
    private void beamgoing()
    {
        if (BeamLengthGoing <= _iBeamRange && !HittingObject)
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
            LineRenderer.SetPosition(1, LineCastHit.point);
            LineEnd = LineCastHit.point;
        }
    }
    //function to reset the beam
    private void BeamReset()
    {
        //linecast reset
        LineStart = muzzle.transform.position;
        LineEnd = muzzle.transform.position;
        //linerenderer reset
        LineRenderer.SetPosition(0, LineStart);
        LineRenderer.SetPosition(1, LineEnd);
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
