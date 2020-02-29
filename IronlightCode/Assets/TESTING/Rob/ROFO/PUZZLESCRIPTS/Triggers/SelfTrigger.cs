using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //NEED CUSTOM COLLIDER SCRIPT FOR THIS TO WORK PROPERLY
    public class SelfTrigger : MonoBehaviour, ITrigger
    {
        public string input;
        public string tagTrigger;
        private AChange[] changes;


        //calling this will set off
        public void Trigger()
        {
            for (int i = 0; i < changes.Length; i++)
            {
                changes[i].Change();
            }
        }

        //get all changes attached to this object
        private AChange[] GatherChanges()
        {
            return GetComponents<AChange>();
        }

        private void Start()
        {
            changes = GatherChanges();
        }



        //need generic statement that will work here
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == tagTrigger &&
                Input.GetKeyUp(KeyCode.P))
            {
                //send call to input manager to put in interaction list
                PuzzleInteractionManager.SetInteractObject(this);
                Trigger();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == tagTrigger &&
                Input.GetKeyUp(KeyCode.P))
            {
                //send call to input manager to put in interaction list
                PuzzleInteractionManager.RemoveInteractObject(this);
                Debug.Log("FUCKKK");
                this.Trigger();
            }
        }
    }

}
