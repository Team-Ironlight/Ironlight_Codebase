using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private PlayerCamera camera;

    [Space]
    [SerializeField] private float deathLength = 3f;
    [SerializeField] [Range(0.001f,1)] private float deathSlowAmount = 0.2f;
    private static Vector3 currentRespawnPoint;
    private static float currentRespawnRotationY;

    private void Start()
    {
        //Set Player Spawn
        if (currentRespawnPoint == Vector3.zero)
        {
            currentRespawnPoint = transform.position;
            currentRespawnRotationY = transform.GetChild(0).eulerAngles.y;
        }
        else
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = currentRespawnPoint;
        transform.GetChild(0).rotation = Quaternion.Euler(0, currentRespawnRotationY, 0);
        //camera.Respawn(currentRespawnRotationY);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //GetComponent<PlayerStateController>().movementDir = transform.GetChild(0).forward;
        //GetComponent<PlayerStateController>().Respawn = true;
    }

    public void Dead()
    {
        StartCoroutine("DeadRoutine");
    }

    private IEnumerator DeadRoutine()
    {
        //Slow while dieing time
        Time.timeScale = deathSlowAmount;

        yield return new WaitForSeconds(deathLength * deathSlowAmount);

        Time.timeScale = 1.0f;
        //Temp Restart Scene (replace this with the proper scene manager and with a HUD element)
        SaveAndLoad._Instance.QuickLoadSave();
    }

    public void setRespawnTransform(Vector3 pPoint, float pRotationY)
    {
        //Change Where the player spawns after dieing
        currentRespawnPoint = pPoint;
        currentRespawnRotationY = pRotationY;
    }

    public Vector3 getRespawnPoint()
    {
        return currentRespawnPoint;
    }

    public float getRespawnRotation()
    {
        return currentRespawnRotationY;
    }
}
