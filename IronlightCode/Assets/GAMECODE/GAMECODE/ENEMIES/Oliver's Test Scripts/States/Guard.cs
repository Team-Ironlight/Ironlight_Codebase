using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;

    private Vector3 _home;
    [SerializeField] private float homeReachedDistance = 1;

    private int _subState = 0;
    private float _stateChangeTime = 0;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;

        _home = transform.position;
    }

    public void Enter()
    {
        _enabled = true;
        _subState = 0;
        _stateChangeTime = Time.time + 1;
        if (_agent != null)
            _agent.isStopped = true;
        //Debug.Log("Guard: Enter");
    }

    public void Exit()
    {
        //Debug.Log("Guard: Exit");
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //Debug.Log("Guard: CanEnter");
        //False if no patrol points

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
            //Idle a bit before moving
            case 0:
                //Switch to moving to next point after wait time up
                if (Time.time >= _stateChangeTime)
                {
                    //Debug.Log("Guard: Switch to moving");
                    _subState = 1;
                    if (_agent != null)
                        _agent.isStopped = false;
                    _agent.SetDestination(_home);
                }
                break;

            //Moving to next point
            case 1:
                //Switch to idle when reached home
                if (Vector3.Distance(transform.position, _home) <= homeReachedDistance)
                {
                    //Debug.Log("Guard: Switch to idle");
                    _subState = 2;
                    if (_agent != null)
                        _agent.isStopped = true;
                    _anim.SetFloat("Speed", 0);
                }
                break;

            //Idle at home
            case 2:

                break;
        }

        //Set Anim Speed
        if (_subState != 2)
            _anim.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);
    }
}
