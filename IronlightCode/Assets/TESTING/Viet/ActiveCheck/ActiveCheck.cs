using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCheck : MonoBehaviour
{

    public List<GameObject> activeObject = new List<GameObject>(); //Create list of Object to check

    private bool allActive = false;

    private float numberOfObject;
    private float currentActiveObject = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Object = GameObject.FindGameObjectsWithTag("Object"); //Add Object into the list
        for (int i = 0; i < Object.Length; i++)
        {
            activeObject.Add(Object[i]);
            numberOfObject++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckActivation();
    }

    void CheckActivation()
    {
        for (int i = 0; i < activeObject.Count; i++) //Check Acitve
        {
            if (activeObject[i].activeSelf == true)
            {
                currentActiveObject++;
            }
            else
            {
                return;
            }
        }

        if (currentActiveObject == numberOfObject) //If all active or not, switch bool
        {
            allActive = true;
            Debug.Log("All Active");
        }

        else if (currentActiveObject < numberOfObject)
        {
            allActive = false;
            Debug.Log("Some is Acitve");
        }
    }
}
