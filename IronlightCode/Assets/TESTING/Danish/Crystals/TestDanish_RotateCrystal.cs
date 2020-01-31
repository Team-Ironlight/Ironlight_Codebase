using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_RotateCrystal : MonoBehaviour, IHit
{
    public LayerMask lm;
    public bool lineActive;
    public bool playerCanActivate;
    public float smoothRot = 1;
    public float RayDistance = 5;
    public LineRenderer _line;
    public Transform startPoint;
    public Transform CrystalFace;

    public Collider collider;

    Vector3 target;


    //private GameObject Crystal;
    private Quaternion targetRot;
    IHit lastHitThing;


    void Start()
    {
        //Crystal = GetComponentInParent<GameObject>();
        targetRot = CrystalFace.rotation;
        _line.gameObject.SetActive(true);

        _line.SetPosition(0, startPoint.position);
        _line.SetPosition(1, startPoint.position);
    }

    void Update()
    {
        
        _line.SetPosition(0, startPoint.position);

        if (playerCanActivate)
        {
            Rotate();
        }

        if (lineActive)
        {
            HitWithLight(0);
        }
        else
        {
            _line.SetPosition(1, startPoint.position);
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
        CrystalFace.rotation = Quaternion.Lerp(CrystalFace.rotation, targetRot, 10 * smoothRot * Time.deltaTime);
    }

    public void HitWithLight(float pAmount)
    {
        Vector3 start = startPoint.position;
        Vector3 end = startPoint.position + (CrystalFace.forward * RayDistance);

        RaycastHit hit;
        if (Physics.Linecast(start, end, out hit, lm))
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
        _line.SetPosition(1, end);

        Debug.DrawLine(start, end, Color.green);
    }
    public void EnterHitWithLight(float pAmount)
    {

    }
    public void ExitHitWithLight()
    { }

    

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerCanActivate = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerCanActivate = false;
    //    }
    //}
}
