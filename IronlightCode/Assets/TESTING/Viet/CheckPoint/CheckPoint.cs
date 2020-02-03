using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	//Please check this code for unneeded complication. Please refrain from using Lists when dealing with the Vector3 transform position information of one gameObject (CheckPoint) at a time. Please try to use the simplest version of code to prioritize performance and save memory. Thanks...


    public bool Activated = false;

    public static GameObject[] CheckPointsList;

    public static Vector3 currentCheck;

    void Start()
    {
        // We search all the checkpoints in the current scene... (Wasiq's response: BUY WHY?)
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
    }

    public void Update()
    {
        checkPos();
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(currentCheck.ToString());
        }
    }

    public static void checkPos()
    {
        if (CheckPointsList != null)
        {          
            foreach (GameObject cp in CheckPointsList )
            {
                if (cp.GetComponent<CheckPoint>().Activated)
                {
                    currentCheck = cp.transform.position;
                }
				else
				{
					currentCheck = new Vector3(-7.43f, 1.2f, 5.65f);//Wasiq's response: I assigned this value because it is the starting point of tiny in the test game. Please make it so as to refer an empty gameObject we can move around in game for level design purposes.
				}
            }
        }
    }

    private void ActivateCheckPoint()
    {
		// We deactive all checkpoints in the scene (Wasiq's response: again, why?)
		foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<CheckPoint>().Activated = false;
        }

        // We activated the current checkpoint
        Activated = true;
    }    

    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.tag == "Player")
        {
            ActivateCheckPoint();
        }
    }
}
