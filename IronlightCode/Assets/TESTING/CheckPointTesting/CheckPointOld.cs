using UnityEngine;

public class CheckPointOld : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// Indicate if the checkpoint is activated
    /// </summary>
    public bool Activated = false;

    #endregion

    #region Private Variables


    #endregion

    #region Static Variables

    /// <summary>
    /// List with all checkpoints objects in the scene
    /// </summary>
    public static GameObject[] CheckPointsList;

    #endregion

    #region Static Functions

    /// <summary>
    /// Get position of the last activated checkpoint
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetActiveCheckPointPosition()  //Last Position
    {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<CheckPointOld>().Activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }

        return result;
    }

    #endregion

    #region Private Functions

    /// <summary>
    /// Activate the checkpoint
    /// </summary>
    private void ActivateCheckPoint()
    {
        // We deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<CheckPointOld>().Activated = false;
        }

        // We activated the current checkpoint
        Activated = true;
    }

    #endregion

    void Start()
    {
        // We search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
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
