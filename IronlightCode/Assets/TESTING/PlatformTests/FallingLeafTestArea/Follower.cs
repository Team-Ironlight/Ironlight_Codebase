using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float travelled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        travelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(travelled);
    }
}
