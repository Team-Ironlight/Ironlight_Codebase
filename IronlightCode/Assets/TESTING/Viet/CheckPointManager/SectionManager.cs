using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>(); //List of enemy alive
    public List<GameObject> deadEnemyList = new List<GameObject>(); //List of dead enemy

    public bool endCheckActive = false;
    public bool resetPlayer = false;

	void Start()
	{
		GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < Enemies.Length; i++)
		{
			if (Enemies[i].GetComponent<BeamPuzzleShrine>() != null)
			{
				enemyList.Add(Enemies[i]);
			}
		}
	}


	void Update()
    {
        CheckActivationState(); //check enemy alive or dead

        if (resetPlayer == true)
        {
            
            ResetPlayer();
        }
    }


    void CheckActivationState() //Check enemy is dead or alive
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].activeSelf) //Alive, continue
            {
                continue;
            }
            else
            {
                if (!deadEnemyList.Contains(enemyList[i])) //Is dead, add them to the dead list
                {
                    deadEnemyList.Add(enemyList[i]);
                }
                else
                {
                    continue;
                }
            }
        }
    }

    void ResetPlayer()
    {
        if (!endCheckActive) //Player didn't hit new checkpoint, respawn player and remove all enemy in the dead list and active them.
        {
            deadEnemyList.Clear();

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].activeSelf)
                {
                    enemyList[i].SetActive(true);
					enemyList[i].GetComponent<UnitHealth>().ReviveEnemy();
                }
                else
                {
                    continue;
                }
            }
        }
        else if (endCheckActive) //Player hit new checkpoint, respawn player at new checkpoint and keep dead enemy disable.
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                for (int k = 0; k < deadEnemyList.Count; k++)
                {
                    if(enemyList[i] == deadEnemyList[k])
                    {
                        enemyList[i].SetActive(false);
                    }
                }
            }
        }

        resetPlayer = false;
    }
}
