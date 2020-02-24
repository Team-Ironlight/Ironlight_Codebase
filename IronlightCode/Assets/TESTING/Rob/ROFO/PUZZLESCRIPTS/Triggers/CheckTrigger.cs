using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //this script is for objects that need to check a puzzles state
    //and trigger a dependant object when the puzzle is correct
    public class CheckTrigger : MonoBehaviour, ITrigger
    {
        public string GroupToCheck;
        public string GroupToTrigger;
        public string tagTrigger;

        private Coroutine c = null;

        //when this object gets triggered it checks the check group
        //if true than calls trigger group
        public void Trigger()
        {
            Debug.Log("CHECK TRIGGER CHECK");
            c = null;
            c = StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            Debug.Log("Wait");
            int frames = 0;
            while (PuzzleManager.IsGroupMoving(GroupToCheck) == false)
            {
                frames++;
                Debug.Log("Waiting..." + frames);
                yield return new WaitForEndOfFrame();
            }

            //check puzzle correct
            bool puzzleCorrect = PuzzleManager.IsGroupInCorrectPosiition(GroupToCheck);

            if (puzzleCorrect)
            {
                //check group isn't moving
                bool notChangingGroup = PuzzleManager.IsGroupMoving(GroupToTrigger);

                if (notChangingGroup)
                {
                    PuzzleManager.TriggerThisGroup(GroupToTrigger);
                }
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
}
