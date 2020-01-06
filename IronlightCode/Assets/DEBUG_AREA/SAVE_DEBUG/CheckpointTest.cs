using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTest : MonoBehaviour
{
    float ver;
    float hor;
    bool inputRecieved = false;

    public GameObject currentCheck;

    public Vector3 moveDir;

    public static GameObject[] CheckPointsList;




    // Start is called before the first frame update
    void Start()
    {
        // We search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();

        if (inputRecieved)
        {
            ResetToCheckpoint();
        }
    }

    void GetInput()
    {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        moveDir = new Vector3(hor, 0, ver);

        inputRecieved = Input.GetKeyDown(KeyCode.Space);
    }

    void ResetToCheckpoint()
    {
        // move object to las activated checkpoint
        gameObject.transform.position = currentCheck.transform.position;
    }

    void Move()
    {
        transform.position = new Vector3(transform.position.x + moveDir.x, 0, 0);
    }

    // when checkpoint trigger entered
        // assign checkpoint gameobject to currentCheck
        // deactivate checkpoint gameobject's trigger


    // when player dies OR when input received
        // set player position to currentCheck position




    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.tag == "CheckPoint")
        {
            currentCheck = other.gameObject;
        }
    }

   
    
}
