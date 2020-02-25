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
using UnityEngine.AI;
using IronLight;
using TMPro;

[System.Serializable]
[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/AI States/New ChaseState")]
public class ChaseStateNew : MonoBehaviour
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif

    [Header("Target")]
    public Transform _mTarget;
    [Header("Decision Making")]
    public string OnEnemyLostState = "FleeState";                                               //To Do:  Convert this to enum
    private string OnEnemyChaseDistance = "ChaseState";                                         //To Do:  Convert this to enum
    private bool isAware = false;
    private bool _playerRunAway = false;
    // private bool isOnPerimeter;
    public TMP_Text tester;
    [Header("-------------")]
    [Header("Movement")]
    public float walk_Speed = 2f;
    public float run_Speed = 4f;

    [Header("Components Entity")]
    // public Transform ComponentContainer;
    public NavMeshAgent _navMeshAgent;                                                         //reference to the navmesh agent.
    public Animator _aniMator;

    public AI_AbilityManager _updateMinMax;                                                    //Tell the Random Attacks about the MinMax value for Perimeter check
                                                                 // Local use

    public float _maxDistanceToChase;
    public float _minDistanceToChase;

    public  void  Start()                                                             // This is called before the first frame
    {
        tester = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();

        tester.text = "Start";

        _mTarget = GameObject.FindWithTag("Player").transform;
        if (!_mTarget)
            Application.Quit();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _aniMator = GetComponent<Animator>();
        _updateMinMax = GetComponent<AI_AbilityManager>();

        _navMeshAgent.enabled = true;

         _maxDistanceToChase = GetComponent<StateMachine>().Get_MaxDistanceChase;
        _minDistanceToChase = GetComponent<StateMachine>().Get_MinDistanceChase;

        _updateMinMax.Set_MaxDistance = _maxDistanceToChase;
        _updateMinMax.Set_MinDistance = _minDistanceToChase;
    }
	public  void  Update()                                                                 //Called every frame after the first frame, Initiate by the StateMachine
    {
         CheckConditions();
        
        


        if (_mTarget != null)
        {
            

            if (_navMeshAgent.enabled ==true)
            {
                

                if (isAware)
                {
                   
                    Vector3 dirToTarget = (_mTarget.position - transform.position).normalized;

                    // Turn the enemy facing to the Player
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                                        Quaternion.LookRotation(dirToTarget),
                                        1.0f * Time.deltaTime);

                    Vector3 destination = _mTarget.position + dirToTarget;

                    // Validate if the distance between the player and the enemy
                    // if the distance between enemy and player is less than attack distance
                    if ((Vector3.Distance(transform.position, _mTarget.position) <= _maxDistanceToChase))
                    {
                        _navMeshAgent.isStopped = false;                        //Tell the Agent to move
                        _navMeshAgent.speed = run_Speed;
                        _navMeshAgent.SetDestination(destination);
                    }
                    else if ((Vector3.Distance(transform.position, _mTarget.position) >= _minDistanceToChase))
                    {
                        _playerRunAway = true;
                    }

                }
            }
        }
    }
	public  string CheckConditions()                                                            //Decision Making - Called every frame after the First Frame 
    {

     

        
        
        if (_mTarget == null) { tester.text = "null"; return "";  }

        /*if (_playerRunAway)
        {
            isAware = false;
            _playerRunAway = false;
            return OnEnemyChaseDistance;
        }*/

        Collider[] overlapResults = new Collider[500];
        int numFound = Physics.OverlapSphereNonAlloc(transform.position, _maxDistanceToChase, overlapResults);
        
        for (int i = 0; i < numFound; i++)
        {
            

            if (overlapResults[i] != null)
            {
               

                if (overlapResults[i].transform == _mTarget.parent)
                {
                    tester.text = "_mTarget Found";

                    if ((Vector3.Distance(transform.position, _mTarget.position) >= _maxDistanceToChase))              //Chase State
                    {
                        OnAware();

                        tester.text = "Chase State";

                        

                        return "";
                    }
                    else if (Vector3.Distance(transform.position, _mTarget.position) <= _minDistanceToChase)           // Switch to <Attack State>
                    {
                        
                        tester.text = "Attack State";

                        return OnEnemyLostState;
                    }
                  //  Debug.DrawLine(transform.position, overlapResults[i].transform.position, Color.yellow);

                }

            }
         
        }
        overlapResults = new Collider[0];

        return "";                                                                                                              // Return empty String so that the StateMachine bypass validation check, and retained the current states, This saves memory calls
    }
	public  void   OnExit()
	{
		// TODO destroy Effects / Animation
	}
    public void OnAware()
    {
        isAware = true;
    }

}






































































































































































































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh