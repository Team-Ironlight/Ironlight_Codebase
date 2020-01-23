using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_BeamTestForBuild : MonoBehaviour
{
    public bool inputReceived = false;

    public LineRenderer _line;

	//Crystal bool
	public bool Crystal1 = false;
	public bool Crystal2 = false;
	public bool Crystal3 = false;

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
	//public CapsuleCollider capsule;
	//float LineWidth = 0.1f; // use the same as you set in the line renderer.
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

	RaycastHit hit;

    private void Start()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);

		//capsule = gameObject.AddComponent();
		//capsule.radius = LineWidth / 2;
		//capsule.center = Vector3.zero;
		//capsule.direction = 2; // Z-axis for easier "LookAt" orientation
	}

    private void Update()
    {
        GetInput();

		lineLength = lineStartPosition - lineEndPosition;

		CameraFace = Camera.transform.position;

		AimBeam = new Vector3(CameraFace.x, CameraFace.y, CameraFace.z);

		PlayerPos = new Vector3(PlayerTiny.transform.position.x, PlayerTiny.transform.position.y, PlayerTiny.transform.position.z);


		PlayerTiny.transform.LookAt(PlayerPos -(AimBeam - PlayerPos));

        if(StartAttack)
        {
            beamgoing();

            if (_line.GetPosition(1).z <= _iBeamRange)
            {
				lineStartPosition = new Vector3(_line.GetPosition(1).x, _line.GetPosition(1).y, _line.GetPosition(1).z + _fBeamSpeedGoing * Time.deltaTime);

				_line.SetPosition(1, lineStartPosition);
            }
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

		if (Physics.Raycast(transform.position, lineLength, out hit))
		{
			if (hit.collider)
			{
				print(hit.collider.gameObject.name);
				//print(hit.collider.ToString());
			}

			if (hit.collider.gameObject.name.Contains("Crystal 1"))
			{
				//Output message
				print("Crystal 1 detected");
				Crystal1 = true;
			}

			if (hit.collider.gameObject.name.Contains("Crystal 2"))
			{
				//Output message
				print("Crystal 2 detected");
				Crystal2 = true;
			}

			if (hit.collider.gameObject.name.Contains("Crystal 3"))
			{
				//Output message
				print("Crystal 3 detected");
				Crystal3 = true;
			}

			//if(hit.transform.gameObject.name.Contains("Crystal1Core"))
			//{
			//	//Output message
			//	print("Crystal 1 detected");
			//	Crystal1 = true;
			//}
			//if (hit.transform.gameObject.name.Contains("Crystal2Core"))
			//{
			//	//Output message
			//	print("Crystal 2 detected");
			//	Crystal2 = true;
			//}
			//if (hit.transform.gameObject.name.Contains("Crystal3Core"))
			//{
			//	//Output message
			//	print("Crystal 3 detected");
			//	Crystal3 = true;
			//}
		}

		//capsule.transform.position = lineStartPosition + (lineEndPosition - lineStartPosition) / 2;
		//capsule.transform.LookAt(lineStartPosition);
		//capsule.height = (lineEndPosition - lineStartPosition).magnitude;

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
        
    }

    private void beamEnding()
    {
		lineEndPosition = new Vector3(_line.GetPosition(0).x, _line.GetPosition(0).y, _line.GetPosition(0).z + _fBeamSpeedClosing * Time.deltaTime);

		_line.SetPosition(0, lineEndPosition);
    }

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.gameObject.name.Contains("Crystal1Core"))
	//	{
	//		//Output message
	//		print("Crystal 1 detected");
	//		Crystal1 = true;
	//	}

	//	if (other.gameObject.name.Contains("Crystal2Core"))
	//	{
	//		//Output message
	//		print("Crystal 2 detected");
	//		Crystal2 = true;
	//	}

	//	if (other.gameObject.name.Contains("Crystal3Core"))
	//	{
	//		//Output message
	//		print("Crystal 3 detected");
	//		Crystal3 = true;
	//	}
		
	//}

}
