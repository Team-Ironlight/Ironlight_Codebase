//Programmer : Phil James
//Description :  Version 1 (January 14, 2020)
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using IronLight;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class WanderState : StateMachine.BaseState
{
    [Header("Target")]
    public Transform target;
    public float minDistanceToTarget = 2.0f;

    [Header("Wander Setup")]
    public float fov = 120f;                                                    //To Do: Seperate this into another Class using WireSphere Radius
    public float viewDistance = 10f;
    public float wanderRadius = 7f;

    [Header("Decision Making")]
    private bool isAware = false;
    private Vector3 wanderPoint;

    public string OnEnemyMinDistanceState = "FleeState";                        //To Do:  Convert this to enum
    public string WanderFinishState = "FleeState";                              //To Do:  Convert this to enum

   
    private UnityEngine.AI.NavMeshAgent agent;                                  //reference to the navmesh agent.
   
    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;
  
     //Local Variables
    private Renderer renderer;                                                  //Temporary Variable

    public override void OnEnter()                                              // This is called before the first frame Tick()
    {
        agent = GetComponent<NavMeshAgent>();          

        renderer = GetComponent<Renderer>();                                    //Temporary - Removed this on Final Version
        wanderPoint = RandomWanderPoint();                                      //Destination to locate
        

    }

    public override void Tick()                                                 //Called every frame , Initiate by the StateMachine
    {
        // tell nav agent that he can move
        agent.isStopped = false;
        agent.speed = walk_Speed;                                                   //Agent Speed
           
            if (isAware)
            {
                agent.SetDestination(target.transform.position);
                renderer.material.color = Color.red;                                 //Temporary - Removed this on Final Version                
            }
            else
            {
               
                agent.SetDestination(wanderPoint);

                renderer.material.color = Color.blue;                               //Temporary - Removed this on Final Version     
        }
       
    }


    public override string CheckConditions()                                        //Decisions has been made here
    {
        SearchForPlayer();                                                                      // To Do: Enhancement, like is it needed to Update the State here into "AttackState" or Right-away this enemy need to do the Attack Actions ?                

        if (Vector3.Distance(transform.position, wanderPoint) < minDistanceToTarget)            //  Has been reached ?
        {
            wanderPoint = RandomWanderPoint();                                                  //  Generate Destination

            return WanderFinishState;                                                           //  Update the State
        }
        else if (Vector3.Distance(transform.position, target.position) < minDistanceToTarget)   //  If Within Perimeter
        {
          
            return OnEnemyMinDistanceState;                                                     // Update the State
        }
        else
            return "";

    }

 
    public override void OnExit()
    {
        //To Do:  Destroy effects / animation
    }

    public void OnAware()
    {
        isAware = true;
    }
    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, UnityEngine.AI.NavMesh.AllAreas);  //-1
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(target.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if(Physics.Linecast(transform.position, target.transform.position, out hit, -1))
                if (hit.transform.CompareTag("Player"))
                {        
                        OnAware();
                }
            }
        }
    }
    public void Wander()                                                                             //To Do:  next version will remove this, not needed
    {

        if (Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }

    }

}
