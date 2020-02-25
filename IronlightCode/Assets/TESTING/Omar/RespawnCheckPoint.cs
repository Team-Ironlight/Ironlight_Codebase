using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCheckPoint : MonoBehaviour
{

    public Transform lastCheckPoint;
    public Transform startPos;

    private void Start()
    {
        lastCheckPoint = startPos;
    }

}
