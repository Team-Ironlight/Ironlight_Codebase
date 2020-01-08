using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxGizmo : MonoBehaviour
{
    [SerializeField] private Color color;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
