﻿// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/23/2020
// ----------------------------------------------------------------------------
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
    private bool isAware = false;
    private bool _playerRunAway = false;
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

    [Header("Components")]
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;                   //reference to the navmesh agent.
    private Animator _aniMator;

    public override void  OnEnter()                                      // This is called before the first frame Tick()
    {
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _aniMator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        Name = this.GetType().ToString();
    }
	public override void  Tick()                                         //Called every frame , Initiate by the StateMachine
    {
		if(target != null)
        {
            if(_navMeshAgent.enabled ==true)
            {
                if (isAware)
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
                                             
                        _navMeshAgent.isStopped = false;
                        _navMeshAgent.speed = run_Speed;

                        _navMeshAgent.SetDestination(destination);
                    
                    }
                    else if ((Vector3.Distance(transform.position, target.position) >= maxDistanceToChase))
                    {
                        _playerRunAway = true;
                    }
                }
            }
        }
    }
	public override string CheckConditions()                                             //Decision Making
	{
        if (target == null) {  return "";  }

        if (_playerRunAway)
        {
            _aniMator.enabled = true;
            _navMeshAgent.enabled = false;
            _playerRunAway = false;
            return "ChaseState";
        }

        Collider[] overlapResults = new Collider[10];
        int numFound = Physics.OverlapSphereNonAlloc(transform.position, maxDistanceToChase, overlapResults);
            
        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {              
                if (overlapResults[i].transform == target)
                {
                    Debug.DrawLine(transform.position, overlapResults[i].transform.position, Color.yellow);
                    //if ((Vector3.Distance(transform.position, target.position) >= maxDistanceToChase))
                    //{
                    //    if (Vector3.Distance(transform.position, target.position) >= minDistanceToChase)           // Current State <Patrol State>
                    //    {
                    //        OnAware();
                    //        Name = "CHASE_ANIMATE";
                    //        return OnEnemyChaseDistance;
                    //    }

                    //}
                    //else if (Vector3.Distance(transform.position, target.position) <= maxDistanceToChase)
                    //{
                    //    if (Vector3.Distance(transform.position, target.position) <= minDistanceToChase)           // Switch to <Attack State>
                    //    {
                    //        Name = this.GetType().ToString();
                    //        _aniMator.enabled = true;
                    //        return OnEnemyLostState;
                    //    }

                    //}

                    if ((Vector3.Distance(transform.position, target.position) < maxDistanceToChase))
                    {

                        if (Vector3.Distance(transform.position, target.position) > minDistanceToChase)           // Current State <Patrol State>
                        {
                            OnAware();
                            Name = "CHASE_ANIMATE";
                            return OnEnemyChaseDistance;
                        }
                        else
                        {
                            Name = this.GetType().ToString();
                            _aniMator.enabled = true;
                            return OnEnemyLostState;
                        }



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
    public void OnAware()
    {
        isAware = true;
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






































































































































































































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh