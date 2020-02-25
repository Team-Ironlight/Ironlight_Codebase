using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{ 
public class RangeCondition : ICondition
{
    //constructor
    public RangeCondition(float r)
    {
        range = r;
        target = GameObject.Find("Player").transform;
    }

    //variables
    private Transform target;
    public float range = 0;


    public override bool Check()
    {
        if ((target.position - transform.position).magnitude < range)
        {
            //return the state for that range
            return true;
        }

        return false;        
    }

        private void Start()
        {
            if (target == null)
            {
                target = GameObject.Find("Player").transform;
            }
        }
    }
}