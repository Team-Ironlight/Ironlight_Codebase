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