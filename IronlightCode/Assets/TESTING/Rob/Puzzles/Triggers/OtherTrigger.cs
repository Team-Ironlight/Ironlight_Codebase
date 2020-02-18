using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this object triggers other objects
//even single objects that are triggered should have their own group...
public class OtherTrigger : MonoBehaviour, ITrigger
{
    //doesn't need reference to anything
    //just needs the right string to send to hashtable which contains all the references
    //than sends those references to the player input which will call all the triggers
    //if the appropriate input is applied
    public string groupToTrigger;
    public string tagTrigger;

    public void Trigger()
    {
        bool work = PuzzleManager.IsGroupMoving(groupToTrigger);
        
        if(work)
        {
            PuzzleManager.TriggerThisGroup(groupToTrigger);
        }
    }


    //need generic statement that will work here
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagTrigger)
        {
            //send call to input manager to put in interaction list
            InputManagerTemp.SetInteractObject(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tagTrigger)
        {
            //send call to input manager to put in interaction list
            InputManagerTemp.RemoveInteractObject(this);
        }
    }
}
