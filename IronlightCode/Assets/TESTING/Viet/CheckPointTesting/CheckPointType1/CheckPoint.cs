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
        //teleportToCheckPoint();
        if (dead == true)
        {
            if (currentCheck != null)
            {
                print("Revive!");
                transform.position = currentCheck;
            }
            else
            {
                print("Reset");
                transform.position = new Vector3(0, 0, 0);
            }
            dead = false;
        }
    }

    //void teleportToCheckPoint()
    //{
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            print("Checkpoint reached: " + currentCheck.ToString());
            currentCheck = other.gameObject.transform.position;
        }
        if (other.tag == "KillPlane")
        {
            print("Death");
            dead = true;
        }
    }

}
