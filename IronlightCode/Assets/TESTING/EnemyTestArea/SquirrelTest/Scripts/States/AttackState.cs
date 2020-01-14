//Programmer : Phil James
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using IronLight;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AttackState : StateMachine.BaseState
{
    [Header("Target")]
    public Transform target;
    [Header("Decision Making")]                                 //To Do:  Convert this to enum
    public string OnEnemyLostState = "AttackState";             //If the Player run-away then do Default/assign State
    public string OnEnemyChaseDistance = "ChaseState";          //If the Player within Chase Perimeter
    public string OnEnemyAttackDistance = "AttackState";        //If the Player within Attack Perimeter

    private UnityEngine.AI.NavMeshAgent agent;                  //reference to the navmesh agent.


    [Header("Sensors")]
    [Tooltip("Color Yellow.")]
    [Range(0f, 15f)]
    public float chase_Distance = 7.25f;
    private float current_Chase_Distance;
    [Tooltip("Color Blue.")]
    [Range(0f, 10f)]
    public float attack_Distance = 2f;

    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;



   
    public override void OnEnter()                                               // This is called before the first frame Tick()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override void Tick()                                                  //Called every frame , Initiate by the StateMachine
    {
        if (target != null)
        {
            Vector3 agentUpdate = Vector3.zero;

            Vector3 delta = target.position - transform.position;
            Vector3 destination = transform.position + delta;
        

            // test the distance between the player and the enemy
            // if the distance between enemy and player is less than attack distance
            if (Vector3.Distance(transform.position, target.position) <= attack_Distance)  // Switch to <Attack State>
            {
                // tell nav agent that he can move
                agent.isStopped = false;
                agent.speed = walk_Speed;

                agent.SetDestination(destination);
            }
         

        }
    }

    public override string CheckConditions()                                                //Decisions has been made here
    {
        // TODO IN- Progress 
        if (target == null)
        {
          
            return "";
                                 
        } 
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance)    // Back to Default <Idle , Attack>
        {
            // player run away from enemy
            // Do Something! Back to Default
            return OnEnemyAttackDistance;
            // return OnEnemyLostState;
        }
        else if (Vector3.Distance(transform.position, target.position) >= attack_Distance)  // Back to  <Chase State>
        {
            //Back to Chase State
            Debug.Log(OnEnemyAttackDistance);
            return OnEnemyAttackDistance;
        }
        else
        {
            return "";
        }


       //    // test the distance between the player and the enemy
       //else if (Vector3.Distance(transform.position, target.position) <= chase_Distance)        // Switch To <Chase State>
       // {
       //     Debug.Log(OnEnemyChaseDistance);
       //     return OnEnemyChaseDistance;
       // }// if the distance between enemy and player is less than attack distance
       // else if (Vector3.Distance(transform.position, target.position) <= attack_Distance)  // Switch to <Attack State>
       // {
       //     Debug.Log(OnEnemyAttackDistance);
       //     return OnEnemyAttackDistance;
       // }
    }

    public override void OnExit()
    {
        // TODO destroy Effects / Animation
    }

    private void OnDrawGizmos()
    {
        //Attack Radius WireSphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attack_Distance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chase_Distance);


        //Patrol Radius WireSphere
        //Gizmos.color = Color.black;
        //Gizmos.DrawWireSphere(transform.position, patrol_Radius_Min);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, patrol_Radius_Max);

    }
}
