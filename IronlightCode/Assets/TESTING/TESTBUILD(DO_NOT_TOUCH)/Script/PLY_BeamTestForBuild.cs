using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_BeamTestForBuild : MonoBehaviour
{
	public gameLogic gameLogic;

    public bool inputReceived = false;

    public LineRenderer _line;

	//CameraObject
	Vector3 CameraFace;
	public GameObject Camera;

	//PlayerObject
	Vector3 PlayerPos;
	public GameObject PlayerTiny;

	//aim
	Vector3 AimBeam;

    //beam
    public GameObject muzzle;
	Vector3 beamDir;
	Ray beamRay;
	private float Distance;
	Vector3 lineStartPosition;
	Vector3 lineEndPosition;
	Vector3 lineLength;
	public bool endAttack;
    public bool StartAttack;
    [SerializeField] private float _fBeamSpeedGoing;
    [SerializeField] private float _fBeamSpeedClosing;
    [SerializeField] private int _iBeamRange;

    float blah = 0;


    private void Start()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);

	}

    private void Update()
    {
        GetInput();

		lineLength = lineStartPosition - lineEndPosition;

		CameraFace = Camera.transform.position;

		AimBeam = new Vector3(CameraFace.x, CameraFace.y, CameraFace.z);

		PlayerPos = new Vector3(PlayerTiny.transform.position.x, PlayerTiny.transform.position.y, PlayerTiny.transform.position.z);

		beamDir = ((lineEndPosition - transform.position) / (lineEndPosition - transform.position).magnitude);

		PlayerTiny.transform.LookAt(PlayerPos -(AimBeam - PlayerPos));



		if (StartAttack)
        {

            if (_line.GetPosition(1).z <= _iBeamRange)
            {
				lineStartPosition = new Vector3(_line.GetPosition(1).x, _line.GetPosition(1).y, _line.GetPosition(1).z + _fBeamSpeedGoing * Time.deltaTime);

				_line.SetPosition(1, lineStartPosition);
            }

			//print("START" + lineStartPosition);
			//print("START" + lineEndPosition);

			beamgoing();

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
			//print("End" + lineStartPosition);
			//print("End" + lineEndPosition);
        }

		RaycastHit hit;

		if (Physics.Raycast(beamRay, out hit))
		{

			print("Ray " + hit.transform.gameObject.name);

			if (hit.transform.gameObject.name.Contains("Crystal 1"))
			{
				//Output message
				print("Crystal 1 detected");
				gameLogic.Crystal1 = true;
			}

			if (hit.transform.gameObject.name.Contains("Crystal 2"))
			{
				//Output message
				print("Crystal 2 detected");
				gameLogic.Crystal2 = true;
			}

			if (hit.transform.gameObject.name.Contains("Crystal 3"))
			{
				//Output message
				print("Crystal 3 detected");
				gameLogic.Crystal3 = true;
			}

		}

		//if(Physics.Linecast(lineStartPosition, lineEndPosition, out hit))
		//{
		//	if (hit.collider)
		//	{
		//		print("Line " + hit.collider.gameObject.name);
		//		//print(hit.collider.ToString());
		//	}

		//}
	}

    void GetInput()
    {
        // Change this depending on how you want the attack to work
        if (!endAttack)
        {
			if (Input.GetMouseButtonDown(0))
			{
				inputReceived = true;
				StartAttack = true;
			}

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    inputReceived = true;
            //    StartAttack = true;
            //}
        }
		if (Input.GetMouseButtonUp(0))
		{
			inputReceived = false;
			StartAttack = false;
			endAttack = true;
		}
		//if (Input.GetKeyUp(KeyCode.Space))
  //      {
  //          inputReceived = false;
  //          StartAttack = false;
  //          endAttack = true;
  //      }
    }

    private void beamgoing()
    {
		beamRay = new Ray(transform.position, beamDir);
	}

    private void beamEnding()
    {
		lineEndPosition = new Vector3(_line.GetPosition(0).x, _line.GetPosition(0).y, _line.GetPosition(0).z + _fBeamSpeedClosing * Time.deltaTime);

		_line.SetPosition(0, lineEndPosition);
    }

}
