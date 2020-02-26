using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class MoveControlY : MonoBehaviour
    {
        JumpForce jF;
        GravityForce gF;
        GroundCheck gC;
        CeilingCheck cC;

        public Transform parent;
        public float yStart;
        public float yDisplacement = 0.0f;
        public float yDisplacementPrevious = 0.0f;

        [Header("Jumping")]
        public int maxJumps = 1;
        public int totalJumps = 0;
        public float jumpOffset = 0.1f;

        [Header("Falling")]
        [Range(0.1f, 5f)] public float fallingGravity = 0.5f;

        [Header("Landing")]
        [Range(0.01f, 1f)] public float landingZone = 0.1f;

        public enum YState { None, Grounded, Falling, Jumping }
        public YState currentYstate = YState.Grounded;

        //determing for animator
        public enum JumpPhase { None, Ascending, Descending, JumpAgain }
        public JumpPhase currentJumpPhase = JumpPhase.None;


        private void Awake()
        {
            jF = GetComponent<JumpForce>();
            gF = GetComponent<GravityForce>();

            gC = GetComponent<GroundCheck>();
            cC = GetComponent<CeilingCheck>();
        }

        private void Start()
        {
            ConditionsALT();
        }

        // Update is called once per frame
        void Update()
        {
            YState previous = currentYstate;

            currentYstate = ConditionsALT();
            StatesALT();

            if (previous != currentYstate)
            {
                //adjust animator
            }
            //Conditions();
            //States();
        }


        private YState ConditionsALT()
        {
            //STATES
            if (currentYstate == YState.Grounded)
            {
                //if grounded and jump input, set jumping
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    totalJumps++;

                    yStart = parent.position.y;
                    yDisplacement = 0.0f;
                    yDisplacementPrevious = 0.0f;
                    jF.Set(true);
                    gF.Set(true);
                    return YState.Jumping;
                }

                //if grounded but not on ground, set falling
                if (parent.position.y - gC.minY > 0f)
                {
                    yStart = parent.position.y;
                    jF.Set(false);
                    gF.Set(true);
                    return YState.Falling;
                }
            }
            else if (currentYstate == YState.Jumping)
            {
                //if grounded and jump input, set jumping
                if (currentJumpPhase == JumpPhase.JumpAgain &&
                    Input.GetKeyDown(KeyCode.Space) &&
                    totalJumps < maxJumps)
                {
                    totalJumps++;

                    yStart = parent.position.y;
                    yDisplacement = 0.0f;
                    yDisplacementPrevious = 0.0f;
                    jF.Reset();
                    gF.Reset();
                    return YState.Jumping;
                }

                //if jumping and hit ceiling, set falling
                if (parent.position.y - cC.maxY >= 0.0f)
                {
                    //hit a ceiling
                    yStart = parent.position.y;
                    jF.Set(false);
                    gF.Reset();
                    currentJumpPhase = JumpPhase.Descending;
                    return YState.Falling;
                }
            }
            else if (currentYstate == YState.Falling)
            {

            }

            //if not grounded and hit ground, set grounded
            if (parent.position.y - gC.minY <= 0.0f &&
                currentYstate != YState.Grounded)
            {
                totalJumps = 0;

                jF.Set(false);
                gF.Set(false);
                currentJumpPhase = JumpPhase.None;
                return YState.Grounded;
            }

            return currentYstate;
        }

        private void StatesALT()
        {
            //STATES, make states???
            if (currentYstate == YState.Grounded)
            {
                //set y pos to ground
                parent.position = new Vector3(parent.position.x, gC.minY, parent.position.z);
            }

            if (currentYstate == YState.Falling)
            {
                yDisplacement = gF.GetGravity() * fallingGravity;
                Vector3 change = new Vector3(parent.position.x, yStart + yDisplacement, parent.position.z);
                parent.position = change;
            }

            if (currentYstate == YState.Jumping)
            {
                yDisplacementPrevious = yDisplacement;
                yDisplacement = jF.GetJump() + gF.GetGravity();
                Vector3 change = new Vector3(parent.position.x, yStart + yDisplacement, parent.position.z);
                parent.position = change;

                if (yDisplacement > yDisplacementPrevious)
                {
                    currentJumpPhase = JumpPhase.Ascending;
                }
                else if (yDisplacement + jumpOffset < yDisplacementPrevious)
                {
                    currentJumpPhase = JumpPhase.JumpAgain;
                }
                else if (yDisplacement < yDisplacementPrevious)
                {
                    currentJumpPhase = JumpPhase.Descending;
                }
            }


            Debug.Log("<color=purple>Falling: </color>" + currentYstate.ToString());
        }
    }
}
