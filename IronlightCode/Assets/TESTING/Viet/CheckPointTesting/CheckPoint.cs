using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static GameObject[] CheckPointsList;

    public Vector3 currentCheck;

    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
        Debug.Log(CheckPointsList.Length);
    }

    // Update is called once per frame
    void Update()
    {
        teleportToCheckPoint();
    }

    void teleportToCheckPoint()
    {
        if (dead == true)
        {
            if (currentCheck != null)
            {
                transform.position = currentCheck;
            }
            else
            {
                transform.position = new Vector3(0, 0, 0);
            }
            dead = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            currentCheck = other.gameObject.transform.position;
        }
        if (other.tag == "KillPlane")
        {
            dead = true;
        }
    }

}
