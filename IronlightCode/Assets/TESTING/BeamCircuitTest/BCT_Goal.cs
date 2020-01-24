using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCT_Goal : MonoBehaviour
{
    public GameObject bridge;

    public Vector3 currentPos;
    public Vector3 endPos;

    public float raiseTime = 0.5f;

    public bool Activated = false;


    // Start is called before the first frame update
    void Start()
    {
        currentPos = bridge.transform.position;
        endPos = currentPos;
        endPos.y += 2;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = bridge.transform.position;

        if (Activated)
        {
            bridge.transform.position = Vector3.Lerp(currentPos, endPos, raiseTime);
        }
    }
}
