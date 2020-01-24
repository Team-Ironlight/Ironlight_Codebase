//Programmer : Phil James
//Description :  Version 2 (January 16, 2020)
using UnityEngine;
using System.Collections;
using IronLight;

[System.Serializable]
public class ChaseState : StateMachine.BaseState
{
    [Header("Target")]
	private Transform target;
    [Header("Decision Making")]
    public string OnEnemyLostState = "FleeState";                       //To Do:  Convert this to enum
    private string OnEnemyChaseDistance = "ChaseState";                 //To Do:  Convert this to enum
  //  private bool isAware = false;
    private bool isOnPerimeter;


    [Header("Sensor Color Cyan.")]
    public bool ShowWireSphere_Sensor;
    [Tooltip("Min - Cyan WireSphere.")]
    [Range(0f, 10f)]
    public float minDistanceToChase = 10.0f;                            //Minimum Chase Distance
    [Tooltip("Max - Cyan WireSphere.")]
    [Range(0f, 40f)]
    public float maxDistanceToChase = 14.0f;                            //Maximum Chase Distance


    [Header("-------------")]
    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    private UnityEngine.AI.NavMeshAgent agent;                          //reference to the navmesh agent.


    public override void  OnEnter()                                      // This is called before the first frame Tick()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        Name = this.GetType().ToString();
    }
	public override void  Tick()                                         //Called every frame , Initiate by the StateMachine
    {
		if(target != null)
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // Turn the enemy facing to the Player
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                Quaternion.LookRotation(dirToTarget),
                                1.0f * Time.deltaTime);
                      
			Vector3 destination = transform.position + dirToTarget;

            // Validate if the distance between the player and the enemy
            // if the distance between enemy and player is less than attack distance
            if ((Vector3.Distance(transform.position, target.position) <= maxDistanceToChase))
            {
        
                // enable the agent to move again
                agent.isStopped = false;
                agent.speed = run_Speed;

                agent.SetDestination(destination);

            }
           
        }
    }
	public override string CheckConditions()                                             //Decision Making
	{
        if (target == null) {  return "";  }
        
        Collider[] overlapResults = new Collider[10];
        int numFound = Physics.OverlapSphereNonAlloc(transform.position, maxDistanceToChase, overlapResults);
            
        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {              
                if (overlapResults[i].transform == target)
                {
                    Debug.DrawLine(transform.position, overlapResults[i].transform.position, Color.yellow);
                    if ((Vector3.Distance(transform.position, target.position) > minDistanceToChase))
                    {
                      
                        return OnEnemyChaseDistance;
                    }
                    else 
                    {
                       
                        return OnEnemyLostState;                
                    }

                }
               
            }
         
        }


        return "";
    }
	public override void   OnExit()
	{
		// TODO destroy Effects / Animation
	}
    private void OnDrawGizmos()
    {
        if (ShowWireSphere_Sensor)
        {
            //Minimum Distance WireSphere
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, minDistanceToChase);

            //Maximum Distance WireSphere
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, maxDistanceToChase);
        }
    }
}
