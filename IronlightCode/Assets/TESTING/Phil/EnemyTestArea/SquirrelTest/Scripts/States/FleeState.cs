// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James    /  Alteration dates below
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using IronLight;

[System.Serializable]
[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New FleeState")]
public class FleeState : StateMachine.BaseState
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif
    private Transform target;
    
    public float minFleeTime = 2.0f;
    public float maxFleeTime = 5.0f;
    private float fleeTime = 0.0f;   
    public float multiplyBy;

    public string OnFleeTimerUp = "FleeState";
    public float minDistanceToRunAway = 1.0f;
    public string OnEnemyMinDistanceFlee = "FleeState";

    private UnityEngine.AI.NavMeshAgent agent;                                                                                                  //reference to the navmesh agent.

    public StateMachine behaviour { get; protected set; }

    public override void   OnEnter(MonoBehaviour runner)                                                                                        // This is called before the first frame Tick()
    {
        agent = behaviour.GetComponent<UnityEngine.AI.NavMeshAgent>();                                                                          //Initialized All the Components
        target = GameObject.FindWithTag("Player").transform;
        Name = this.GetType().ToString();
        fleeTime = Random.Range(minFleeTime, maxFleeTime);      
    }
	
	public override void   Tick(MonoBehaviour runner)                                                                                           //Called every frame , Initiate by the StateMachine
    {
		fleeTime -= Time.deltaTime;
        behaviour.transform.rotation = Quaternion.LookRotation(behaviour.transform.position - target.position);
        Vector3 runTo = behaviour.transform.position + behaviour.transform.forward * multiplyBy;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(runTo, out hit, 5, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable"));
        agent.SetDestination(hit.position);
    }

	public override string CheckConditions(MonoBehaviour runner)                                                //Validation and update the current States
    {
        if (Vector3.Distance(behaviour.transform.position, target.position) > minDistanceToRunAway)
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

	public override void   OnExit(MonoBehaviour runner)
	{
		//To Do  Destroy effects / animation
	}
}





































































































































































































































































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh