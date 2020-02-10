using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> deadEnemyList = new List<GameObject>();

    
    //public List<listItem> enemies = new List<listItem>();
    //public GameObject startingCheckpoint;
    //public GameObject endCheckpoint;

    public bool endCheckActive = false;
    public bool resetPlayer = false;


    //public struct listItem
    //{
    //    public GameObject enemyObj;
    //    public bool isDead;
    //}

    private void Start()
    {
        //addToEnemiesList(enemyList);
    }


    void Update()
    {
        CheckActivationState();

        if (resetPlayer)
        {
            ResetPlayer();
        }
    }


    //void addToEnemiesList(List<GameObject> list) 
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        listItem temp = new listItem();

    //        temp.enemyObj = list[i];
    //        temp.isDead = false;

    //        Debug.Log(temp);

    //        enemies.Add(temp);
    //    }
    //}

    void CheckActivationState()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].activeSelf)
            {
                continue;
            }
            else
            {
                if (!deadEnemyList.Contains(enemyList[i]))
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
        if (!endCheckActive)
        {
            deadEnemyList.Clear();

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].activeSelf)
                {
                    enemyList[i].SetActive(true);
                }
                else
                {
                    continue;
                }
            }
        }
        else if (endCheckActive)
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
