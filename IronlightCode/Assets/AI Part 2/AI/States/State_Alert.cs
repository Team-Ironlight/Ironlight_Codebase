using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public class State_Alert : IState
    {
        //constructor
        public State_Alert(Transform parent,
                           float speedMin,
                           float speedMax,
                           float rotMin,
                           float rotMax)
        {
            this.parent = parent;
            this.speedMin = speedMin;
            this.speedMax = speedMax;
            this.rotMin = rotMin;
            this.rotMax = rotMax;
        }

        private Transform parent;
        private float speedMin;
        private float speedMax;
        private float rotMin;
        private float rotMax;

        private float rotationSpeed;
        private float rotationAmount;
        private float rotationCount;
        private int rotationDirection;


        public void Enter()
        {
            Debug.Log("Enter Searching");
            SetValues();
        }

        public void Execute()
        {
            Debug.Log("Execute Searching");
            //rotate
            if (rotationCount < Mathf.Abs(rotationAmount))
            {
                //rotate in y direction
                parent.Rotate(0f, rotationSpeed * rotationDirection * Time.deltaTime, 0f);
                rotationCount += rotationSpeed * Time.deltaTime;
            }
            else
            {
                SetValues();
            }
        }

        public void Exit()
        {
            Debug.Log("Exit Searching");
        }

        public void Examine()
        {

        }

        private void SetValues()
        {
            rotationSpeed = Random.Range(speedMin, speedMax);
            rotationAmount = Random.Range(rotMin, rotMax);
            rotationCount = 0f;

            do
            {
                rotationDirection = Random.Range(-1, 2);
            }
            while (rotationDirection == 0);

            //Debug.Log("New Rotation: " + rotationAmount + " " + rotationDirection);
        }
    }   
}
