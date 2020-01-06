using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTest : MonoBehaviour
{
    float ver;
    float hor;
    bool dodgeInput = false;


    public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        moveDir = new Vector3(hor, 0, ver);

        if (dodgeInput)
        {
            Dodge();
        }
    }

    void Dodge()
    {
        Debug.Log("I'm Jumping bitches");
    }

    void GetInput()
    {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dodgeInput = true;
        }
        else
        {
            dodgeInput = false;
        }
    }
}
