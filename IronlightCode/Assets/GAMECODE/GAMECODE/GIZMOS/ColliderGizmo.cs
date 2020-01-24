using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGizmo : MonoBehaviour
{
    [SerializeField] private Color color;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
    }
}
