using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    private void Update()
    {
        Vector3 relativePos = PlayerTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }

    //// LateUpdate is called after Update methods
    //void LateUpdate()
    //{
    //    Vector3 newPos = PlayerTransform.position + _cameraOffset;
    //    transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    //}
}
