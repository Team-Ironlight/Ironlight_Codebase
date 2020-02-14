// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James    /  Alteration dates below
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using IronLight;

[System.Serializable]
[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New RandomState")]
public class RandomState : StateMachine.BaseState
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif
    public Vector3 Center = Vector2.zero;
    private Vector3 newPos = Vector2.zero;
    public float JumpTimeMax = 5.0f;
    public float JumpTimeMin = 3.0f;
    public float JumpMinDist = 0.1f;
    public float JumpRadius = 10.0f;
    private float jumpTime = 0.0f;
    private float curTime = 0.0f;
    public bool useCenter = true;

    public float dampTime = 1.25f;
    private Vector3 velocity = Vector3.zero;

    public float EnemyMinDist = 2.0f;
    public string OnEmemyLockState = "ChaseState";

    public float minStateTime = 2.0f;
    public float maxStateTime = 5.0f;
    private float StateTime = 0.0f;

    public StateMachine behaviour { get; protected set; }

    public override void   OnEnter(MonoBehaviour runner)
	{
       // StateTime = Random.Range(minStateTime, maxStateTime);
    }
	public override void   Tick(MonoBehaviour runner)
	{
        curTime += Time.deltaTime;
        StateTime -= Time.deltaTime;
        if (curTime >= jumpTime || Vector3.Distance(behaviour.transform.position, behaviour.transform.position) <= JumpMinDist)
        {
            jumpTime = Random.Range(JumpTimeMin, JumpTimeMax);
            curTime = 0.0f;
            newPos = (useCenter ? Center : newPos) + (Random.insideUnitSphere * JumpRadius);
            newPos.z = behaviour.transform.position.z;
            behaviour.transform.position = Vector3.SmoothDamp(behaviour.transform.position, newPos, ref velocity, dampTime);
        }
    }
	public override string CheckConditions(MonoBehaviour runner)
	{
        if (StateTime <= 0.0f)
        {
            return OnEmemyLockState;
        }
        return "";

    }
	public override void   OnExit(MonoBehaviour runner)
	{
		//To Do Destroy effects  / Animation
	}
}
