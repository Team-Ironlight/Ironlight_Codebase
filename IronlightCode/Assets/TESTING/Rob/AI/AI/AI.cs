using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    //public State defaultState = State.None;
    private IState defaultState;

    private ConditionContainer[] container;
    public ConditionContainer[] GetContainer()
    {
        return container;
    }

    private bool isReady = false;
    public bool GetisReady()
    {
        return isReady;
    }


    private void Start()
    {
        defaultState = new None();
        container = CreateContainer(gameObject);
    }

    
    //state machine will call this to get a next state
    public IState CheckContainer(ConditionContainer[] cc)
    {
        if(cc == null)
        {
            return defaultState;
        }

        //Debug.Log("Container Length: " + cc.Length);
        for (int i = 0; i < cc.Length; i++)
        {
            bool all = true;
            for (int j = 0; j < cc[i].conditions.Length; j++)
            {              
                //if a single condition in the conditoin containe is false, break
                if (cc[i].conditions[j].Check() == false)
                {
                    all = false;
                    break;
                }
            }

            //if all conditions pass than return state
            if(all)
            {
                //Debug.Log("Returning: " + cc[i].name);
                return cc[i].GetState();
            }
        }

        //if no conditions check out return default state
        Debug.Log("Nothing True, returning none");
        return defaultState;
    }


    //get all instances of conditionContainer on gameobject
    private ConditionContainer[] CreateContainer(GameObject g)
    {
        ConditionContainer[] temp = g.GetComponents<ConditionContainer>();
        temp = SortContainer(temp);

        return temp;
    }


    //sort the container passed in based on rank
    private ConditionContainer[] SortContainer(ConditionContainer[] cc)
    {
        //new sort
        ConditionContainer[] temp = new ConditionContainer[cc.Length];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[cc[i].rank] = cc[i];
        }

        //debug the order with rank
        for (int i = 0; i < temp.Length; i++)
        {
            Debug.Log("<color=blue>Return: </color>" + temp[i].returnState + " Rank: " + temp[i].rank);
        }

        return temp;
    }


    //private void OnDrawGizmos()
    //{
    //    if (container != null)
    //    {
    //        for (int i = 0; i < container.Length; i++)
    //        {
    //            ICondition[] c = container[i].conditions;

    //            for (int j = 0; j < c.Length; j++)
    //            {
    //                if (c[j].GetComponent<RangeCondition>())
    //                {
    //                    Gizmos.color = Color.cyan;
    //                    float range = c[j].GetComponent<RangeCondition>().range;
    //                    Gizmos.DrawWireSphere(transform.position, range);
    //                }
    //                else if(c[j].GetComponent<InViewCondition>())
    //                {
    //                    Gizmos.color = Color.white;
    //                    float angle = c[j].GetComponent<InViewCondition>().viewAngle;
    //                    //Quaternion original = transform.rotation;
    //                    Quaternion right = transform.rotation * Quaternion.Euler(new Vector3(0f, angle / 2f, 0f));
    //                    Quaternion left = transform.rotation * Quaternion.Euler(new Vector3(0f, -angle / 2f, 0f));

    //                    Transform temp = transform;
    //                    temp.rotation = right;
    //                    Vector3 rightForward = temp.forward;
    //                    temp.rotation = left;
    //                    Vector3 leftForward = temp.forward;

    //                    Gizmos.DrawLine(transform.position, transform.position + rightForward * 5f);
    //                    Gizmos.DrawLine(transform.position, transform.position + leftForward * 5f);                        
    //                }
    //            }
    //        }
    //    }
    //}    
}
