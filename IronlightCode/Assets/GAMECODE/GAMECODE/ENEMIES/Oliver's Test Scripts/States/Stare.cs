using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stare : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private float _stareRange = 11;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
    }

    public void Enter()
    {
        _enabled = true;
        _agent.isStopped = true;
        _anim.SetFloat("Speed", 0);
    }

    public void Exit()
    {
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //Can shoot if cooldown is up and player is in range
        if (pDistance <= _stareRange)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        return true;
    }

    public void Tick()
    {
        Vector3 lookAtTarget = new Vector3(_target.position.x, 0, _target.position.z);
        transform.LookAt(lookAtTarget);
    }
}
