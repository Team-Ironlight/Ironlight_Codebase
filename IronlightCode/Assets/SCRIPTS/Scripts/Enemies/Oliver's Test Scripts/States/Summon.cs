using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Summon : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private float _cooldown = 1.0f;
    private float _nextEnterTime = 0.0f;

    [Space]
    [SerializeField] private Transform[] summonSpots = new Transform[3];
    [SerializeField] private GameObject gruntPrefab;

    [SerializeField] private float _summonRangeMax;
    [SerializeField] private float _summonRangeMin;

    private int _gruntCount = 0;

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
        _anim.SetBool("Summoning", true);
        _agent.isStopped = true;
    }

    public void Exit()
    {
        _enabled = false;
        _anim.SetBool("Summoning", false);
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //Summon Grunt Limit
        if (_gruntCount >= 2)
            return false;

        //Can shoot if cooldown is up and player is in range
        if (Time.time >= _nextEnterTime && pDistance < _summonRangeMax && pDistance > _summonRangeMin)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        return (_enabled == false); 
    }

    public void Tick()
    {

    }

    // Additional Functions //////////
    public void GruntDied()
    {
        _gruntCount--;
    }

    // Animation Events //////////////
    public void AESummonGrunts()
    {
        //Debug.Log("Fireball: AESummonGrunts");
        foreach (Transform spot in summonSpots)
        {
            GameObject summonedGrunt = Instantiate(gruntPrefab, spot.position, spot.rotation);
            summonedGrunt.GetComponent<Decision>().target = _target;
            summonedGrunt.GetComponent<EnemyAttributes>().SetSummonedGrunt(this);
            ++_gruntCount;
        }
    }

    public void AEDoneSummoning()
    {
        //Debug.Log("Fireball: AEDoneShooting");
        _enabled = false;
        _nextEnterTime = Time.time + _cooldown;
    }
}
