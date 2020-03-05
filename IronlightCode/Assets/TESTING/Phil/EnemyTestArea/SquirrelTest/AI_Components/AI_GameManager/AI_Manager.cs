// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{

    public static AI_Manager instance;

    [SerializeField]
    private GameObject SQwander_Prefab, SQchase_Prefab;

    public Transform[] wander_SpawnPoints, chase_SpawnPoints;

    [SerializeField]
    private int wander_Enemy_Count, chase_Enemy_Count;

    private int initial_SQwander_Count, initial_SQchase_Count;

    public float wait_Before_Spawn_Enemies_Time = 10f;

    // Use this for initialization
    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        initial_SQwander_Count = wander_Enemy_Count;
        initial_SQchase_Count = chase_Enemy_Count;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {
        SpawnSQwander();
        SpawnSQchase();
    }

    void SpawnSQwander()
    {

        int index = 0;

        for (int i = 0; i < wander_Enemy_Count; i++)
        {

            if (index >= wander_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(SQwander_Prefab, wander_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        wander_Enemy_Count = 0;

    }

    void SpawnSQchase()
    {

        int index = 0;

        for (int i = 0; i < chase_Enemy_Count; i++)
        {

            if (index >= chase_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(SQchase_Prefab, chase_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        chase_Enemy_Count = 0;

    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);

        SpawnSQwander();

        SpawnSQchase();

        StartCoroutine("CheckToSpawnEnemies");

    }

    public void EnemyDied(bool wander)
    {

        if (wander)
        {

            wander_Enemy_Count++;

            if (wander_Enemy_Count > initial_SQwander_Count)
            {
                wander_Enemy_Count = initial_SQwander_Count;
            }

        }
        else
        {

            chase_Enemy_Count++;

            if (chase_Enemy_Count > initial_SQchase_Count)
            {
                chase_Enemy_Count = initial_SQchase_Count;
            }

        }

    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

} // class


































