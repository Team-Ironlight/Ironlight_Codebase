using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private float _cooldown = 1.0f;
    private float _nextEnterTime = 0.0f;

    [SerializeField] private float _attackRange = 1;

    [SerializeField] private GameObject _hitbox;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
        _hitbox.SetActive(false);
    }

    public void Enter()
    {
        //Debug.Log("Fireball: Enter");
        _enabled = true;
        //_agent.isStopped = true;
        transform.LookAt(_target.position);
        _hitbox = GetComponentInChildren<AttackHitbox>().gameObject;
        _anim.SetBool("Attacking", true);
    }

    public void Exit()
    {
        //Debug.Log("Fireball: Exit");
        _enabled = false;
        _anim.SetBool("Attacking", false);
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //Can shoot if cooldown is up and player is in range
        if (Time.time >= _nextEnterTime && pDistance < _attackRange)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        //Debug.Log("Fireball: CanExit - " + (_enabled == false));
        return (_enabled == false);
    }

    public void Tick()
    {
        
    }

    //Animation Events //////////////
    public void AEEnableHitbox()
    {
        _hitbox.SetActive(true);
    }

    public void AEDisableHitbox()
    {
        _hitbox.SetActive(false);
    }

    public void AEDoneAttack()
    {
        GetComponent<Decision>().ForceStateSwitch((IState)GetComponent<JumpBack>());
        _enabled = false;
        _nextEnterTime = Time.time + _cooldown;
    }
}
