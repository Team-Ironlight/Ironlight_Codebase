using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public PathForLeaf path;

    public void CreatePath()
    {
        path = new PathForLeaf(transform.position);
    }
}
