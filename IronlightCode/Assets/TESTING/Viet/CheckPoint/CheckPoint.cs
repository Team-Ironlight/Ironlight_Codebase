using UnityEngine;

public class CheckPoint : MonoBehaviour
{
 
    public bool Activated = false;

    public static GameObject[] CheckPointsList;

    public static Vector3 currentCheck;

    void Start()
    {
        // We search all the checkpoints in the current scene
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
            }
        }
    }

    private void ActivateCheckPoint()
    {
        // We deactive all checkpoints in the scene
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
