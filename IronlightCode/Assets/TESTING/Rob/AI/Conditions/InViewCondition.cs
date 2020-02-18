using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//check to see if target is in view range of this
public class InViewCondition : ICondition
{    
    //constructor
    public InViewCondition(float v)
    {
        viewAngle = v;
        target = GameObject.Find("Player").transform;
        Debug.Log("Target" + target);
    }

    //variables
    private Transform target;
    [Range(0f,360f)] public float viewAngle = 30f;

    

    public override bool Check()
    {
        Vector3 vectorToTarget = target.position - transform.position;

        if (Vector3.Dot(transform.forward, vectorToTarget) > 1 - viewAngle / 360f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        if(target == null)
        {
            target = GameObject.Find("Player").transform;
        }
    }
}
