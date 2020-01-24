using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;

    private int _subState = 0;

    [SerializeField] private float _pauseTimeMin = 1;
    [SerializeField] private float _pauseTimeMax = 1;
    private float _stateChangeTime = 0;

    [Space]
    [SerializeField] private float pointReachedDistance = 1;
    [SerializeField] private GameObject patrolPath;
    private Transform[] _patrolPoints;
    private int _currentPatrolIndex;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;

        _patrolPoints = patrolPath.GetComponentsInChildren<Transform>();
    }

    public void Enter()
    {
        _enabled = true;
        _stateChangeTime = Time.time + 1;
        _subState = 0;
        _agent.isStopped = true;
        //Debug.Log("Patrol: Enter");

        // TODO Pick closest point to be the _currentPatrolDest
    }

    public void Exit()
    {
        //Debug.Log("Patrol: Exit");
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //Debug.Log("Patrol: CanEnter");
        // TODO False if no patrol points

        return true;
    }

    public bool CanExit(float pDistance)
    {
        return true;
    }

    // Update is called once per frame
    public void Tick()
    {
        switch (_subState)
        {
            //Idle at point
            case 0:
                //Switch to moving to next point after wait time up
                if (Time.time >= _stateChangeTime)
                {
                    //Debug.Log("Patrol: Switch to moving");
                    _subState = 1;
                    _agent.isStopped = false;
                    // TODO Can change direction along path
                    _currentPatrolIndex++;

                    if (_currentPatrolIndex == _patrolPoints.Length) _currentPatrolIndex = 0;
                    if (_currentPatrolIndex == -1) _currentPatrolIndex = _patrolPoints.Length - 1;

                    _agent.SetDestination(_patrolPoints[_currentPatrolIndex].position);
                }
                break;

            //Moving to next point
            case 1:
                //Switch to idle when reached destination
                if (Vector3.Distance(transform.position, _patrolPoints[_currentPatrolIndex].position) <= pointReachedDistance)
                {
                    //Debug.Log("Patrol: Switch to idle");
                    _subState = 0;
                    _stateChangeTime = Time.time + Random.Range(_pauseTimeMin, _pauseTimeMax);
                    _agent.isStopped = true;
                }
                break;
        }

        //Set Anim Speed
        _anim.SetFloat("Speed", _agent.velocity.magnitude/ _agent.speed);
    }
}
