using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrystal : MonoBehaviour, ICrystal
{
    public LayerMask lm;
    public bool lineActive;
    public bool playerCanActivate;
    public float smoothRot = 1;
    public float RayDistance = 5;
    Vector3 target;
    //private GameObject Crystal;
    private Quaternion targetRot;
    public ICrystal lastHitThing;
    // Start is called before the first frame update
    void Start()
    {
       //Crystal = GetComponentInParent<GameObject>();
        targetRot = transform.parent.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCanActivate)
        {
            Rotate();
        }
        if (lineActive)
        {
            isActivated();
        }
    }
    //private void OnDrawGizmos()
    //{
    //    if (lineActive)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(gameObject.transform.position, transform.position + (transform.forward * 5));
    //    }
    //}
    void Rotate()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRot *= Quaternion.AngleAxis(45, Vector3.up);
            print("Rotate");
        }
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, targetRot, 10 * smoothRot * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerCanActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCanActivate = false;
        }
    }
    public void isActivated()
    {

        RaycastHit hit;
        if (Physics.Linecast(transform.position, transform.position + (transform.forward * RayDistance), out hit, lm))
        {
            ICrystal hitThing = hit.transform.GetComponent<ICrystal>();

            if (hitThing != null)
            {
                hitThing.isActivated();
                //if (hitThing != lastHitThing)
                //{
                ////    hitThing.EnterHitWithLight(0);
                ////    lastHitThing.ExitHitWithLight();

                //}
            }
            print("Draw a line");
            //hitThing = lastHitThing;

        }

        Debug.DrawLine(gameObject.transform.position, transform.position + (transform.forward * RayDistance),Color.green);
    }
}
