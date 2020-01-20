using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENV_FallingLeaf : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float fallSpeed2 = 2f;
    public float sideSpeed = 5f;
    private bool switchSide = false;
    public bool upDown = false;

    public GameObject leafHolder;
    public LineRenderer line;
    public int count = 0;

    Vector3[] points = new Vector3[50];

    float timer = 0;
    public float Delay = 2;

    public float timer2 = 0;
    public float Delay2 = 4;

    float difference;
    Vector3 last;

    // Start is called before the first frame update
    void Start()
    {
        leafHolder = transform.parent.gameObject;
        last = transform.position;
        points[count] = leafHolder.transform.position;
        line.SetPosition(count, points[count]);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        //timer2 += Time.deltaTime;

        if (timer > Delay)
        {
            switchSide = !switchSide;
            timer = 0;
            DrawLine();
            last = transform.position;
        }

        difference = last.y - transform.position.y;

        //if (timer2 > Delay2)
        //{
        //    upDown = !upDown;
        //    timer2 = 0;
        //}
        holder();

        Falling();
        sideFall();
        
    }

    void Awake()
    {
        
    }

    void holder()
    {
        leafHolder.transform.Translate(0, -(Mathf.Sin(-difference * Time.deltaTime)), 0);
    }
    
    void DrawLine()
    {
        count++;
        points[count] = leafHolder.transform.position;
        line.SetPosition(count, points[count]);
    }

    void Falling()
    {
        
        if (upDown == false)
        {
            transform.Translate(0, -fallSpeed * Time.deltaTime, 0);

        }
        //if (upDown == true)
        //{
        //    transform.Translate(0, fallSpeed2 * Time.deltaTime, 0);
        //}
    }

    void sideFall()
    {
        if (switchSide == false)
        {
            transform.Translate(sideSpeed * Time.deltaTime, 0, 0);
            
        }
        if (switchSide == true)
        {
            transform.Translate(-sideSpeed * Time.deltaTime, 0, 0);
        }
    }


    
}
