using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDie : MonoBehaviour
{

    public CheckPoint Dead; //Where are we even using this?
    public bool die;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (die == true)
        {
            transform.position = CheckPoint.currentCheck;// if CheckPoint is being called like this then what did you call at the top?
            die = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillPlane")
        {
            die = true;
        }
    }
}
