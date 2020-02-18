using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionContainer : MonoBehaviour
{
    public int rank = 0;
    public State returnState;

    private IState state;
    public IState GetState()
    {
        return state;
    }

    public int numberOfConditions = 0;
    public ICondition[] conditions;


    private void Start()
    {
        target = GameObject.Find("Player").transform;

        state = StateEnum.CreateState(returnState, this);
        
    }

    //a container for holding potential gameobjects needed for states
    public GameObject launcher;
    public GameObject projectile;
    public float launchTime;
    public GameObject gameObjectHolder;
    public Transform target;
    
}



