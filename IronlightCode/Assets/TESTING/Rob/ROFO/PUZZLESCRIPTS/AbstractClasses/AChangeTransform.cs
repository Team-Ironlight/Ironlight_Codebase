using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public abstract class AChangeTransform : AChange
    {
        public bool hasCorrectPosition = false;
        public int correctPosition;
        public bool IsInCorrectPos()
        {
            if (indexPos == correctPosition)
            {
                return true;
            }

            return false;
        }

        public bool limitedMoves = false;
        //for if limited moves is true
        public uint NumberOfMoves;

        private bool isMoving = false;
        public bool GetIsMoving()
        {
            return isMoving;
        }
        public void SetIsMoving(bool b)
        {
            isMoving = b;
        }

        //all these have this
        public int startPos = 0;
        public float speed = 1f;

        public int indexPos;
        public float gizmosSize = 1f;


        protected bool ChangeLogic()
        {
            //prevent running this is no more moves left
            if (limitedMoves && NumberOfMoves <= 0)
            {
                Debug.Log("No Change");

                // WASIQ ADDED HERE... SORRY! *********************************************************************************************************************************************
                // Make the crystal spawn the beam on current position. The only change wasiq made.
                hasCorrectPosition = false;
                return false;
            }
            else
            {
                Debug.Log("Check if going");
                if (GetIsMoving() == false)
                {
                    if (limitedMoves)
                    {
                        NumberOfMoves--;
                    }

                    return true;
                }

                return false;
            }
        }

        protected Coroutine c = null;

        //must fill in their own 
        protected abstract IEnumerator Work();

        protected abstract void Setup();
    }
}
