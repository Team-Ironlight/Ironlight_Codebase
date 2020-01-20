using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_2ndBeam : MonoBehaviour
{
    [SerializeField] private GameObject Points;
    [SerializeField] private GameObject muzzle;
    //[SerializeField] public List<GameObject> beamLists = new List<GameObject>();
    [SerializeField] private List<point> beamList = new List<point>();
    [SerializeField] private int PointsInBeam;
    [SerializeField] private int PointSpeed;


    public LineRenderer line;

    struct point
    {
        public bool origin;
        public bool IsAtMaxDis;
        public bool isActive;
        public GameObject obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        //clone = Instantiate(Point, transform.position, transform.rotation);
        //clone.transform.parent = muzzle.transform;
        //clone.SetActive(false);
        //creating points for the beam
        Initialize();

        line.positionCount = PointsInBeam;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            //BeamActive();
            //beamLists.Add(Instantiate(Points, transform.position, transform.rotation));
        }
        //movePoint();

        DrawLine();
    }

    void DrawLine()
    {
        for (int i = 0; i < beamList.Count; i++)
        {
            line.SetPosition(i, beamList[i].obj.transform.position);
        }
    }

    public void BeamActive()
    {
        float togo = 5;
        for(int i = 0; i < beamList.Count;)
        {
            if(!beamList[i].origin)
            {
                if(Vector3.Distance(beamList[i].obj.transform.position,beamList[0].obj.transform.position) <= togo)
                {
                    //move the point
                    beamList[i].obj.transform.position = beamList[i - 1].obj.transform.forward * Time.deltaTime * PointSpeed;
                }
                else
                {
                    //beamList[i].IsAtMaxDis = true;
                    togo += 5;
                    i++;
                }
            }
            else
            {
                beamList[i].obj.transform.position = muzzle.transform.position;
                i++;
            }
        }
    }

    public void movePoint()
    {
        Vector3 point1 = Vector3.zero;
        Vector3 point2 = Vector3.zero;
        Vector3 direction = Vector3.zero;
        Vector3 forceToAdd = Vector3.zero;




        for (int i = 0; i < beamList.Count;)
        {

            point1 = beamList[i].obj.transform.position;
            point2 = beamList[i + 1].obj.transform.position;
            direction = beamList[9].obj.transform.forward;
            forceToAdd = direction * (Time.deltaTime * PointSpeed);

            Debug.Log(Vector3.Distance(point1, point2));



            if (Vector3.Distance(point1, point2) < 6)
            {
                beamList[i].obj.transform.position += forceToAdd;

                if (Vector3.Distance(point1, point2) > 5)
                {
                    i++;
                    continue;
                }
            }
        }
    }


    void Initialize()
    {
        for (int i = 0; i < PointsInBeam; i++)
        {
            point clone = new point();
            //clone.obj = Instantiate(Point, transform.position, transform.rotation);

            clone.obj = new GameObject();

            clone.obj.transform.forward = muzzle.transform.forward;

            if (i == 0)
            {
                clone.origin = true;
            }
            else
            {
                clone.origin = false;
            }

            //if(i == 9)
            //{
            //    clone.isActive = true;
            //}
            //else
            //{
            //    clone.isActive = false;
            //}
            clone.obj.transform.parent = muzzle.transform;
            beamList.Add(clone);

            Debug.Log(i.ToString() + ": " + clone.origin.ToString() + ": " + clone.isActive.ToString());
        }
    }

}
