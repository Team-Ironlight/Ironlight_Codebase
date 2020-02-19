using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //basic jump that will print to the console
    public class JumpAnimator : MonoBehaviour
    {
        [Header("Jump Variables")]
        public float jumpForce = 1f;
        public float jumpGravity = 1f;
        [Header("JumpChange")]
        public bool isJumping = false;
        public float startY;
        public float yDisplacement = 0f;

        [Header("Jump Time")]
        public float count = 0f;
        public float countTangent;
        public bool isFalling = false;


        private void Start()
        {
            Reset();
            JumpDerivativeCalculation();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startY = transform.position.y;
                AnimatorFacade.SetParameter(false, "Grounded");
                AnimatorFacade.SetParameter(true, "Jumping");
                isJumping = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
                transform.position = new Vector3(transform.position.x,
                                                 startY,
                                                 transform.position.z);

                AnimatorFacade.SetParameter(false, "Jumping");
                AnimatorFacade.SetParameter(false, "Falling");
                AnimatorFacade.SetParameter(true, "Grounded");
                AnimatorFacade.SetAnimation("Landing");
                Reset();
            }

            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                jumpCalculation();
                MoveTransform();
            }
        }

        private void Reset()
        {
            count = 0.0f;
            yDisplacement = 0.0f;
        }

        private void jumpCalculation()
        {
            count += Time.deltaTime;

            yDisplacement = (jumpForce * count) - (0.5f * jumpGravity * (count * count));

            if (count > countTangent)
            {
                AnimatorFacade.SetParameter(false, "Jumping");
                AnimatorFacade.SetParameter(true, "Falling");
            }

            if (count < countTangent)
            {
                Debug.Log("<color=blue>Jumping</color>");
            }
            else
            {
                Debug.Log("<color=green>Falling</color>");
            }
            //Debug.Log("<color=purple>Count: </color>" + count + " <color=red>Displacement: </color>" + yDisplacement);
        }

        private void MoveTransform()
        {
            transform.position = new Vector3(transform.position.x,
                                             startY + yDisplacement,
                                             transform.position.z);
        }

        private void JumpDerivativeCalculation()
        {
            countTangent = jumpForce / jumpGravity;
        }
    }
}
