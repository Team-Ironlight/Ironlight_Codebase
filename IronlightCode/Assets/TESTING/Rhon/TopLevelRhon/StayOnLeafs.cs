using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnLeafs : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.collider.transform.SetParent(null);
    }

}
