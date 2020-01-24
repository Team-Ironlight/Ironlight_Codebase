using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardGizmo : MonoBehaviour
{
    [SerializeField] private Color _cColor;
    [SerializeField] private float _fLength;

    private void OnDrawGizmos()
    {
        Vector3 lineEnd = transform.position + transform.forward * _fLength;
        Gizmos.color = _cColor;
        Gizmos.DrawLine(transform.position, lineEnd);
        Gizmos.DrawLine(lineEnd + transform.up / 6, lineEnd - transform.up / 6);
    }
}
