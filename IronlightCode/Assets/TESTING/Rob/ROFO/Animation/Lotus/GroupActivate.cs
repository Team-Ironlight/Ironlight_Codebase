using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//get all child objects and activate them if they have certain script
public class GroupActivate : MonoBehaviour
{
    public float delay = 0.2f;
    private AnimationProximity[] children;
    private Coroutine c = null;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    //get all objects that change in this...
    //maybe integrate it into puzzle scripts...
    private void Setup()
    {
        List<AnimationProximity> c = new List<AnimationProximity>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AnimationProximity>())
            {
                //Debug.Log("Child");
                c.Add(transform.GetChild(i).GetComponent<AnimationProximity>());
            }
        }

        children = c.ToArray();
    }


    //in and out
    private void OnTriggerEnter(Collider other)
    {
        c = null;        
        c = StartCoroutine(Work());
    }

    private void OnTriggerExit(Collider other)
    {
        c = null;        
        c = StartCoroutine(Work());
    }

    //coroutine to add delay to opening
    IEnumerator Work()
    {
        int i = 0;
        float count = 0f;
        while(i < children.Length)
        {
            while(count < delay)
            {
                count += Time.deltaTime;
                yield return null;
            }

            children[i].Activate();
            i++;
            count = 0f;

            yield return null;
        }
    }
}
