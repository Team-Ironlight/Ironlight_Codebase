using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVector : MonoBehaviour
{
    public GameObject Player;

    public int YPos; 
    public int GroundPos;

    Vector3 owlpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(calculate(), transform.position) > 0.1)
        {
            var direction = calculate() - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);
            //move forward
            transform.Translate(0, 0, Time.deltaTime * 4);
        }
        else
        {
            var direction = Player.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);
        }
    }

    Vector3 calculate()
    {
        owlpos = transform.position;
        owlpos.y = Player.transform.position.y;
        Vector3 dist = -(Player.transform.position - owlpos);
        dist = dist.normalized * GroundPos;
        dist.y = Player.transform.position.y + YPos;;
        //transform.position = dist;
        return dist;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Player.transform.position);
    }
}
