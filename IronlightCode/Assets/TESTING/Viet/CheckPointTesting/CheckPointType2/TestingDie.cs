using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDie : MonoBehaviour
{

    public CheckPoint2 Dead;
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
            transform.position = CheckPoint2.GetActiveCheckPointPosition();
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
