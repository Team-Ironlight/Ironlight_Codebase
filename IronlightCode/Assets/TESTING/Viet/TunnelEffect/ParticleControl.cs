using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    public ParticleSystem particleSys;

    //private bool inOrOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            particleSys.Play();
            //particleTime();
            //Debug.Log("In");
            //inOrOut = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            particleSys.Stop();
            //particleTime();
            //Debug.Log("Out");
            //inOrOut = false;
        }
    }

    //void particleTime()
    //{
    //    if (inOrOut == true)
    //    {
    //        particleSys.Play();
    //    }
    //    if (inOrOut == false)
    //    {
    //        particleSys.Stop();
    //    }
    //}
}
