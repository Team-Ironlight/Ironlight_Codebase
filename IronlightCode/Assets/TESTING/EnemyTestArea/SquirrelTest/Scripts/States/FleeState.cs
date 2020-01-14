using UnityEngine;
using System.Collections;
using IronLight;

[System.Serializable]
public class FleeState : StateMachine.BaseState {

    public Transform target;
    //   public float breakAwayMin = 30.0f;
    //public float breakAwayMax = 60.0f;

    public float minFleeTime = 2.0f;
    public float maxFleeTime = 5.0f;
    private float fleeTime = 0.0f;

	//public Vector3 prebreakDirection = Vector3.up;
	//public Vector3 breakDirection = Vector3.right;
    public float multiplyBy;
    //public const float BreakMagnitude = 10.0f;

    public string OnFleeTimerUp = "FleeState";
    public float minDistanceToRunAway = 1.0f;
    public string OnEnemyMinDistanceFlee = "FleeState";


    private UnityEngine.AI.NavMeshAgent agent;   //reference to the navmesh agent.

    public override void   OnEnter()
	{

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //      int directionMod = (Random.Range(0, 2) == 1 ? 1 : -1);
        //float breakAngle = Random.Range(breakAwayMin, breakAwayMax) * directionMod;
        //breakDirection = Quaternion.Euler(0, 0, breakAngle) * prebreakDirection;
        //breakDirection.Normalize();

        fleeTime = Random.Range(minFleeTime, maxFleeTime);


    }
	
	public override void   Tick()
	{
		fleeTime -= Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(transform.position - target.position);

        Vector3 runTo = transform.position + transform.forward * multiplyBy;

        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(runTo, out hit, 5, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable"));

        agent.SetDestination(hit.position);
    }

	public override string CheckConditions()
    {
        if (Vector3.Distance(transform.position, target.position) > minDistanceToRunAway)
        {
            return OnEnemyMinDistanceFlee;
        }
        else if (fleeTime <= 0.0f)
		{
			return OnFleeTimerUp;
		}
        else
		    return "";
	}

	public override void   OnExit()
	{
		//To Do  Destroy effects / animation
	}
}
