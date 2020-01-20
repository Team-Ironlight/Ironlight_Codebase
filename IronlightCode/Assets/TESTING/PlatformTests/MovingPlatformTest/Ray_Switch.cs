using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray_Switch : MonoBehaviour {

    public Transform point;
    public LayerMask whichLayer;

    public float range = 100;
  
  
    // Update is called once per frame
    void Update()
    {

        rayPoint();

    }
  
    public void rayPoint()
    {
        RaycastHit hit;
        Vector3 position = point.position;
      
        //for tracing the RayCast
        Debug.DrawRay(position, transform.TransformDirection(Vector3.forward * range), Color.green);
        Debug.DrawRay(position, transform.TransformDirection(Vector3.down * range), Color.green);
        if (Physics.Raycast(position, transform.TransformDirection(Vector3.forward * range), out hit, range, whichLayer) || Physics.Raycast(position, transform.TransformDirection(Vector3.down * range), out hit, range, whichLayer))
        {
            if (hit.transform.tag == "MovingPlatform")
            {

                //Security Check we need to verify if the component is exist!

               if (hit.collider.gameObject.GetComponent<Interactive_Obj>() != null)
                {
                 
                    Interactive_Obj rock = hit.transform.gameObject.GetComponentInChildren<Interactive_Obj>();
                    rock.activate = true;
                }
            }

         
        }
        
        
      
    
    }
}







