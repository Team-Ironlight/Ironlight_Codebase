using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public bool PlayerInZone = false;
	public bool enemiesSpawned = false;

	public List<EnemyInitializer> enemySpawns = new List<EnemyInitializer>();



    // Start is called before the first frame update
    void Start()
    {
		PopulateEnemySpawnList();
    }

	void PopulateEnemySpawnList()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("SpawnPoint");

		for (int i = 0; i < enemies.Length; i++)
		{
			enemySpawns.Add(enemies[i].GetComponent<EnemyInitializer>());
		}
	}

	void StartInitializingEnemies()
	{
		for (int i = 0; i < enemySpawns.Count; i++)
		{
			//enemySpawns[i].Init();
		}

		enemiesSpawned = true;
	}

	// Update is called once per frame
	void Update()
    {
		if (PlayerInZone && !enemiesSpawned)
		{
			StartInitializingEnemies();
		}
    }
}
