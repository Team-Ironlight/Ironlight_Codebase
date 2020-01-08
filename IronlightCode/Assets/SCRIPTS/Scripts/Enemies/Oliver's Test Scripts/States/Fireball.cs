using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fireball : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private float _cooldown = 1.0f;
    private float _nextEnterTime = 0.0f;

    [SerializeField] private Rigidbody _fireballPrefab;
    [SerializeField] private Transform _fireballSpawnpoint;

    [Space]
    [SerializeField] private float _fireballRangeMax = 10;
    [SerializeField] private float _fireballRangeMin = 3;

    [SerializeField] private float _fireballSpeed = 1000;

    [Space]
    [SerializeField] [Range(0,1)] private float _oddsOfSkipOver = 0;
    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
    }

    public void Enter()
    {
        //Debug.Log("Fireball: Enter");
        _enabled = true;
        _agent.isStopped = true;
        transform.LookAt(_target.position);
        _anim.SetBool("Shooting", true);
    }

    public void Exit()
    {
        //Debug.Log("Fireball: Exit");
        _enabled = false;
        _anim.SetBool("Shooting", false);
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //Can shoot if cooldown is up and player is in range
        if (Time.time >= _nextEnterTime && pDistance < _fireballRangeMax && pDistance > _fireballRangeMin) 
            return CheckForSkipOver();

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

    // Additional Functions /////////
    private bool CheckForSkipOver()
    {
        // Randomization that prevents Enemy acting the same way everytime
        if (Random.value > _oddsOfSkipOver)
        {
            // Return don't skip over
            return true;
        }
        else
        {
            // Return skip over + put on cooldown
            _nextEnterTime = Time.time + (_cooldown / 2);
            return false;
        }
    }

    //Animation Events //////////////
    public void AEShootFireball()
    {
        //Debug.Log("Fireball: AEShootFireball");
        Rigidbody fireballInstance;
        Vector3 direction;

        direction = (_target.position - _fireballSpawnpoint.position).normalized;
        fireballInstance = Instantiate(_fireballPrefab) as Rigidbody;
        fireballInstance.transform.position = _fireballSpawnpoint.position;
        fireballInstance.AddForce(direction * _fireballSpeed);
    }

    public void AEDoneShooting()
    {
        //Debug.Log("Fireball: AEDoneShooting");
        _enabled = false;
        _nextEnterTime = Time.time + _cooldown;
    }
}
