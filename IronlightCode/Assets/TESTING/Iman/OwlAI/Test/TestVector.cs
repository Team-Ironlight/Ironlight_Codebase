using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVector : MonoBehaviour
{
    public GameObject Player;

    public int YPos; 
    public int GroundPos;
    public Vector3 startPos;

    public Vector3 SweepEndPos;

    Vector3 owlpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startPos, transform.position) > 0.1)
        {
            var direction = startPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);
            //move forward
            transform.Translate(0, 0, Time.deltaTime * 4);
        }
        else
        {
            var direction = Player.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);
        }
        calculateEndAttackPos();
        calculate();
    }

    void calculate()
    {
        owlpos = transform.position;
        owlpos.y = Player.transform.position.y;
        Vector3 dist = -(Player.transform.position - owlpos);
        dist = dist.normalized * GroundPos;
        dist = dist + Player.transform.position;
        dist.y = Player.transform.position.y + YPos;
        startPos = dist;
    }

    private void calculateEndAttackPos()
    {
        var PPos = Player.transform.position;
        PPos.y = transform.position.y;
        SweepEndPos = ((PPos - transform.position).normalized * GroundPos) + Player.transform.position;
        SweepEndPos.y = Player.transform.position.y + YPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, SweepEndPos);
    }
}
