using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dead : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

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
        _anim.SetTrigger("Dead");
        _anim.SetBool("IsDead", true);
        _agent.isStopped = true;
        // TODO Remove collision during animation and after animation replace model with ragdoll version
    }

    public void Exit()
    {
        _anim.SetTrigger("Respawn");
        _anim.SetBool("IsDead", false);
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        return false;
    }

    public bool CanExit(float pDistance)
    {
        return false;
    }

    public void Tick()
    {

    }
}
