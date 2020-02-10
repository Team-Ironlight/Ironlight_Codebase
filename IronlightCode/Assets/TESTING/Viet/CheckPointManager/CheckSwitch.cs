using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecKSwitch : MonoBehaviour
{
    private bool newCheckPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPointSwitch();
    }

    public void checkPointSwitch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (newCheckPoint == false)
            {
                newCheckPoint = true;
                Debug.Log("To CheckPoint2");
                return;
            }
            if (newCheckPoint == true)
            {
                newCheckPoint = false;
                Debug.Log("To CheckPoint1");
                return;
            }
        }
    }
}
