using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrystal : MonoBehaviour, IHit
{
    public LayerMask lm;
    public bool lineActive;
    public bool playerCanActivate;
    public float smoothRot = 1;
    Vector3 target;
    //private GameObject Crystal;
    private Quaternion targetRot;
    IHit lastHitThing;
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
            HitWithLight(0);
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

    public void HitWithLight(float pAmount)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, transform.position + (transform.forward * 10), out hit, lm))
        {
            IHit hitThing = hit.transform.GetComponent<IHit>();

            if (hitThing != null)
            {
                hitThing.HitWithLight(0);
                //if (hitThing != lastHitThing)
                //{
                //    hitThing.EnterHitWithLight(0);
                //    lastHitThing.ExitHitWithLight();

                //}
            }
            print("Draw a line");
            //hitThing = lastHitThing;

        }

        Debug.DrawLine(gameObject.transform.position, transform.position + (transform.forward * 10),Color.green);
    }
    public void EnterHitWithLight(float pAmount)
    {
     
    }
    public void ExitHitWithLight()
    { }

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
}
