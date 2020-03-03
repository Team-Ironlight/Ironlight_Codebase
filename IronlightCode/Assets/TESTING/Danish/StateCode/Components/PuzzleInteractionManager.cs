using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROFO;

public class PuzzleInteractionManager : MonoBehaviour
{
    //can interact with objects
    static public bool interaction = false;

    //objects it interacts with
    static private List<ITrigger> interactObjects = new List<ITrigger>();

    public static PuzzleInteractionManager singleton = null;

        
    public PuzzleInteractionManager()
    {
        
        if (singleton == null)
        {
            singleton = this;
        }
        
    }

        
    //objects will call these functions to add to the players list
    static public void SetInteractObject(ITrigger t)
    {
        Debug.Log("<color=blue>Added interaction</color>");
        interactObjects.Add(t);
        Debug.Log(interactObjects.Count);

        interaction = true;
    }

    static public void RemoveInteractObject(ITrigger t)
    {
        Debug.Log("<color=purple>Removed interaction</color>");
        interactObjects.Remove(t);

        if (interactObjects.Count == 0)
        {
            interaction = false;
        }
    }

    static public void CallTriggers()
    {
        //call all interactions

        Debug.Log("Triggered");
        Debug.Log(interactObjects.Count);
        foreach (ITrigger t in interactObjects)
        {
            Debug.Log(interactObjects.Count);
            t.Trigger();
        }

    }

}
