//Programmer : Phil James
//Description :  Version 3 (January 24, 2020)
using UnityEngine;
using System.Collections;
using IronLight;

[System.Serializable]
public class FleeState : StateMachine.BaseState {

    private Transform target;


    public float minFleeTime = 2.0f;
    public float maxFleeTime = 5.0f;
    private float fleeTime = 0.0f;


    public float multiplyBy;

    public string OnFleeTimerUp = "FleeState";
    public float minDistanceToRunAway = 1.0f;
    public string OnEnemyMinDistanceFlee = "FleeState";


    private UnityEngine.AI.NavMeshAgent agent;                                              //reference to the navmesh agent.

    public override void   OnEnter()                                                        // This is called before the first frame Tick()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();                                //Initialized All the Components
        target = GameObject.FindWithTag("Player").transform;
        Name = this.GetType().ToString();

        fleeTime = Random.Range(minFleeTime, maxFleeTime);
        
    }
	
	public override void   Tick()                                                          //Called every frame , Initiate by the StateMachine
    {
		fleeTime -= Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(transform.position - target.position);

        Vector3 runTo = transform.position + transform.forward * multiplyBy;

        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(runTo, out hit, 5, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable"));

        agent.SetDestination(hit.position);
    }

	public override string CheckConditions()                                                //Validation and update the current States
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





































































































































































































































































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh