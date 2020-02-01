using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_RotatingCrystal : MonoBehaviour
{
    public LayerMask lm;
    public LayerMask obstruction;
    public LayerMask shrine;

    public bool lineActive;
    public bool playerCanActivate;
    public float smoothRot = 1;
    public float RayDistance = 5;
    public float minRayDistance = 1;
    public LineRenderer _line;
    public Transform startPoint;
    public Transform CrystalFace;

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
        if (playerCanActivate)
        {
            Rotate();
        }

        if (lineActive)
        {
            CheckForObstructions();
        }

    }

    void Rotate()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRot *= Quaternion.AngleAxis(45, Vector3.up);
            print("Rotate");
        }
        CrystalFace.rotation = Quaternion.Lerp(CrystalFace.rotation, targetRot, 10 * smoothRot * Time.deltaTime);
    }

    void CheckForObstructions()
    {
        Vector3 start = startPoint.position + (CrystalFace.forward * minRayDistance);
        Vector3 end = startPoint.position + (CrystalFace.forward * RayDistance);

        RaycastHit hit;
        if (Physics.Linecast(start, end, out hit, obstruction))
        {
            Vector3 modifiedEnd = hit.point;

            _line.SetPosition(1, modifiedEnd);
        }
        else if (Physics.Linecast(start, end, out hit, lm))
        {
            TestDanish_CrystalCollisions hitThing = hit.transform.GetComponent<TestDanish_CrystalCollisions>();

            if (hitThing != null)
            {
                Debug.Log("Found a thing");
                hitThing.HitWithLight(0);

                Vector3 newPoint = hitThing.crystal.startPoint.position;

                _line.SetPosition(1, newPoint);
                Debug.DrawLine(start, newPoint, Color.green);
            }
        }else if(Physics.Linecast(start, end, out hit, shrine))
        {
            BeamPuzzleShrine hitThing = hit.transform.GetComponent<BeamPuzzleShrine>();

            if (hitThing != null)
            {
                hitThing.HitWithLight(0);

                _line.SetPosition(1, hitThing.crystalPos.position); ;
                Debug.DrawLine(start, hitThing.crystalPos.position, Color.yellow);
            }
        }
        else
        {
            _line.SetPosition(1, end);
        }

    }

    public void HitWithLight(float pAmount)
    {
        throw new System.NotImplementedException();
    }

    public void EnterHitWithLight(float pAmount)
    {
        throw new System.NotImplementedException();
    }

    public void ExitHitWithLight()
    {
        throw new System.NotImplementedException();
    }
}
