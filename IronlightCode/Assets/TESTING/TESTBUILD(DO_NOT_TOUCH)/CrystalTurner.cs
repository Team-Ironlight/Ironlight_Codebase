using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTurner : MonoBehaviour
{
	public PLY_BeamTestForBuild BeamBool;

	//public Transform target;

	//public int Num;

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
		if (BeamBool.Crystal1)
		{
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed);

			laser1.SetActive(true);

			PlatformRaised1.transform.position = Vector3.Lerp(PlatformRaised1.transform.position, new Vector3(1.5f, 0.5f, 10.24f), Time.deltaTime);

		}

		if (BeamBool.Crystal2)
		{
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed);

			laser2.SetActive(true);

			PlatformRaised2.transform.position = Vector3.Lerp(PlatformRaised2.transform.position, new Vector3(1.5f, 0.5f, 12.24f), Time.deltaTime);
		}

		if (BeamBool.Crystal3)
		{
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed);

			laser3.SetActive(true);

			PlatformRaised3.transform.position = Vector3.Lerp(PlatformRaised3.transform.position, new Vector3(1.5f, 0.5f, 14.24f), Time.deltaTime); 
		}


    }

	
}
