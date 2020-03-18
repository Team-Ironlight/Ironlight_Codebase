using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathBed : MonoBehaviour
{
    Animator anim;
    public SectionManager sm;
    private GameObject player;
    public GameObject respchkpnt;
    Rigidbody rb;
    void Start()
    {
        sm = gameObject.GetComponent<SectionManager>();
    }

    private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.CompareTag("Player"))
       {
            player = other.gameObject;
            anim = other.GetComponentInChildren <Animator>();
            rb = other.gameObject.GetComponent<Rigidbody>();

            //tp = other.transform.position;
            StartCoroutine(DeathCor());
            rb.velocity = new Vector3(0, 0, 0);
            Debug.Log("Exit Trigger On Death!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            StopCoroutine(DeathCor());

        }
   }
    
    IEnumerator DeathCor()
    {
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(2);

        //rb.velocity.
        //sm.resetPlayer = true;
        anim.SetBool("Death", false);
        player.transform.position = respchkpnt.GetComponent<RespawnCheckPoint>().lastCheckPoint.transform.position;
        
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.CompareTag("Player"))
    //    {
    //        StopCoroutine(DeathCor());
    //    }
    //}
}
