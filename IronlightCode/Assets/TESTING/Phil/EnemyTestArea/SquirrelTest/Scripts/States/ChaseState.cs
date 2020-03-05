// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James    /  Alteration dates below
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// Date:   03/3/2020        Version 4
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using TMPro;                            //Debugging Purposes
using IronLight;

[System.Serializable]
[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New ChaseState")]
public class ChaseState : Phil_StateMa.BaseState
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif

    [Header("Target")]
	private Transform _mTarget;
    private GameObject _oTarget;
    [Header("Decision Making")]
    public string OnEnemyLostState = "ChaseState";                                               //To Do:  Convert this to enum
    private string OnEnemyChaseDistance = "ChaseState";                                         //To Do:  Convert this to enum
    private bool isAware = false;
    private bool _playerRunAway = false;
   // private bool isOnPerimeter;

    [Header("-------------")]
    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    [Header("Components Entity")]
   // public Transform ComponentContainer;
    private NavMeshAgent _navMeshAgent;                                                         //reference to the navmesh agent.
    private Animator _aniMator;

    private AI_AbilityManager _updateMinMax;                                                    //Tell the Random Attacks about the MinMax value for Perimeter check
    private MonoBehaviour _mRunner;                                                             // Local use

    private float _maxDistanceToChase;
    private float _minDistanceToChase;



    public override void  OnEnter(MonoBehaviour runner)                                                             // This is called before the first frame
    {
        _mRunner = runner;
      
        _oTarget = GameObject.FindWithTag("Player").gameObject;
        if (_oTarget != null)
        {
            _mTarget = _oTarget.transform;
        }
        else
        {
            Debug.Log("No Player game objects found in the 'Scene'");
        }
        _navMeshAgent = runner.GetComponent<NavMeshAgent>();
        _aniMator = runner.GetComponent<Animator>();
        _updateMinMax = runner.GetComponent<AI_AbilityManager>();
        
        Name = this.GetType().ToString();

        

    }
	public override void  Tick(MonoBehaviour runner)                                                                 //Called every frame after the first frame, Initiate by the StateMachine
    {
		if(_mTarget != null && !isDead)
        {
            if(_navMeshAgent.enabled ==true)
            {

                _maxDistanceToChase = runner.GetComponent<Phil_StateMa>().Get_MaxDistanceChase;
                _minDistanceToChase = runner.GetComponent<Phil_StateMa>().Get_MinDistanceChase;

                _updateMinMax.Set_MaxDistance = _maxDistanceToChase;
                _updateMinMax.Set_MinDistance = _minDistanceToChase;

              

                if (isAware)
                {
                    Vector3 dirToTarget = (_mTarget.position - runner.transform.position).normalized;

                    // Turn the enemy facing to the Player
                    runner.transform.rotation = Quaternion.Slerp(runner.transform.rotation,
                                        Quaternion.LookRotation(dirToTarget),
                                        1.0f * Time.deltaTime);

                    Vector3 destination = _mTarget.position + dirToTarget;

                    // Validate if the distance between the player and the enemy
                    // if the distance between enemy and player is less than attack distance
                    if ((Vector3.Distance(runner.transform.position, _mTarget.position) <= _maxDistanceToChase))
                    {
                        _navMeshAgent.isStopped = false;                        //Tell the Agent to move
                        _navMeshAgent.speed = run_Speed;

                        if ((Vector3.Distance(runner.transform.position, _mTarget.position) > _minDistanceToChase))
                        {
                            _navMeshAgent.SetDestination(destination);
                        }
                        else
                        {
                            dirToTarget = (_navMeshAgent.transform.position - _navMeshAgent.transform.position).normalized;
                            Vector3 attackPosition = _navMeshAgent.pathEndPosition - dirToTarget * (_maxDistanceToChase);
                            _navMeshAgent.SetDestination(attackPosition);

                        }


                    }
                    else if ((Vector3.Distance(runner.transform.position, _mTarget.position) >= _minDistanceToChase))
                    {
                       
                        _playerRunAway = true;
                    }

                   
                }
            }
        }
    }
	public override string CheckConditions(MonoBehaviour runner)                                                            //Decision Making - Called every frame after the First Frame 
    {
        if (_mTarget == null || isDead) {  return "";  }

        if (_playerRunAway)
        {
            isAware = false;
            _playerRunAway = false;

            return OnEnemyChaseDistance;
        }


        if ((Vector3.Distance(runner.transform.position, _mTarget.position) >= _maxDistanceToChase))                            //Chase State
        {
          
            OnAware();
            return "";
        }
        else if (Vector3.Distance(runner.transform.position, _mTarget.position) <= _minDistanceToChase)                         // Switch to <Attack State>
        {
            return OnEnemyLostState;
        }


        return "";                                                                                                              // Return empty String so that the StateMachine bypass validation check, and retained the current states, This saves memory calls
    }
	public override void   OnExit(MonoBehaviour runner)
	{
		// TODO destroy Effects / Animation
	}
    public void OnAware()
    {
        isAware = true;
    }

}






































































































































































































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh