using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathBed : MonoBehaviour
{
    public SectionManager sm;
    
    public GameObject respchkpnt;


    void Start()
    {
        sm = gameObject.GetComponent<SectionManager>();
    }

    private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.CompareTag("Player"))
       {
            other.transform.position = respchkpnt.GetComponent<RespawnCheckPoint>().lastCheckPoint.transform.position;
            sm.resetPlayer = true;
	   }
   }

}
