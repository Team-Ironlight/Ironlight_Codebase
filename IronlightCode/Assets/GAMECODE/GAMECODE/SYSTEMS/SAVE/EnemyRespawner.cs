using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Oliver

    Script gets attached to enemy and adds itself to listen for enemyRespawn function
    This is what respawns enemies
*/

public class EnemyRespawner : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    [SerializeField] private bool _destroyOnLoad;

    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        //Add my function to event
        SaveAndLoad.RespawnEnemies += RespawnEnemy;
    }

    private void OnDestroy()
    {
        //Remove my function from event
        SaveAndLoad.RespawnEnemies -= RespawnEnemy;
    }

    void RespawnEnemy(SaveAndLoad pSaveAndLoad)
    {
        // Replaced this line with the use of an object pool
        if (_destroyOnLoad) Destroy(this.gameObject);

        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        GetComponent<IAttributes>().Respawn();
        GetComponent<Decision>().Respawn();
        gameObject.SetActive(true);
    }
}
