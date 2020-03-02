using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//on collision start a countdown timer, check group
//if check group comes back successful (all timers in group are above 0
//trigger group, else do nothing

namespace ROFO
{ 
public class TimerCollisionTrigger : MonoBehaviour, ITrigger
{
    public string tagCollision;
    public string GroupToCheck;
    public string GroupToTrigger;
    public float timeActive = 1f;
    public bool isTimer = false;
    private float count = 0.0f;


    public void Trigger()
    {
        //call trigger on all groupToTrigger
        PuzzlePiece[] p = PuzzleManager.GetGroup(GroupToTrigger);

        for (int i = 0; i < p.Length; i++)
        {
            p[i].Trigger();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == tagCollision)
        {
            c = null;
            c = StartCoroutine(Timer());

            //check timers
            bool check = TimerCheck();

            //if all puzzle pieces in group have their timers activated
            //than puzzle correct so trigger it
            if(check)
            {
                Trigger();
            }            
        }
    }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals(tagCollision))
            {
                Debug.Log("Blast working");

                c = null;
                c = StartCoroutine(Timer());

                //check timers
                bool check = TimerCheck();

                //if all puzzle pieces in group have their timers activated
                //than puzzle correct so trigger it
                if (check)
                {
                    Trigger();
                }
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            
            if (other.gameObject.tag.Equals(tagCollision))
            {
                Debug.Log("Blast working");

                c = null;
                c = StartCoroutine(Timer());

                //check timers
                bool check = TimerCheck();

                //if all puzzle pieces in group have their timers activated
                //than puzzle correct so trigger it
                if (check)
                {
                    Trigger();
                }
            }
        }

        private bool TimerCheck()
    {
        //get group
        PuzzlePiece[] p = PuzzleManager.GetGroup(GroupToCheck);

        for (int i = 0; i < p.Length; i++)
        {
            //check if has this script, if not consider true
            if (p[i].GetComponent<TimerCollisionTrigger>())
            {
                //if has check if timer is on
                if (p[i].GetComponent<TimerCollisionTrigger>().isTimer == false)
                {
                    Debug.Log("Timer False, some objects timers are off.");
                    return false;                    
                }
            }
        }

        //got through all puzzle pieces, either doesn't have this script (therefore true),
        //or has script and count is greater than 0 meaning has been activated
        Debug.Log("Timer True");
        return true;
    }

    Coroutine c = null;
    IEnumerator Timer()
    {
        isTimer = true;
        count = timeActive;

        while(count > 0.0f)
        {
            count -= Time.deltaTime;

            yield return null;
        }

        isTimer = false;
    }

        private void OnDrawGizmos()
        {
            if (isTimer)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one);
            }
        }    
    }
}
