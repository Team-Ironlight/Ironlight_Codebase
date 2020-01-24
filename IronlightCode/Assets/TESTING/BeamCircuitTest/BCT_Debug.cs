using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCT_Debug : MonoBehaviour
{
    public GameObject player;
    public Transform camTrans;

    bool inRange = false;

    public BCT_Crystal currentCrystal = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        checkForCrystal();

        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                currentCrystal.interacted = true;
            }
        }
    }


    void checkForCrystal()
    {
        RaycastHit hit;


        if (Physics.Linecast(player.transform.position, (player.transform.position + (player.transform.forward * 1.5f)), out hit))
        {
            if (hit.collider.gameObject.GetComponent<BCT_Crystal>())
            {
                Debug.Log("Hit Crystal");

                currentCrystal = hit.collider.gameObject.GetComponent<BCT_Crystal>();

                currentCrystal.beamActive = true;

                inRange = true;
            }
        }
        else
        {
            inRange = false;
            if (currentCrystal != null && !currentCrystal.linked)
            {
                currentCrystal.beamActive = false;
                currentCrystal = null;
                
            }
        } 


    }


    void MovePlayer()
    {
        Vector3 rotation = camTrans.eulerAngles;

        rotation.x = 0;

        Quaternion forward = Quaternion.Euler(rotation);


        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0)
        {
            player.transform.localRotation = forward;

            if(vertical > 0)
            {
                player.transform.position += (player.transform.forward * 0.05f);
            }
            else
            {
                player.transform.position -= (player.transform.forward * 0.04f);
            }
        }

        if(horizontal != 0)
        {
            player.transform.localRotation = forward;

            if(horizontal > 0)
            {
                player.transform.position += (player.transform.right * 0.03f);
            }
            else
            {
                player.transform.position -= (player.transform.right * 0.03f);
            }
        }        
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(player.transform.position, (player.transform.position + player.transform.forward));
    }
}
