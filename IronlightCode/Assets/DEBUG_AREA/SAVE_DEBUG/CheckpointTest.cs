using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTest : MonoBehaviour
{
    float ver;
    float hor;
    bool inputeRecieved = false;

    public Vector3 moveDir;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();

        if (inputeRecieved)
        {
            ResetToCheckpoint();
        }
    }

    void GetInput()
    {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        moveDir = new Vector3(hor, 0, ver);

        inputeRecieved = Input.GetKeyDown(KeyCode.Space);
    }

    void ResetToCheckpoint()
    {
        // move object to las activated checkpoint
    }

    void Move()
    {
        transform.position = new Vector3(transform.position.x + moveDir.x, 0, 0);
    }
}
