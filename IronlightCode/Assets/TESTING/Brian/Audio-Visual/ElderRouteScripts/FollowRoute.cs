using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] Routes;
    private int routeToGo;
    private float param;
    private Vector3 ElderPos;
    public float speed;
    private bool CanCour;
    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        param = 0;
        //speed = 1;
        CanCour = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanCour)
        {
            StartCoroutine(MovePath(routeToGo));
        }
    }
    private IEnumerator MovePath(int routeNum)
    {
        CanCour = false;
        Vector3 p0 = Routes[routeNum].GetChild(0).position;
        Vector3 p1 = Routes[routeNum].GetChild(1).position;
        Vector3 p2 = Routes[routeNum].GetChild(2).position;
        Vector3 p3 = Routes[routeNum].GetChild(3).position;
        while(param < 1)
        {
            param += Time.deltaTime * speed;
            ElderPos = Mathf.Pow(1 - param, 3) * p0 + 3 * Mathf.Pow(1 - param, 2) * param * p1 + 3 * (1 - param) * Mathf.Pow(param, 2) * p2 + Mathf.Pow(param, 3) * p3;
            transform.position = ElderPos;
            yield return new WaitForEndOfFrame();
        }
        param = 0;
        routeToGo++;
        if (routeToGo > Routes.Length - 1)
        {
            routeToGo = 0;
        }
        CanCour = true;
    }
}
