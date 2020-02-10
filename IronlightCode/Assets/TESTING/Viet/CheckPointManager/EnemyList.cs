using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckActivationState();
    }

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
                Debug.Log(enemyList[i].name + " inactive");
            }
        }
    }
}
