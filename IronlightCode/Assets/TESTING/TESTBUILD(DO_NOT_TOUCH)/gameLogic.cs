using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLogic : MonoBehaviour
{
	//beam script
	public PLY_BeamTestForBuild BeamBool;

	//Crystal bool
	public bool Crystal1 = false;
	public bool Crystal2 = false;
	public bool Crystal3 = false;

	//public float speed = 15f;

	public GameObject PlatformRaised1;
	public GameObject PlatformRaised2;
	public GameObject PlatformRaised3;

	public GameObject laser1;
	public GameObject laser2;
	public GameObject laser3;

    // Start is called before the first frame update
    void Start()
    {
		//speed *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
		if (Crystal1)
		{
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed);

			laser1.SetActive(true);

			PlatformRaised1.transform.position = Vector3.Lerp(PlatformRaised1.transform.position, new Vector3(1.5f, 0.5f, 10.24f), Time.deltaTime);

		}

		if (Crystal2)
		{
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed);

			laser2.SetActive(true);

			PlatformRaised2.transform.position = Vector3.Lerp(PlatformRaised2.transform.position, new Vector3(1.5f, 0.5f, 12.24f), Time.deltaTime);
		}

		if (Crystal3)
		{
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed);

			laser3.SetActive(true);

			PlatformRaised3.transform.position = Vector3.Lerp(PlatformRaised3.transform.position, new Vector3(1.5f, 0.5f, 14.24f), Time.deltaTime); 
		}


    }

	
}
