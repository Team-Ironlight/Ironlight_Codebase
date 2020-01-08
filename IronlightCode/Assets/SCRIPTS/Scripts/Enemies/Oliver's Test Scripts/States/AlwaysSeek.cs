using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlwaysSeek : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    protected Transform _target;

    [SerializeField] protected bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
    }

    public void Enter()
    {
        _enabled = true;
        _agent.isStopped = false;
    }

    public void Exit()
    {
        _enabled = false;
    }

    public virtual bool CanEnter(float pDistance)
    {
        return true;
    }

    public bool CanExit(float pDistance)
    {
        return true;
    }

    public void Tick()
    {
        //Vector3 direction = (target.position - transform.position).normalized;
        //direction.y = 0;

        //Quaternion targetRot = Quaternion.LookRotation(direction);
        //targetRot = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotation);
        //transform.rotation = targetRot;

        //Move to player
        _agent.SetDestination(_target.position);

        //Set Anim Speed
        _anim.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);

        if (Vector3.Distance(transform.position, _target.position) <= 1)
        {
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
        }
    }
}