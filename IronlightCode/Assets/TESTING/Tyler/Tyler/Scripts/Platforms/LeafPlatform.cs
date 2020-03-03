using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafPlatform : MonoBehaviour
{
   

    [SerializeField] private float speed = 15;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.tag == ("Player"))
        {
            transform.parent.Rotate(Vector3.left * speed * Time.deltaTime);
        }
    }

    

    




}
