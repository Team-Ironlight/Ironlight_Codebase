using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class CollissionCheckTrigger : MonoBehaviour, ITrigger
    {
        public string tagCollision;
        public string GroupToCheck;
        public string GroupToTrigger;


        private Coroutine c = null;
        public void Trigger()
        {
            Debug.Log("CHECK TRIGGER CHECK");
            c = null;
            c = StartCoroutine(Wait());
            ////check not moving
            //bool NotChangingCheck = TriggerManager.IsGroupMoving(GroupToCheck);

            //if(NotChangingCheck)
            //{
            //    //check puzzle correct
            //    bool puzzleCorrect = TriggerManager.IsGroupInCorrectPosiition(GroupToCheck);

            //    if(puzzleCorrect)
            //    {
            //        //check group isn't moving
            //        bool notChangingGroup = TriggerManager.IsGroupMoving(GroupToTrigger);

            //        if(notChangingGroup)
            //        {
            //            TriggerManager.TriggerThisGroup(GroupToTrigger);
            //        }
            //    }
            //}
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == tagCollision)
            {
                Trigger();
            }
        }
    }
}
