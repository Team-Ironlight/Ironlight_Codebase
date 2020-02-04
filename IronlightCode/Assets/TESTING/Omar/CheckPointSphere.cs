using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSphere : MonoBehaviour
{
   public GameObject systemGo;
   private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.CompareTag("Player"))
       {
            systemGo.GetComponent<RespawnCheckPoint>().lastCheckPoint = this.transform;
	   }
   }
}
