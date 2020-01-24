using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stunned : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
    }

    public void Enter()
    {
        _enabled = true;
        _anim.SetTrigger("Hurt");
        _agent.isStopped = true;
    }

    public void Exit()
    {
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        return false;
    }

    public bool CanExit(float pDistance)
    {
        return (_enabled == false);
    }

    public void Tick()
    {

    }

    //Animation Events //////////////
    public void AEDoneStunned()
    {
        //Debug.Log("Stunned: AEDoneStunned");
        _enabled = false;
    }
}
