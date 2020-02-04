using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_HangingCrystal : MonoBehaviour
{
    public LayerMask lm;
    public LayerMask obstruction;
    public bool lineActive;
    public LineRenderer _line;
    public Transform startPoint;
    public Transform FirstCrystal;

    //private GameObject Crystal;


    void Start()
    {
        _line.gameObject.SetActive(true);

        _line.SetPosition(0, startPoint.position);
        _line.SetPosition(1, startPoint.position);
    }

    void Update()
    {
        if (lineActive)
        {
            _line.SetPosition(0, startPoint.position);
            _line.SetPosition(1, FirstCrystal.position);
        }
        else
        {
            _line.SetPosition(0, startPoint.position);
            _line.SetPosition(1, startPoint.position);
        }

        CheckForObstructions();

    }

    void CheckForObstructions()
    {
        Vector3 start = startPoint.position;
        Vector3 end = FirstCrystal.position;

        RaycastHit hit;
        if (Physics.Linecast(start, end, out hit, obstruction))
        {
            Vector3 modifiedEnd = hit.point;

            _line.SetPosition(1, modifiedEnd);
        }
        else if(Physics.Linecast(start, end, out hit, lm)) 
        {
            IHit hitThing = hit.transform.GetComponent<IHit>();

            if (hitThing != null)
            {
                Debug.Log("Found a thing");
                hitThing.HitWithLight(0);

                
            }
            _line.SetPosition(1, end);
            Debug.DrawLine(start, end, Color.green);
        }

    }




}
