using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCheckPoint : MonoBehaviour
{

    public Transform lastCheckPoint;
    public Transform StartPoint;
    private void Start()
    {
        lastCheckPoint = StartPoint;
    }

}
