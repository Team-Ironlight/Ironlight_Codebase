using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRT_Reflect : MonoBehaviour
{
    public GameObject objHit = null;

    LineRenderer m_line;

    public Transform p1;
    public Transform p2;

    public float lengthOfCast = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        m_line = GetComponentInChildren<LineRenderer>();

        m_line.SetPosition(0, p1.position);
        m_line.SetPosition(1, p2.position);
    }

    // Update is called once per frame
    void Update()
    {
        m_line.SetPosition(0, p1.position);
        m_line.SetPosition(1, p2.position);

        if (Input.GetKey(KeyCode.Space))
        {
            p2.position += (p2.forward * 0.1f);
        }

        CheckForWall();


        //Debug.Log(p2.TransformVector(p2.position));
    }

    void CheckForWall()
    {
        RaycastHit hit;
        Vector3 start;
        Vector3 end;
        Vector3 dir;
        Vector2 u = Vector2.zero;
        Vector2 v = Vector2.zero;

        start = p2.position;
        dir = p2.forward;
        end = start + (dir * lengthOfCast);

        if (Physics.Linecast(start, end, out hit))
        {
            objHit = hit.collider.gameObject;

            u = dir.normalized;

            v = hit.normal;

            CalculateProjAngle(u, v);
            //Debug.Log(objHit.transform.TransformVector(objHit.transform.position));
        }
        else
        {
            objHit = null;
        }

    }


    void CalculateProjAngle(Vector3 u, Vector3 v)
    {
        Debug.Log(u);
        Debug.Log(v);

        
    }



    private void OnDrawGizmos()
    {
        Vector3 start;
        Vector3 end;
        Vector3 dir;

        start = p2.position;
        dir = p2.forward;
        end = start + (dir * lengthOfCast);

        Debug.DrawLine(start, end, Color.black);
    }
}
