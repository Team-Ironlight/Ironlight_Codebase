using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBed : MonoBehaviour
{
    
    
    public GameObject respchkpnt;

   private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.CompareTag("Player"))
       {
            other.transform.position = respchkpnt.GetComponent<RespawnCheckPoint>().lastCheckPoint.transform.position;
	   }
   }
}
