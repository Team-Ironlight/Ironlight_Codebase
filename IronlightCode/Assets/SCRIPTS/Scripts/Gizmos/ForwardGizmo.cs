using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardGizmo : MonoBehaviour
{
    [SerializeField] private Color color;

    private void OnDrawGizmosSelected()
    {
        Vector3 lineEnd = transform.position + transform.forward;
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, lineEnd);
        Gizmos.DrawLine(lineEnd + transform.up / 6, lineEnd - transform.up / 6);
    }
}
