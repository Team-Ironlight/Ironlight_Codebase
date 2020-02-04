//-------------------------------------------------------------------------------------------------------------
// Version 1: Base Structure
//	                Objective make a Pluggable System for AI, for easy versioning Control
//
//                                        -  INSTRUCTION Below -
//
//-------------------------------------------------------------------------------------------------------------
//                    >>- Steps: for - WanderState -<<
//  1. Add NavMesh Agent
//  2. Attache the Script (StateMachine)
//  3. Attache the script States (ex:WanderState) must be similar from the Script Name.
//	    - Assign Name (WanderState,or AttackState, or, FleeState, PatrolState, ChaseState)
//	    - Assign the Target , Drag the Player GameObject from the Hierarchy ,ensure the Player has Tag name "Player"
//	    - Assign the ("On Enemy Min Distance State" = WanderState), and ("Wander Finish State" = WanderState)
//  4. From the Inspector Define the StateMachine
//	    - Setup Available States (Size = 1)
//	    - Drag the Attached Script(the Steps Number 3) from the Inspector into "Element 0"
//	    - Do the same for "Current State" , attached the script (the steps number 3) on it
//  5. Final, ensure the "is Active" was tick (set into True)
//
// 	Note: Expected Output , The AI will move random Destination. 
//-------------------------------------------------------------------------------------------------------------
//                    >>- Steps: for - PatrolState -<<
//  1. Add NavMesh Agent
//  2. Attache the Script (StateMachine)
//  3. Attache the script States (ex:PatrolState) must be similar from the Script Name.
//	    - Assign Name (WanderState,or AttackState, or, FleeState, PatrolState, ChaseState)
//	    - Assign the Target , Drag the "Player GameObject" from the Hierarchy ,ensure the Player has Tag name "Player"
//	    - Then Define the Waypoints Patrol, by dragging the GameObject(waypoint from the Hierarchy) on it , must be morethan-one waypoints
//  4. From the Inspector Define the StateMachine
//	    - Setup Available States (Size = 1)
//	    - Drag the Attached Script(the Steps Number 3) from the Inspector into "Element 0"
//	    - Do the same for "Current State" , attached the script (the steps number 3) on it
//  5. Final, ensure the "is Active" was tick (set into True)
//
// 	Note: Expected Output , The AI will move into assigned waypoints
//-------------------------------------------------------------------------------------------------------------
//                   >>- Steps: for - AttackState -<<
//  1. Add NavMesh Agent
//  2. Attache the Script (StateMachine)
//  3. Attache the script States (ex:AttackState) must be similar from the Script Name.
//	    - Assign Name (WanderState,or AttackState, or, FleeState, PatrolState, ChaseState)
//	    - Assign the Target , Drag the "Player GameObject" from the Hierarchy ,ensure the Player has Tag name "Player"
//	    - Define Decision Making :
//		    -> On Enemy Lost State = "WanderState"
//		    -> On Enemy Chase Distance = "ChaseState"
//		    -> On Enemy Attack Distance =  "AttackState"	
//	    - Define Sensors :
//		    -> Chase Distance = 10 (Yellow WireSphere)
//		    -> Attack Distance = 6 (Blue WireSphere)	
//  4. From the Inspector Define the StateMachine
//	    - Setup Available States (Size = 1)
//	    - Drag the Attached Script(the Steps Number 3) from the Inspector into "Element 0"
//	    - Do the same for "Current State" , attached the script (the steps number 3) on it
//  5. Final, ensure the "is Active" was tick (set into True)
//
//	Note: To Test this(initial version), move the Player(A-W-S-D) near to the AI, upon entering into Yellow WireSphere "AI" will update his State,
//		you must move the player to the Blue WireSphere so that the AI will do the Attack!
//		Try to run-away atleast outside the Yellow WireSphere, expecting the AI will stop following/attacking the Player.
//-------------------------------------------------------------------------------------------------------------
// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/23/2020
// ----------------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//-------------------------------------------------------------------------------------------------------------                                      
//                                  -  Please Follow the INSTRUCTION Above -
//-------------------------------------------------------------------------------------------------------------
namespace IronLight
{
    [System.Serializable]
    public class StateMachine : MonoBehaviour
    {
        // -----------------------------------------------------
        // Programmer : Phil
        // Note	:	main idea (interfaces commented below) but Interfaces classes, 
        //			Having Technical issue So we adopt a change (Tweak Main Idea)
        //			So ive Got Advice from Dominic that i have to use Abstract instead of using the interfaces class
        // -----------------------------------------------------


        //public interface BaseState
        //{
        //    public string Name;
        //    void OnEnter();                             // When entering state
        //    void Tick();                                // Replacement for Update (Called every frame when it is the current state)
        //    public abstract string CheckConditions();   // Validation and Decission Check and must be Return here
        //    void OnExit();                              // When Exiting state
        //}


        // -----------------------------------------------------------------------------------------------------------
        // Programmer : Phil
        // Description	:	Objective: create a Base State which is a Systematically, 
        //			kinda like pluggable States, this is to Break the AI Functionality into pieces(Class)
        //			Recommended by Dominic that i have to use Abstract here instead of using the interfaces class
        // Research: This New System got the idea from the (YouTuber) Jason Weimann!
        //           the Enhancement version still in-Progress!!!
        // -----------------------------------------------------------------------------------------------------------
        
        [System.Serializable]                                  // To do: Future enhancement Seperate this into another script(Class)
        public abstract class BaseState : MonoBehaviour
        {         
            public string Name;
            public abstract void OnEnter();                                     // When entering state
            public abstract void Tick();                                        // Replacement for Update (Called every frame when it is the current state)
            public abstract string CheckConditions();                           // Validation "Decission Check" , and must be Return here
            public abstract void OnExit();                                      // When Exiting state
        }


        public BaseState[] AvailableStates;                                     // Container for BaseState
        [Header("State On Real Time.")]
        public BaseState CurrentState;                                          // Current BaseState
       

        [Header("Ability and Decisions.")]
        public bool isActive = true;                                            // Flag for this Class, if "Active" will do the process

        [Header("Components.")]
        private UnityEngine.AI.NavMeshAgent _navMeshAgent;                      //reference to the navmesh agent.
        private Animator _aniMator;
        private Rigidbody _arcRigidBody;

        //local variable
        [HideInInspector] public bool isOnAnimation = false;
        [HideInInspector] private bool isOnAttackMode = false;
        [HideInInspector] private bool isOnSafeMode = false;
      
        private string stateId;
        void Start()
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();        // Initialize all the Components
            _aniMator = GetComponent<Animator>();
            _arcRigidBody = GetComponent<Rigidbody>();

            if (CurrentState != null)                                           // Precaution Check
            {
                CurrentState.OnEnter();
            }
        }

        void Update()
        {
            if (isActive == true && CurrentState != null)                       // Precaution Check
            {
                CurrentState.Tick();                                            // called once per frame
                 stateId = CurrentState.CheckConditions();
                if (stateId.Length > 0)
                {

                    foreach (BaseState s in AvailableStates)                          //Let us Update the States   
                    {
                        if (s.Name == stateId)
                        {
                            CurrentState.OnExit();                                    // ChangeState function call placed here.  
                            CurrentState = s;
                            CurrentState.OnEnter();
                            break;
                        }
                    }

                }

            }
        }

        public void ChangeState(BaseState state)                                    // This Code has been move & combine to the above Update function call
        {
            if (state == null)
                return;
            if (CurrentState != null)
            {
                CurrentState.OnExit();
            }
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public void SetActive(bool isActive)                                        // Update the Flag for this Class
        {
            this.isActive = isActive;
        }


        // To Do: This is for next version, to cater Animation using Animator
        public void AgentEventHandler()                                            // Our Event function call for the animator
        {


            if (!isOnAnimation)
            {
                _navMeshAgent.enabled = false;                                    // We need this to allow us to do Animation, and avoid sticking up to the Ground.  
                isOnAnimation = true;
      
            }
            else
            {
                _navMeshAgent.enabled = true;                                     // Enabled back to allow the NavAgent for Pathfinding / Obstacle avoidance 
                isOnAnimation = false;
            }

        }
    }
}