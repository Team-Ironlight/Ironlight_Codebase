using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCT_Crystal : MonoBehaviour
{
    public Transform CrystalHolder;
    public Transform Caster;
    public GameObject Beam;

    public bool beamActive = false;
    public bool linked = false;

    public bool interacted = false;


    public float rotationToAdd;

    public float rayLength = 3;

    public Vector3 current;

    public Quaternion targetRotation;
    public float smooth = 1;

    Coroutine currentCO;

    private void Start()
    {
        CrystalHolder = gameObject.transform;

        targetRotation = transform.rotation;
        //target = current + rotationToAdd;
    }

    // Update is called once per frame
    void Update()
    {
        current = CrystalHolder.localEulerAngles;


        if (beamActive)
        {
            Beam.SetActive(true);
        }
        else
        {
            Beam.SetActive(false);
        }

        if (beamActive)
        {
            checkForCrystal();
        }

        if (interacted)
        {
            RotateCrystal();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);
        
    }



    void RotateCrystal()
    {
        targetRotation *= Quaternion.AngleAxis(rotationToAdd, Vector3.up);
        interacted = false;
    }




    void checkForCrystal()
    {
        RaycastHit hit;


        if(Physics.Linecast(Caster.position, (Caster.position + (Caster.forward * rayLength)), out hit))
        {
            if (hit.collider.gameObject.GetComponent<BCT_Crystal>())
            {
                
                linked = true;

                hit.collider.gameObject.GetComponent<BCT_Crystal>().beamActive = true;
                hit.collider.gameObject.GetComponent<BCT_Crystal>().linked = true;
            }

            if (hit.collider.gameObject.GetComponent<BCT_Goal>())
            {
                Debug.Log("Hit Sphere");
                hit.collider.gameObject.GetComponent<BCT_Goal>().Activated = true;

            }
        }
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawLine(Caster.position, Caster.forward * 5, Color.red);

        Gizmos.DrawLine(Caster.position, (Caster.position + (Caster.forward * rayLength)));
    }
}

