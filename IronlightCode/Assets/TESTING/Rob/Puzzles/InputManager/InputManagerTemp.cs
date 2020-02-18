using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerTemp : MonoBehaviour
{
    //can interact with objects
    static public bool interaction = false;

    //objects it interacts with
    static private List<ITrigger> interactObjects = new List<ITrigger>();


    //objects will call these functions to add to the players list
    static public void SetInteractObject(ITrigger t)
    {
        Debug.Log("Added interaction");
        interactObjects.Add(t);

        interaction = true;
    }

    static public void RemoveInteractObject(ITrigger t)
    {
        Debug.Log("Removed interaction");
        interactObjects.Remove(t);

        if(interactObjects.Count == 0)
        {
            interaction = false;
        }
    }


    //temp
    private void Update()
    {
        //call all interactions 
        if(interaction && Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("<color=red>ENTER</color>");
            Debug.Log("iterate through each interaction");
            foreach (ITrigger t in interactObjects)
            {
                t.Trigger();
            }
        }
    }
}
