//Programmer : Phil James
using UnityEngine;
using System.Collections;
using IronLight;

[System.Serializable]
public class ChaseState : StateMachine.BaseState
{
    [Header("Target")]
	public Transform target;
    [Header("Decision Making")]
    public float dampTime = 1.25f;
	private Vector3 velocity = Vector3.zero;
	public float minDistanceToTarget = 1.0f;
	public string OnEnemyLostState = "FleeState";                       //To Do:  Convert this to enum
    public string OnEnemyMinDistanceState = "FleeState";                //To Do:  Convert this to enum
    [Header("-------------")]
    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    private UnityEngine.AI.NavMeshAgent agent;   //reference to the navmesh agent.


    public override void  OnEnter()
	{
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	public override void  Tick()
	{
		if(target != null)
        {
            // enable the agent to move again
            agent.isStopped = false;
            agent.speed = run_Speed;

            Vector3 delta = target.position - transform.position;
			Vector3 destination = transform.position + delta;
        //    agentUpdate = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

            agent.SetDestination(destination);
		}
	}
	public override string CheckConditions()
	{
		
		if(target == null)
		{
           
			return OnEnemyLostState;
		}
		else if(Vector3.Distance(transform.position, target.position) < minDistanceToTarget)
		{
			return OnEnemyMinDistanceState;
		}
		else
			return "";
	}
	public override void   OnExit()
	{
		// TODO destroy Effects / Animation
	}
}
