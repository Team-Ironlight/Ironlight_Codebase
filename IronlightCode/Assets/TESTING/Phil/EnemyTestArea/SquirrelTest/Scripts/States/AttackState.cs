// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James    /  Alteration dates below
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// Date:   03/03/2020       Version 4
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using TMPro;                            //Debugging Purposes
using IronLight;


[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New AttackState")]
public class AttackState : Phil_StateMa.BaseState
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif

    [Header("Target")]
    private GameObject _oTarget;
    private Transform _mTarget;

    [Header("Components Entity")]
    private NavMeshAgent _navMeshAgent;                             //reference to the navmesh agent.
    private Animator _aniMator;

    [Header("Decision Making")]                                     //To Do:  Convert this to enum
    public string OnEnemyLostState = "ChaseState";                  //If the Player run-away then do Default/assign State

    private string OnEnemyAttackDistance = "AttackState";           //If the Player within Attack Perimeter
    private bool _playerRunAway = false;
    
    private float _maxDistanceToAttack;                             //Circle Radius, Chase Distance
    private float _minDistanceToAttack;

    [Header("Movement")]
    [SerializeField] float _slerpSpeed = 5.0f;
    public float walk_Speed = 2f;
    public float run_Speed = 4f;
    
    [Header("Special Movement")]
    public AI_SpecialMoveCoroutine SpecialMovement1;             
    public AI_SpecialMoveCoroutine SpecialMovement2;
    public AI_SpecialMoveCoroutine SpecialMovement3; 
    private AI_AbilityManager _updateMinMax;                         //Telling the Random Attacks about the MinMax Perimeter
    
    [HideInInspector]public bool isCharging;
    [HideInInspector]public float wait_Before_Attack = 2f;          //Cooling Attack
    private float attack_Timer;                                     //Cooling Attack
    

    [HideInInspector]public bool isOnAttackMode = false;

    private MonoBehaviour _mRunner;

    public override void OnEnter(MonoBehaviour runner)                                                                                                                          // This is called before the first frame 
    {
        _mRunner = runner;

        _oTarget = GameObject.FindWithTag("Player").gameObject;
        if(_oTarget != null)
        {
            _mTarget = _oTarget.transform;
        }
        else
        {
            Debug.Log("No Player game objects found in the 'Scene'");
        }
        
        _navMeshAgent = runner.GetComponent<NavMeshAgent>();
        _aniMator = runner.GetComponent<Animator>();                                                                                                                            //Initialized      
        _updateMinMax = runner.GetComponent<AI_AbilityManager>();
       
        Name = this.GetType().ToString();

     
    }

    public override void Tick(MonoBehaviour runner)                                                                                                                             //Called every frame after the First Frame , Initiate by the StateMachine
    {
        if (_mTarget != null && !isDead)
        {
            if (_navMeshAgent.enabled == true)
            {
                _maxDistanceToAttack = runner.GetComponent<Phil_StateMa>().Get_MaxDistanceAttack;
                _minDistanceToAttack = runner.GetComponent<Phil_StateMa>().Get_MinDistanceAttack;

                _updateMinMax.Set_MaxDistance = _maxDistanceToAttack;
                _updateMinMax.Set_MinDistance = _minDistanceToAttack;

                Vector3 destination = Vector3.zero;
                Vector3 dirToTarget = (_mTarget.position - runner.transform.position).normalized;                                                                            //* Danish Suggested this Solution

                // Generate a new Quaternion representing the rotation we should have
                Quaternion newRot = Quaternion.LookRotation(dirToTarget);                                                                                                    //* Danish Suggested this Solution

                // Smoothly rotate to that new rotation over time
                runner.transform.rotation = Quaternion.Slerp(runner.transform.rotation, newRot, Time.deltaTime * _slerpSpeed);                                               //* Danish Suggested this Solution

             
                destination = runner.transform.position + dirToTarget;
 
                // Validate if the distance between the player and the enemy
                // if the distance between enemy and player is less than attack distance
                if (Vector3.Distance(runner.transform.position, _mTarget.position) <= _maxDistanceToAttack)                                                                  //maintain in  <Attack State>
                {
                    _navMeshAgent.isStopped = false;                                                                                                                        // To tell the agent can move now
                    _navMeshAgent.speed = walk_Speed;

                    if ((Vector3.Distance(runner.transform.position, _mTarget.position) > _minDistanceToAttack))
                    {

                        _navMeshAgent.SetDestination(destination);

                        isCharging = _mTarget.GetComponentInChildren<LightCharging>().isCharging;
                        if (isCharging)
                        { runner.StartCoroutine(coroutineTrigger(isCharging, runner)); }
                    }
                    else
                    {
                        dirToTarget = (_navMeshAgent.transform.position - _navMeshAgent.transform.position).normalized;
                        Vector3 attackPosition = _navMeshAgent.pathEndPosition - dirToTarget * (_maxDistanceToAttack);
                        _navMeshAgent.SetDestination(attackPosition);
                       
                    }

                }
                else if ((Vector3.Distance(runner.transform.position, _mTarget.position) >= _minDistanceToAttack))
                {
                                                                                                                                          //Stop the agent when the player already outside the perimeter
                    _playerRunAway = true;
                }

            } 
        }
    }

    public override string CheckConditions(MonoBehaviour runner)                                                                        // Decisions has been made here -----------Called every frame after the First Frame ------------------
    {
        if (_mTarget == null || isDead) { return ""; }
        
        if (_playerRunAway) { _playerRunAway = false; return OnEnemyLostState; }                                                        // SetBack to the Previous allocated Location

        Collider[] overlapResults = new Collider[60];                                                                                   // we use physics and not a Trigger Components here for better GCA
        int numFound = Physics.OverlapSphereNonAlloc(runner.transform.position, _maxDistanceToAttack, overlapResults);

        for (int i = 0; i < numFound; i++)
        {
            if (overlapResults[i] != null)
            {
                if (overlapResults[i].transform == _mTarget)
                {
                    if ((Vector3.Distance(runner.transform.position, _mTarget.position) >= _maxDistanceToAttack))
                    {
                     
                        return OnEnemyAttackDistance;
                    }
                    else if (Vector3.Distance(runner.transform.position, _mTarget.position) <= _minDistanceToAttack)           // Switch to <Attack State>
                    {

                        return OnEnemyLostState;
                    }
                }
            }
        }

        overlapResults = new Collider[0];                                                                            // Helping the GCA   

        return "";                                                                                                   // Return empty String so that the StateMachine bypass validation check, and retained the current states
    }

    public override void OnExit(MonoBehaviour runner)
    {
        // TODO destroy Effects / Animation
    }

   
    IEnumerator coroutineTrigger(bool isCharging, MonoBehaviour runner)           
    {
            yield return runner.StartCoroutine(SpecialMovement1.Rotate_Coroutine(runner, _minDistanceToAttack, _maxDistanceToAttack, isCharging));

      yield return null;
    }



}



































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh